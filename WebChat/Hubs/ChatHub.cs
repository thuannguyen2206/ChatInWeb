using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using WebChat.Common.Utilities.Constants;
using WebChat.Common.Utilities.Enums;
using WebChat.Common.ViewModels.Chat;
using WebChat.Common.ViewModels.Notification;
using WebChat.Common.ViewModels.User;
using WebChat.Service.IServices;

namespace WebChat.WebApp.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IUserService _userService;
        private readonly IChatService _chatService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly INotificationService _notiService;
        private readonly ILogger<ChatHub> _logger;

        public ChatHub(IUserService userService, IChatService chatService, 
            IHttpContextAccessor httpContextAccessor, INotificationService notiService,
            ILogger<ChatHub> logger)
        {
            _chatService = chatService;
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
            _notiService = notiService;
            _logger = logger;
        }

        public string InitialConnection()
        {
            try
            {
                string userId = _httpContextAccessor.HttpContext.Session.GetString(SystemConstant.UserId);
                var model = new ConnectionViewModel()
                {
                    UserId = Guid.Parse(userId),
                    ConnectionId = Context.ConnectionId,
                    IPAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString()
                };
                _userService.AddConnection(model);
                return model.ConnectionId;
            }
            catch (Exception e)
            {
                _logger.LogError("Exception initial connection: " + e.Message);
                return string.Empty;
            }
        }

        /// <summary>
        /// Gửi tin nhắn và noti
        /// Nếu thêm tin nhắn thành công thì tạo thông báo cho tin nhắn đó
        /// và gửi thông báo cùng tin nhắn tới các user khác
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task SendMessage(HubRequest<ChatData> request)
        {
            try
            {
                if (request.Data == null || string.IsNullOrEmpty(request.Data.Message))
                {
                    return this.Clients.Clients(string.Empty).SendAsync("SendNewMessage", string.Empty);
                }

                var chatGroup = _chatService.GetChatGroup(request.Data.ChatGroupId);
                if (chatGroup == null)
                {
                    return this.Clients.Clients(string.Empty).SendAsync("SendNewMessage", string.Empty);
                }

                var user = _userService.GetById(request.UserId);
                if (user == null)
                {
                    return this.Clients.Clients(string.Empty).SendAsync("SendNewMessage", string.Empty);
                }

                var connectionIds = _userService.GetListConnectionIds(request.Data.ChatGroupId);

                var chatLog = new ChatLogViewModel()
                {
                    SenderId = request.UserId,
                    ChatGroupId = request.Data.ChatGroupId,
                    Content = request.Data.Message,
                    TimeSent = DateTime.Now,
                    AvatarLink = user.AvatarLink,
                    UserName = user.Username
                };
                bool addResult = _chatService.AddChatLog(chatLog);
                if (addResult)
                {
                    var noti = new NewNotificationViewModel()
                    {
                        SenderId = request.UserId,
                        ReceiverGroupId = request.Data.ChatGroupId,
                        TypeNotify = TypeNotification.Chat
                    };
                    _notiService.AddNotiForNewChatLog(noti);
                }
                return this.Clients.Clients(connectionIds).SendAsync("SendNewMessage", chatLog);
            }
            catch (Exception e)
            {
                _logger.LogError("Exception send message: " + e.Message);
                return this.Clients.Clients(string.Empty).SendAsync("SendNewMessage", string.Empty);
            }
        }

    }                  
}
