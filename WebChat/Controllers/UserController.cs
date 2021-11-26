using System;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using WebChat.Common.Utilities.Constants;
using WebChat.Common.Utilities.Enums;
using WebChat.Common.ViewModels.User;
using WebChat.Service.IServices;
using WebChat.WebApp.Hubs;

namespace WebChat.WebApp.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly INotyfService _notyf;
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly ILogger<AccountController> _logger;

        public UserController(IUserService userService, INotyfService notyf,
            IHubContext<ChatHub> hubContext, ILogger<AccountController> logger)
        {
            _userService = userService;
            _notyf = notyf;
            _hubContext = hubContext;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                _notyf.Error("Dữ liệu không hợp lệ", 4);
            }
            else
            {
                var result = await _userService.UpdateProfile(model);
                if (!result.Succeeded)
                {
                    _notyf.Error("Đã có lỗi xảy ra, vui lòng thử lại sau", 4);
                }
                else
                {
                    _notyf.Success("Cập nhật thông tin cá nhân thành công");
                }
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult ChangePassword(Guid Id)
        {
            var model = new ChangePasswordViewModel()
            {
                Id = Id,
                PresentPassword = string.Empty,
                NewPassword = string.Empty,
                ConfirmNewPassword = string.Empty
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var result = await _userService.ChangePassword(model);
                if (result == ChangePasswordResult.Success)
                {
                    _notyf.Success("Thay đổi mật khẩu thành công", 4);
                    return RedirectToAction("Index", "Home");
                }
                else if (result == ChangePasswordResult.Invalid)
                {
                    ModelState.AddModelError(string.Empty, "Sai mật khẩu.");
                    return View(model);
                }
                ModelState.AddModelError(string.Empty, "Dữ liệu không hợp lệ, vui lòng kiểm tra lại hoặc thử lại sau.");
                return RedirectToAction("Login", "Account");
            }
            catch (Exception)
            {
                _notyf.Error("Đã có lỗi xảy ra, vui lòng thử lại sau.");
                return View(model);
            }

        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            try
            {
                Guid userId = Guid.Parse(HttpContext.Session.GetString(SystemConstant.UserId));
                if (userId != null && userId != Guid.Empty)
                {
                    _userService.ChangeStatus(userId, false);
                }

                await _userService.Logout();
                //var connectionIds = _userService.GetConnectionIdsWhenLog(userId);
                //if (connectionIds != null && connectionIds.Count > 0)
                //    _hubContext.Clients.Clients(connectionIds).SendAsync("UserLogout", userId);

                HttpContext.Session.Clear();
                return RedirectToAction("Login", "Account");
            }
            catch (Exception e)
            {
                _notyf.Error("Không thể thực hiện thao tác");
                _logger.LogError("Exception logout: " + e.Message);
                return RedirectToAction("Index", "Home");
            }
        }

    }
}
