using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebChat.Common.Utilities.Constants;
using WebChat.Common.ViewModels.Chat;
using WebChat.Service.IServices;
using Microsoft.AspNetCore.SignalR;
using WebChat.WebApp.Hubs;
using WebChat.Common.Utilities.Storage;
using AspNetCoreHero.ToastNotification.Abstractions;
using WebChat.Common.ViewModels.Notification;
using System.IO;
using System.Threading.Tasks;
using WebChat.Common.Utilities.Enums;
using Microsoft.Extensions.Logging;

namespace WebChat.WebApp.Controllers
{
    public class ChatController : BaseController
    {
        private readonly IChatService _chatService;
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly IFileStorage _fileStorage;
        private readonly IUserService _userService;
        private readonly INotyfService _notyf;
        private readonly INotificationService _notiService;
        private readonly ILogger<ChatController> _logger;

        public ChatController(IChatService chatService, IHubContext<ChatHub> hubContext,
            IFileStorage fileStorage, IUserService userService, INotyfService notyf,
            INotificationService notiService, ILogger<ChatController> logger)
        {
            _chatService = chatService;
            _hubContext = hubContext;
            _fileStorage = fileStorage;
            _userService = userService;
            _notyf = notyf;
            _notiService = notiService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult LoadChatForm(Guid contactId)
        {
            var chatGroup = GetChatGroupFromContactId(contactId);
            ViewData["ChatGroup"] = chatGroup;
            if (chatGroup != null)
            {
                var chatLogs = GetChatLogs(chatGroup.Id);
                ViewData["ChatLogs"] = chatLogs;
            }
            return PartialView("_ChatForm");
        }

        [HttpGet]
        public IActionResult LoadChatForm2(Guid groupId)
        {
            var userId = Guid.Parse(HttpContext.Session.GetString(SystemConstant.UserId));
            ViewData["Me"] = userId;

            var chatGroup = _chatService.GetChatGroup(groupId);
            ViewData["ChatGroup"] = chatGroup;

            if (chatGroup != null)
            {
                var chatLogs = GetChatLogs(chatGroup.Id);
                ViewData["ChatLogs"] = chatLogs;
            }
            return PartialView("_ChatForm");
        }

        private ChatGroupViewModel GetChatGroupFromContactId(Guid contactId)
        {
            var userId = Guid.Parse(HttpContext.Session.GetString(SystemConstant.UserId));
            ViewData["Me"] = userId;
            var chatGroup = _chatService.GetChatGroup(userId, contactId);
            if (chatGroup != null)
            {
                var model = new ChatGroupViewModel()
                {
                    Id = chatGroup.Id,
                    AvatarLink = chatGroup.AvatarLink,
                    Name = chatGroup.Name,
                    TotalMember = chatGroup.TotalMember,
                    OwnerId = chatGroup.OwnerId,
                    IsOwner = false
                };
                return model;
            }
            return null;
        }

        // Paging result, each time will load the corresponding number of records pageIndex and SystemContants.PageSize
        private List<ChatLogViewModel> GetChatLogs(Guid groupId, int pageIndex = 1)
        {
            var chatLogs = _chatService.ListChatLogs(groupId, pageIndex);
            return chatLogs;
        }

        [HttpPost]
        public IActionResult SendFile()
        {
            try
            {
                IFormFile file;
                Guid userId, groupId;
                try
                {
                    file = HttpContext.Request.Form.Files["uploadfile"];
                    userId = Guid.Parse(HttpContext.Request.Form["userid"]);
                    groupId = Guid.Parse(HttpContext.Request.Form["groupid"]);
                }
                catch (Exception e)
                {
                    _notyf.Error("Rất xin lỗi nhưng dung lượng file quá lớn, tối đa là 20Mb!");
                    _logger.LogError("Exception file too large: " + e.Message);
                    return Json(e.Message);
                }

                var chatGroup = _chatService.GetChatGroup(groupId);
                var user = _userService.GetById(userId);
                if (chatGroup == null || user == null)
                {
                    return Json(false);
                }

                var connectionIds = _userService.GetListConnectionIds(groupId);
                string path = _userService.SaveFile(file, "\\uploads\\").Result;
                var chatLog = new ChatLogViewModel()
                {
                    SenderId = userId,
                    ChatGroupId = groupId,
                    TimeSent = DateTime.Now,
                    AvatarLink = user.AvatarLink,
                    UserName = user.Username,
                    AttachedFile = path,
                    TypeFile = _fileStorage.GetTypeFile(path),
                    FileName = _fileStorage.GetFileName(path)
                };

                bool addResult = _chatService.AddChatLog(chatLog);
                if (addResult)
                {
                    var noti = new NewNotificationViewModel()
                    {
                        SenderId = userId,
                        ReceiverGroupId = groupId,
                        TypeNotify = TypeNotification.Chat
                    };
                    _notiService.AddNotiForNewChatLog(noti);
                }
                _hubContext.Clients.Clients(connectionIds).SendAsync("SendNewFile", chatLog);
                return Json(true);
            }
            catch (Exception e)
            {
                _notyf.Error("Đã có lỗi xảy ra, vui lòng thử lại sau.");
                _logger.LogError("Exception send file: " + e.Message);
                return Json(e.Message);
            }
        }

        [HttpGet]
        public IActionResult LoadMessage(Guid groupId, int pageIndex)
        {
            var messages = GetChatLogs(groupId, pageIndex);
            if (messages != null)
                return Json(messages);

            return Json(string.Empty);
        }

        public async Task<IActionResult> Download(string filePath)
        {
            string directory = Directory.GetCurrentDirectory();
            var fullPath = string.Format("{0}{1}{2}", directory, "\\wwwroot", filePath);
            if (!System.IO.File.Exists(fullPath))
            {
                return Content("File not found!");
            }

            var memory = new MemoryStream();
            using (var stream = new FileStream(fullPath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            var value = File(memory, GetContentType(fullPath), Path.GetFileName(fullPath));
            return value;
        }

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }

    }
}
