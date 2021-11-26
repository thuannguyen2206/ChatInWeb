using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebChat.Service.IServices;

namespace WebChat.WebApp.Controllers
{
    public class EmailController : Controller
    {
        private readonly IEmailService _emailService;
        private readonly INotyfService _notyf;
        private readonly IUserService _userService;

        public EmailController(IEmailService emailService, INotyfService notyf, IUserService userService)
        {
            _emailService = emailService;
            _notyf = notyf;
            _userService = userService;
        }

        public async Task<IActionResult> ConfirmEmail(string email, string token)
        {
            var resultConfirm = await _emailService.EmailConfirmed(email, token);
            if (resultConfirm)
            {
                _notyf.Success("Xác nhận email thành công");
                return RedirectToAction("Login", "Account");
            }
            else
            {
                await _userService.PermanentDeleted(email);
                _notyf.Error("Xác nhận không thành công, vui lòng đăng kí lại tài khoản mới để sử dụng WEBCHAT");
                return RedirectToAction("Register", "Account");
            }

        }
    }
}
