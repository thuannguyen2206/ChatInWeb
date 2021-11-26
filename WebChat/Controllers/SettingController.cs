using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebChat.Common.Utilities.Constants;
using WebChat.Service.IServices;

namespace WebChat.WebApp.Controllers
{
    public class SettingController : BaseController
    {
        private readonly IUserService _userService;

        public SettingController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> LoadSetting()
        {
            await GetInfo();
            return PartialView("_Setting");
        }

        [HttpGet]
        public async Task<PartialViewResult> GetInfo()
        {
            var user = await _userService.GetByName(HttpContext.Session.GetString(SystemConstant.UserName));
            return PartialView("_Info", user);
        }
    }
}
