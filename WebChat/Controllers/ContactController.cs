using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using WebChat.Common.Utilities.Constants;
using WebChat.Common.Utilities.Enums;
using WebChat.Common.ViewModels.Chat;
using WebChat.Common.ViewModels.Notification;
using WebChat.Common.ViewModels.User;
using WebChat.Service.IServices;
using WebChat.WebApp.Hubs;

namespace WebChat.WebApp.Controllers
{
    public class ContactController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IChatService _chatService;
        private readonly INotificationService _notificationService;
        private readonly INotyfService _notyf;
        private readonly IHubContext<ChatHub> _hubContext;

        public ContactController(IUserService userService, IChatService chatService,
            INotificationService notificationService, INotyfService notyf, IHubContext<ChatHub> hubContext)
        {
            _userService = userService;
            _chatService = chatService;
            _notificationService = notificationService;
            _notyf = notyf;
            _hubContext = hubContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetListContact(string keyword)
        {
            Guid userId = Guid.Parse(HttpContext.Session.GetString(SystemConstant.UserId));
            var lists = _userService.GetListContacts(userId, keyword);
            return PartialView("_ListContacts", lists);
        }

        // Send a notification you want to make friends with other users
        [HttpPost]
        public IActionResult SendNotifyAddNewContact(Guid contactId, string message)
        {
            var userId = Guid.Parse(HttpContext.Session.GetString(SystemConstant.UserId));
            var username = HttpContext.Session.GetString(SystemConstant.UserName);
            // nếu message != null => kiểm tra xem đã có group giữa 2 user chưa
            // nếu chưa thì tạo 1 group => add 2 user vào
            // add message vào group đó
            if (!string.IsNullOrEmpty(message))
            {
                var checkId = _chatService.CheckChatGroup(userId, contactId);
                if (checkId == Guid.Empty)
                {
                    var groupId = _chatService.CreateChatGroup(string.Empty);
                    var chatLog = new ChatLogViewModel()
                    {
                        ChatGroupId = groupId,
                        Content = message,
                        SenderId = userId,
                        TimeSent = DateTime.Now
                    };
                    _chatService.AddUsersToGroup(groupId, new List<Guid>() { userId, contactId });
                    _chatService.AddChatLog(chatLog);
                }
            }
            // Kiểm tra contact có gửi kết bạn không
            var contact = _userService.GetById(contactId);
            var checkContactRequest = _notificationService.CheckNewContactRequest(userId, contactId);
            
            if (checkContactRequest)
            {
                _notyf.Success(string.Format("Một yêu cầu kết bạn cũng được gửi từ {0}, vì vậy giờ đây cả hai đã là bạn bè.", contact.Username));
                return Json(true);
            }

            var newNotiModel = new NewNotificationViewModel()
            {
                ContentNotify = username + " muốn thêm liên hệ với bạn",
                ReceiverId = contactId,
                SenderId = userId,
                TypeNotify = TypeNotification.NewContact
            };
            
            var noti = _notificationService.AddNotificationFromUser(newNotiModel);
            if (noti.Result == NewNotifResult.Success)
            {
                var connectionIds = _userService.GetUserConnectionIds(contactId);
                if (connectionIds != null && connectionIds.Count > 0)
                {
                    var user = _userService.GetById(userId);
                    var notiModel = new NotificationViewModel()
                    {
                        NotificationId = noti.NotificationId,
                        AvatarLink = user.AvatarLink,
                        ContentNotify = newNotiModel.ContentNotify,
                        SenderId = userId,
                        TypeNotify = TypeNotification.NewContact,
                        UserName = user.Username,
                        TimeSend = DateTime.Now
                    };
                    _hubContext.Clients.Clients(connectionIds).SendAsync("NotificationNewContact", notiModel);
                }
                // Gửi đi 1 thông báo thành công
                _notyf.Success(string.Format("Gửi kết bạn cho {0} thành công", contact.Username));
                return Json(true);
            }
            else if (noti.Result == NewNotifResult.Exist)
            {
                _notyf.Information(string.Format("Bạn đã gửi lời mời kết bạn cho {0} rồi.", contact.Username));
            }
            else
            {
                _notyf.Error("Đã có lỗi xảy ra, vui lòng thử lại sau.", 4);
            }
            return Json(false);
        }

        [HttpGet]
        public IActionResult GetListToNewContact(string keyword, int page)
        {
            Guid userId = Guid.Parse(HttpContext.Session.GetString(SystemConstant.UserId));
            var contacts = _userService.GetListToAddContact(userId, keyword, page);
            if (contacts != null)
                return Json(contacts);

            return Json(null);
        }

        [HttpPost]
        public IActionResult AcceptNewContact(Guid senderId, int notiId)
        {
            var userId = Guid.Parse(HttpContext.Session.GetString(SystemConstant.UserId));
            var result = _userService.AcceptNewContactRequest(userId, senderId);
            var sender = _userService.GetById(senderId);
            
            if (result == NewContactResult.Successed)
            {
                var connectionIds = _userService.GetUserConnectionIds(senderId);
                if (connectionIds != null && connectionIds.Count > 0)
                {
                    var user = _userService.GetById(userId); // my info
                    var contactModel = new ContactViewModel()
                    {
                        ContactId = userId,
                        AvatarLink = user.AvatarLink,
                        Address = user.Address,
                        UserName = user.Username,
                        FirstName = user.FirstName,
                        Status = true
                    };
                    _hubContext.Clients.Clients(connectionIds).SendAsync("AcceptNewContact", contactModel);
                }

                string content = string.Format("Bạn đã đồng ý kết bạn với {0}", sender.Username);
                _notyf.Success(content, 4);
                _notificationService.UpdateWhenAcceptContact(notiId, content);
                return Json(new { status = true, content = content });
            }
            else if(result == NewContactResult.Exist)
            {
                _notyf.Information(string.Format("Bạn đã có bạn đã có liên lạc với {0}", sender.Username));
            }
            else
            {
                _notyf.Error("Tạm thời không thể thực hiện thao tác này");
            }
            return Json(new { status = false, content = string.Empty });
        }

        [HttpPost]
        public IActionResult IgnoreNewContact(Guid senderId, int notiId)
        {
            bool result = _notificationService.DeleteNotification(notiId);
            if(result)
            {
                var sender = _userService.GetById(senderId);
                _notyf.Success(string.Format("Bạn đã bỏ qua lời mời kết bạn với {0}", sender.Username));
            }
            else
            {
                _notyf.Error("Tạm thời không thể thực hiện thao tác này", 4);
            }
            return Json(result);
        }

        [HttpGet]
        public IActionResult LoadContact(string keyword, int pageIndex)
        {
            Guid userId = Guid.Parse(HttpContext.Session.GetString(SystemConstant.UserId));
            var lists = _userService.GetListContacts(userId, keyword, pageIndex);
            if (lists != null)
                return Json(lists);

            return Json(string.Empty);
        }

        [HttpPost]
        public IActionResult Unfriend(Guid groupId)
        {
            var userId = Guid.Parse(HttpContext.Session.GetString(SystemConstant.UserId));
            var friendId = _userService.Unfriend(groupId, userId);
            if (friendId != Guid.Empty)
            {
                var connectionIds = _userService.GetListConnectionIds(groupId);
                var obj = new List<Guid>() { userId, friendId };
                if (connectionIds.Count > 0)
                    _hubContext.Clients.Clients(connectionIds).SendAsync("Unfriend", obj);
                _notyf.Success("Hủy kết bạn thành công!");
                return Json(true);
            }
            _notyf.Error("Hiện tại không thể thực hiện thao tác này!");
            return Json(false);
        }

        [HttpPost]
        public IActionResult BlockFriend(Guid groupId)
        {
            var userId= Guid.Parse(HttpContext.Session.GetString(SystemConstant.UserId));
            var friendId = _userService.BlockOrUnBlockFriend(groupId, userId);
            if (friendId != Guid.Empty)
            {
                var user = _userService.GetById(friendId);
                var connectionIds = _userService.GetUserConnectionIds(friendId);
                var obj = new Dictionary<string, Guid>() { { "friendId", userId }, { "groupId", groupId } };
                if (connectionIds.Count > 0)
                    _hubContext.Clients.Clients(connectionIds).SendAsync("BlockFriend", obj);
                _notyf.Success(string.Format("Đã chặn {0} thành công", user.Username));
                return Json(true);
            }
            _notyf.Error("Tạm thời không thể thực hiện thao tác, vui lòng thử lại sau");
            return Json(false);
        }

        [HttpPost]
        public IActionResult UnBlockFriend(Guid groupId)
        {
            var userId = Guid.Parse(HttpContext.Session.GetString(SystemConstant.UserId));
            var friendId = _userService.BlockOrUnBlockFriend(groupId, userId);
            if (friendId != Guid.Empty)
            {
                var user = _userService.GetById(friendId);
                _notyf.Success(string.Format("Đã hủy chặn đối với {0} thành công", user.Username));
                return Json(true);
            }
            _notyf.Error("Tạm thời không thể thực hiện thao tác, vui lòng thử lại sau");
            return Json(false);
        }

    }
}
