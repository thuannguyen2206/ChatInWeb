using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebChat.Common.Utilities.Constants;
using WebChat.Common.Utilities.Storage;
using WebChat.Common.ViewModels.Chat;
using WebChat.DataAccess.EF;
using WebChat.Entities.Model;
using WebChat.Service.IServices;

namespace WebChat.Service.Sevices
{
    public class ChatService : IChatService
    {
        private readonly WebChatDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IFileStorage _fileStorage;
        private readonly IDiscussionService _discussionService;

        public ChatService(WebChatDbContext context, IFileStorage fileStorage,
        IHttpContextAccessor httpContextAccessor, IDiscussionService discussionService)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _fileStorage = fileStorage;
            _discussionService = discussionService;
        }

        // Get group by contact id
        public ChatGroupViewModel GetChatGroup(Guid userId, Guid contactId)
        {
            var contact = _context.Users.FirstOrDefault(x => x.Id == contactId);
            if (contact == null)
                return null;
            var checkGroup = CheckChatGroup(userId, contactId);
            var chatGroup = new ChatGroupViewModel();
            chatGroup.AvatarLink = contact.AvatarLink;
            chatGroup.Name = contact.UserName;
            if (checkGroup != null && checkGroup != Guid.Empty)
            {
                chatGroup.Id = checkGroup;
            }
            else
            {
                // If you don't have a chat group, create and add them to that chat group
                var newChatGr = CreateChatGroup();
                chatGroup.Id = newChatGr;
                var ListUser = new List<Guid>() { userId, contactId };

                foreach (var item in ListUser)
                {
                    var uicg = new UserInChatGroup()
                    {
                        GroupId = newChatGr,
                        UserId = item
                    };
                    _context.Add(uicg);
                }
                _context.SaveChanges();
            }
            chatGroup.TotalMember = _discussionService.GetTotalUserInGroup(chatGroup.Id);
            chatGroup.OwnerId = null;
            chatGroup.IsBlock = false;
            return chatGroup;
        }

        // Get group by group id
        public ChatGroupViewModel GetChatGroup(Guid groupId)
        {
            Guid userId = Guid.Parse(_httpContextAccessor.HttpContext.Session.GetString(SystemConstant.UserId));
            var group = _context.ChatGroups.FirstOrDefault(x => x.Id == groupId);
            if (group == null)
            {
                return null;
            }
            var queryUser = _context.Users.AsQueryable();
            var chatGroup = new ChatGroupViewModel()
            {
                Id = group.Id,
                TotalMember = _discussionService.GetTotalUserInGroup(groupId),
                IsOwner = group.OwnerId == userId ? true : false,
                OwnerId = group.OwnerId
            };

            if (chatGroup.TotalMember == 2 && group.OwnerId == null)
            {
                var anotherUserId = _context.UserInChatGroups.Where(x => x.GroupId == groupId && x.UserId != userId).Select(x => x.UserId).FirstOrDefault();
                var anotherUser = queryUser.Where(x => x.Id == anotherUserId).Select(x => new { x.UserName, x.AvatarLink }).FirstOrDefault();
                chatGroup.AvatarLink = anotherUser.AvatarLink;
                chatGroup.Name = anotherUser.UserName;
                chatGroup.IsBlock = _context.Contacts.Where(x => x.UserId == anotherUserId && x.FriendId == userId).Select(x => x.IsBlock).FirstOrDefault(); // true if userId was blocked anotherUserId, otherwise false
            }
            else
            {
                chatGroup.Name = group.Name;
                chatGroup.AvatarLink = group.AvatarGroup;
                chatGroup.IsBlock = false;
            }
            return chatGroup;
        }

        public List<ChatLogViewModel> ListChatLogs(Guid groupId, int pageIndex)
        {
            var queryChatlog = _context.ChatLogs.Where(x => x.ChatGroupId == groupId).AsQueryable();
            var queryUser = _context.Users.AsQueryable();

            var query = (from a in queryChatlog
                         join b in queryUser on a.SenderId equals b.Id
                         select new ChatLogViewModel()
                         {
                             AttachedFile = a.AttachedFiles,
                             CallId = a.CallId,
                             ChatGroupId = a.ChatGroupId,
                             Content = a.Content,
                             Id = a.Id,
                             SenderId = a.SenderId,
                             TimeSent = a.CreatedAt.Value,
                             AvatarLink = b.AvatarLink,
                             UserName = b.UserName,
                             FileName = a.AttachedFiles != null ? _fileStorage.GetFileName(a.AttachedFiles) : string.Empty,
                             TypeFile = a.AttachedFiles != null ? _fileStorage.GetTypeFile(a.AttachedFiles) : -1
                         }).OrderByDescending(x => x.TimeSent)
                        .Skip((pageIndex - 1) * SystemConstant.PageSize)
                        .Take(SystemConstant.PageSize).ToList();

            var result = query.OrderBy(x => x.TimeSent).ToList();
            return result;
        }

        public Guid CheckChatGroup(Guid userId, Guid contactId)
        {
            var listUserId = _context.UserInChatGroups.Where(x => x.UserId == userId).AsQueryable();
            var listGroupId = _context.UserInChatGroups.Where(x => x.UserId == contactId).AsQueryable();
            var result = (from a in listUserId
                          join b in listGroupId on a.GroupId equals b.GroupId
                          select a.GroupId).ToList();

            if (result != null)
            {
                foreach (var item in result)
                {
                    var totalMember = _discussionService.GetTotalUserInGroup(item);
                    if (totalMember == 2)
                        return item;
                };
            }
            return Guid.Empty;
        }

        public Guid CreateChatGroup(string nameGroup = null)
        {
            var chatGroup = new ChatGroup()
            {
                CreatedAt = DateTime.Now,
                Name = nameGroup
            };
            _context.ChatGroups.Add(chatGroup);
            _context.SaveChanges();
            return chatGroup.Id;
        }

        public bool AddChatLog(ChatLogViewModel model)
        {
            try
            {
                var entity = new ChatLog()
                {
                    ChatGroupId = model.ChatGroupId,
                    Content = model.Content,
                    CreatedAt = model.TimeSent,
                    SenderId = model.SenderId,
                    AttachedFiles = model.AttachedFile
                };
                _context.ChatLogs.Add(entity);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool AddUsersToGroup(Guid groupId, List<Guid> listId)
        {
            try
            {
                foreach (var uicg in listId)
                {
                    var entity = new UserInChatGroup()
                    {
                        GroupId = groupId,
                        UserId = uicg
                    };
                    _context.UserInChatGroups.Add(entity);
                }
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

    }
}
