using System;
using System.IO;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.SignalR;
using WebChat.Common.Utilities.Constants;
using WebChat.Common.ViewModels.Discussion;
using WebChat.Service.IServices;
using WebChat.WebApp.Hubs;

namespace WebChat.WebApp.Controllers
{
    public class DiscussionController : BaseController
    {
        private readonly IDiscussionService _discussionService;
        private readonly IUserService _userService;
        private readonly IChatService _chatService;
        private readonly INotyfService _notyf;
        private readonly ICompositeViewEngine _viewEngine;
        private readonly IHubContext<ChatHub> _hubContext;

        public DiscussionController(IDiscussionService discussionService, IUserService userService,
            IChatService chatService, INotyfService notyf, ICompositeViewEngine viewEngine,
            IHubContext<ChatHub> hubContext)
        {
            _discussionService = discussionService;
            _userService = userService;
            _chatService = chatService;
            _notyf = notyf;
            _viewEngine = viewEngine;
            _hubContext = hubContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetListDiscussion(string keyword)
        {
            var userId = Guid.Parse(HttpContext.Session.GetString(SystemConstant.UserId));
            var model = _discussionService.GetListDiscussions(userId, keyword);
            return PartialView("_ListDiscussions", model);
        }

        [HttpPost]
        public IActionResult CreateNewChat(NewChatViewModel data)
        {
            var userId = Guid.Parse(HttpContext.Session.GetString(SystemConstant.UserId));

            if (data.ListMembers.Count == 1)
            {
                var check = _chatService.CheckChatGroup(userId, data.ListMembers[0]);
                if (check != Guid.Empty)
                {
                    _notyf.Information("Bạn đã có group chat với người này");
                }
                else
                {
                    _notyf.Information("Hãy chọn thêm thành viên cho nhóm");
                }
                return Json(new { status = false });
            }

            data.ListMembers.Add(userId);
            data.OwnerId = userId;
            var groupId = _discussionService.CreateNewGroup(data);
            if (groupId != null && groupId != Guid.Empty)
            {
                var discussionModel = new DiscussionViewModel()
                {
                    AvatarDiscussion = SystemConstant.DefaultGroupAvatar,
                    GroupId = groupId,
                    LastMessage = "Chưa có tin nhắn",
                    NameDiscussion = data.NameGroup,
                    Notification = 0,
                    TimeSended = DateTime.Now
                };
                var connectionIds = _userService.GetListConnectionIds(groupId);
                _notyf.Success("Tạo nhóm chat thành công");
                _hubContext.Clients.Clients(connectionIds).SendAsync("NewDiscussion", discussionModel);

                //var model = _discussionService.GetListDiscussions(userId);
                //_notyf.Success("Tạo nhóm chat thành công");
                //return Json(new { status = true, html = this.RenderPartialViewToString("_ListDiscussions", model) });
                return Json(new { status = true });
            }
            _notyf.Error("Đã có lỗi, vui lòng thử lại sau");
            return Json(new { status = false });
        }

        [HttpGet]
        public IActionResult GetMyContacts(string keyword, int page)
        {
            Guid userId = Guid.Parse(HttpContext.Session.GetString(SystemConstant.UserId));
            var contacts = _userService.GetListContacts(userId, keyword, page);
            if (contacts != null)
                return Json(contacts);

            return Json(null);
        }

        [HttpGet]
        public IActionResult GetListToAddMember(string keyword, int page, Guid groupId)
        {
            Guid userId = Guid.Parse(HttpContext.Session.GetString(SystemConstant.UserId));
            var contacts = _userService.GetListToAddMember(userId, groupId, keyword, page);
            if (contacts != null)
                return Json(contacts);

            return Json(null);
        }

        [HttpGet]
        public IActionResult DiscussionDetail(Guid groupId)
        {
            var userId = Guid.Parse(HttpContext.Session.GetString(SystemConstant.UserId));
            var model = _discussionService.GetDiscussionDetail(groupId, userId);
            if (model.OwnerId != null)
            {
                return Json(new { status = true, html = RenderPartialViewToString("_PreviewGroup", model) });
            }

            return Json(new { status = false, html = string.Empty });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateDiscussion(DiscussionDetailViewModel model)
        {
            var result = await _discussionService.UpdateDiscussion(model);
            if (result)
            {
                _notyf.Success("Cập nhật thông tin nhóm thành công");
            }
            else
            {
                _notyf.Error("Đã có lỗi xảy ra, vui lòng thử lại sau");
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult LoadDiscussion(string keyword, int pageIndex)
        {
            var userId = Guid.Parse(HttpContext.Session.GetString(SystemConstant.UserId));
            var result = _discussionService.GetListDiscussions(userId, keyword, pageIndex);
            if (result != null)
                return Json(result);

            return Json(string.Empty);
        }

        [HttpPost]
        public IActionResult RemoveMember(Guid groupId, Guid memberId)
        {
            var member = _userService.GetById(memberId);
            var result = _discussionService.RemoveMember(groupId, memberId);
            if (result)
            {
                var connectionIds = _userService.GetUserConnectionIds(memberId);
                if (connectionIds != null && connectionIds.Count > 0)
                {
                    _hubContext.Clients.Clients(connectionIds).SendAsync("RemoveGroupMember", groupId);
                }
                _notyf.Success(string.Format("Bạn đã xóa thành viên {0} ra khỏi nhóm!", member.Username));
                return Json(true);
            }
            _notyf.Error(string.Format("Đã có lỗi, tạm thời không thể xóa {0} ra khỏi nhóm!", member.Username));
            return Json(false);
        }

        [HttpGet]
        public IActionResult LoadMembers(Guid groupId, int pageIndex)
        {
            var userId = Guid.Parse(HttpContext.Session.GetString(SystemConstant.UserId));
            var result = _discussionService.LoadMembers(groupId, userId, pageIndex);
            if (result.ListMembers.Count > 0)
                return Json(new { status = true, result = result });

            return Json(new { status = false, result = result });
        }

        [HttpPost]
        public IActionResult RemoveDiscussion(Guid groupId)
        {
            var connectionIds = _userService.GetListConnectionIds(groupId);
            var result = _discussionService.RemoveDiscussion(groupId);
            if (!result)
            {
                _notyf.Error("Tạm thời không thể thực hiện thao tác này");
                return Json(new { status = false, redirectToUrl = string.Empty });
            }

            if (connectionIds.Count > 0)
            {
                _hubContext.Clients.Clients(connectionIds).SendAsync("RemoveGroup", groupId);
            }
            _notyf.Success("Xóa nhóm chat thành công");
            return Json(new { status = true, redirectToUrl = Url.Action("Index", "Home") });
        }

        [HttpPost]
        public IActionResult LeaveDiscussion(Guid groupId)
        {
            var userId = Guid.Parse(HttpContext.Session.GetString(SystemConstant.UserId));
            var result = _discussionService.RemoveMember(groupId, userId);
            if (!result)
            {
                _notyf.Error("Tạm thời không thể thực hiện thao tác này");
                return Json(new { status = false, redirectToUrl = string.Empty });
            }

            var connectionIds = _userService.GetUserConnectionIds(userId);
            if (connectionIds != null && connectionIds.Count > 0)
            {
                _hubContext.Clients.Clients(connectionIds).SendAsync("RemoveGroup", groupId);
            }
            _notyf.Success("Rời khỏi nhóm thành công");
            return Json(new { status = true, redirectToUrl = Url.Action("Index", "Home") });
        }

        private async Task<string> RenderPartialViewToString(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = ControllerContext.ActionDescriptor.ActionName;

            ViewData.Model = model;

            using (var writer = new StringWriter())
            {
                ViewEngineResult viewResult = _viewEngine.FindView(ControllerContext, viewName, false);

                ViewContext viewContext = new ViewContext(
                    ControllerContext,
                    viewResult.View,
                    ViewData,
                    TempData,
                    writer,
                    new HtmlHelperOptions()
                );

                await viewResult.View.RenderAsync(viewContext);

                return writer.GetStringBuilder().ToString();
            }
        }

    }
}
