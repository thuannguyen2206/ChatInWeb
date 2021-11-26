using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SendGrid;
using SendGrid.Helpers.Mail;
using WebChat.Common.Utilities.Constants;
using WebChat.Common.Utilities.Enums;
using WebChat.Common.Utilities.Storage;
using WebChat.Common.ViewModels.Account;
using WebChat.Service.IServices;
using WebChat.WebApp.Hubs;

namespace WebChat.WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IEmailService _emailService;
        private readonly IUserService _userService;
        private readonly INotyfService _notyf;
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly ILogger<AccountController> _logger;
        private readonly IConfiguration _configuration;

        public AccountController(IAccountService accountService, IEmailService emailService, 
            IUserService userService, INotyfService notyf, IHubContext<ChatHub> hubContext, 
            ILogger<AccountController> logger, IConfiguration configuration)
        {
            _accountService = accountService;
            _emailService = emailService;
            _userService = userService;
            _notyf = notyf;
            _hubContext = hubContext;
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                HttpContext.Session.Clear();
                var result = await _accountService.Login(model);

                if (result.Succeeded)
                {
                    await InitialData(model.Username);
                    //var userId = Guid.Parse(HttpContext.Session.GetString(SystemConstant.UserId));
                    //var connectionIds = _userService.GetConnectionIdsWhenLog(userId);
                    //if (connectionIds != null && connectionIds.Count > 0)
                    //    await _hubContext.Clients.Clients(connectionIds).SendAsync("UserLogin", userId);
                    return RedirectToAction("Index", "Home");
                }
                else if (result.IsNotAllowed)
                {
                    ModelState.AddModelError("", "Vui lòng xác nhận email trước khi đăng nhập.");
                }
                else if (result.IsLockedOut)
                {
                    ModelState.AddModelError(string.Empty, "Tài khoản bị khoá. Xin vui lòng thử lại sau.");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Thông tin đăng nhập không hợp lệ.");
                }
                return this.View(model);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Đã có lỗi xảy ra, xin vui lòng thử lại sau.");
                _logger.LogError("Exception login: " + e.Message);
                return this.View(model);
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // If the registration is successful, send a confirmation email
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var captcha = Request.Form["g-recaptcha-response"].ToString();
                if (!await IsValidCaptcha(captcha))
                {
                    return View(model);
                } 

                model.AvatarLink = SystemConstant.DefaultUserAvatar;
                var result = await _accountService.Register(model);
                if (result == RegisterAccountResult.Successed)
                {
                    // get token
                    var tokenEmailConfirm = await _emailService.EmailConfirmationToken(model.Username);
                    // Encode token
                    tokenEmailConfirm = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(tokenEmailConfirm));
                    // Callback url
                    var confirmationLink = Url.Action("ConfirmEmail", "Email", new { token = tokenEmailConfirm, email = model.Email }, Request.Scheme);

                    try // Using SendGrid to send email
                    {
                        await SendGridEmail(model.Username, model.Email, confirmationLink);
                        _notyf.Success("Tạo tài khoản thành công");
                        return RedirectToAction("Login", "Account");
                    }
                    catch (Exception e)
                    {
                        _notyf.Error("Không thể gửi email xác nhận, vui lòng thử lại sau.");
                        await _userService.PermanentDeleted(model.Email);
                        _logger.LogError("Exception send email: " + e.Message);
                        return View(model);
                    }
                }
                else if (result == RegisterAccountResult.ExistUsername)
                {
                    ModelState.AddModelError(string.Empty, "Tên tài khoản đã tồn tại.");
                }
                else if (result == RegisterAccountResult.ExistEmail)
                {
                    ModelState.AddModelError(string.Empty, "Email đã đăng kí cho tài khoản khác.");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Thông tin đăng kí không hợp lệ.");
                }
                return View(model);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Đã có lỗi xảy ra, xin vui lòng thử lại sau.");
                await _userService.PermanentDeleted(model.Email);
                _logger.LogError("Exception register: " + e.Message);
                return View(model);
            }
        }

        private async Task InitialData(string username)
        {
            var user = await _userService.GetByName(username);
            HttpContext.Session.SetString(SystemConstant.UserName, username);
            HttpContext.Session.SetString(SystemConstant.UserId, user.Id.ToString());
            _userService.ChangeStatus(user.Id, true);
            _userService.RemoveConnections(user.Id);
        }

        public IActionResult AccessDenied()
        {
            return this.View();
        }

        private async Task SendGridEmail(string userName, string toEmail, string confirmationLink)
        {
            string apiKey = _configuration.GetValue<string>("SendGridKey");
            string fromEmail = _configuration.GetValue<string>("FromMyEmail");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(fromEmail, "WebChat");
            var subject = "Xác nhận email đăng kí";
            var to = new EmailAddress(toEmail, "User");
            var plainTextContent = string.Format("Xin chào {0}, bạn vừa đăng kí tài khoản tại WebChat, để đăng nhập thì bạn hãy vui lòng xác nhận email này nha.", userName);

            string directory = Directory.GetCurrentDirectory();
            var fullPath = string.Format("{0}{1}", directory, "\\wwwroot\\user-content\\template\\confirm.html");
            string htmlContent = System.IO.File.ReadAllText(fullPath);
            htmlContent = htmlContent.Replace("{{UserName}}", userName);
            htmlContent = htmlContent.Replace("{{LifeTime}}", SystemConstant.LifeTimeConfirmEmail.ToString());
            htmlContent = htmlContent.Replace("{{ConfirmationLink}}", confirmationLink);

            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }

        private async Task<bool> IsValidCaptcha(string captcha)
        {
            try
            {
                using var client = new HttpClient();
                string secretKey = _configuration.GetValue<string>("ReCaptcha:ServerKey");
                var response = await client.PostAsync($"https://www.google.com/recaptcha/api/siteverify?secret={secretKey}&response={captcha}", new StringContent(""));
                var jsonString = await response.Content.ReadAsStringAsync();
                var captchaVerfication = JsonConvert.DeserializeObject<JObject>(jsonString);
                var result = captchaVerfication.GetValue("success");
                return (bool)result;
            }
            catch (Exception)
            {
                _logger.LogError("Failed to verify captcha.");
                return false;
            }
        }

    }
}
