using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebChat.Common.Utilities.Constants;
using WebChat.Service.IServices;

namespace WebChat.WebApp.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IUserService _userService;

        public HomeController(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<IActionResult> Index()
        {
            await GetAvatarAsync();
            return View();
        }

        [HttpGet]
        public async Task<PartialViewResult> GetAvatarAsync()
        {
            var userId = Guid.Parse(HttpContext.Session.GetString(SystemConstant.UserId));
            string path = await _userService.GetAvatar(userId);
            ViewData["Avatar"] = path;
            return PartialView("_Avatar");
        }


    }
}
