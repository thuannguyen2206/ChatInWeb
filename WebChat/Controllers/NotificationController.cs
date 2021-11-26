using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using WebChat.Common.Utilities.Constants;
using WebChat.Service.IServices;

namespace WebChat.WebApp.Controllers
{
    public class NotificationController : BaseController
    {
        private readonly INotificationService _notifycationService;

        public NotificationController(INotificationService notifycationService)
        {
            _notifycationService = notifycationService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetListNotification(string keyword)
        {
            Guid userId = Guid.Parse(HttpContext.Session.GetString(SystemConstant.UserId));
            var lists = _notifycationService.GetListNotifications(userId, keyword);
            return PartialView("_ListNotifications", lists);
        }

        [HttpPost]
        public IActionResult DeleteWhenRead(Guid groupId)
        {
            var userId = Guid.Parse(HttpContext.Session.GetString(SystemConstant.UserId));
            bool result = _notifycationService.DeleteWhenRead(userId, groupId);
            return Json(result);
        }

        [HttpGet]
        public IActionResult LoadNotification( string keyword, int pageIndex)
        {
            Guid userId = Guid.Parse(HttpContext.Session.GetString(SystemConstant.UserId));
            var result = _notifycationService.GetListNotifications(userId, keyword, pageIndex);
            if (result != null)
                return Json(result);

            return Json(string.Empty);
        }

    }
}
