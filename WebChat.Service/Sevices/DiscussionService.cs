using System.Linq;
using System.Collections.Generic;
using WebChat.Common.ViewModels.Discussion;
using WebChat.DataAccess.EF;
using WebChat.Service.IServices;
using System;
using WebChat.Entities.Model;
using WebChat.Common.Utilities.Constants;
using WebChat.Common.ViewModels.User;
using System.Threading.Tasks;

namespace WebChat.Service.Sevices
{
    public class DiscussionService : IDiscussionService
    {
        private readonly WebChatDbContext _context;
        private readonly INotificationService _notificationService;
        private readonly IUserService _userService;

        public DiscussionService(WebChatDbContext context, INotificationService notificationService, IUserService userService)
        {
            _context = context;
            _notificationService = notificationService;
            _userService = userService;
        }

        // create a chat group and add list users to that group
        public Guid CreateNewGroup(NewChatViewModel model)
        {
            var groupId = CreateChatGroup(model);
            if (groupId != Guid.Empty)
            {
                foreach (var item in model.ListMembers)
                {
                    var entity = new UserInChatGroup()
                    {
                        GroupId = groupId,
                        UserId = item
                    };
                    _context.UserInChatGroups.Add(entity);
                }
                _context.SaveChanges();
                return groupId;
            }
            return Guid.Empty;
        }

        // get list chat group
        // iterate through groups to get the last chat log
        // from that chat log get the sent user
        // get list of notifications from group id
        public List<DiscussionViewModel> GetListDiscussions(Guid userId, string keyword, int pageIndex)
        {
            var queryGroup = _context.ChatGroups.AsQueryable();
            var queryUserInGroup = _context.UserInChatGroups.AsQueryable();
            var queryChatLog = _context.ChatLogs.AsQueryable();
            var queryUser = _context.Users.AsQueryable();
            var queryContact = _context.Contacts.AsQueryable();

            var query = (from a in queryGroup
                         join b in queryUserInGroup on a.Id equals b.GroupId
                         where b.UserId == userId
                         select new { a.Name, a.CreatedAt, a.AvatarGroup, b.GroupId, a.OwnerId }).ToList();

            var models = new List<DiscussionViewModel>();
            int totalUser;
            foreach (var item in query)
            {
                var model = new DiscussionViewModel();
                model.Notification = _notificationService.TotalUnread(userId, item.GroupId);
                model.GroupId = item.GroupId;
                totalUser = GetTotalUserInGroup(item.GroupId);
                if (totalUser == 2 && item.OwnerId == null)
                {
                    var anotherUserId = queryUserInGroup.Where(x => x.GroupId == item.GroupId && x.UserId != userId).Select(y => y.UserId).FirstOrDefault();
                    var user = queryUser.Where(x => x.Id == anotherUserId).Select(y => new { y.UserName, y.AvatarLink }).FirstOrDefault();
                    model.AvatarDiscussion = user.AvatarLink;
                    model.NameDiscussion = user.UserName;
                    var isBlock = queryContact.Where(x => x.UserId == userId && x.FriendId == anotherUserId).Select(x => x.IsBlock).FirstOrDefault();
                    if (isBlock) // Nếu true thì anotherUserId đã block userId
                        continue;
                }
                else
                {
                    model.AvatarDiscussion = item.AvatarGroup;
                    model.NameDiscussion = item.Name;
                }

                var chatLog = queryChatLog.Where(x => x.ChatGroupId == item.GroupId).OrderByDescending(y => y.CreatedAt).FirstOrDefault();
                if (chatLog != null)
                {
                    model.LastMessage = chatLog.Content != null ? chatLog.Content : (chatLog.AttachedFiles != null ? "Có 1 file." : "Chưa có tin nhắn nào.");
                    model.TimeSended = chatLog.CreatedAt;
                }
                else
                {
                    model.LastMessage = "Chưa có tin nhắn nào.";
                    model.TimeSended = item.CreatedAt.Value;
                }

                if (!string.IsNullOrEmpty(keyword))
                {
                    keyword = keyword.Trim().ToLower();
                    if (model.NameDiscussion.ToLower().Contains(keyword))
                        models.Add(model);
                }
                else
                {
                    models.Add(model);
                }
            }
            var result = models.OrderByDescending(x => x.TimeSended)
                         .Skip((pageIndex - 1) * SystemConstant.PageSize)
                         .Take(SystemConstant.PageSize).ToList();
            return result;
        }

        public int GetTotalUserInGroup(Guid groupId)
        {
            int count = _context.UserInChatGroups.Where(x => x.GroupId == groupId).Count();
            return count;
        }

        private Guid CreateChatGroup(NewChatViewModel data)
        {
            try
            {
                var entity = new ChatGroup();
                entity.CreatedAt = DateTime.Now;
                entity.AvatarGroup = SystemConstant.DefaultGroupAvatar; // set default avatar
                entity.Name = string.IsNullOrEmpty(data.NameGroup) ? SystemConstant.DefaultNameGroup : data.NameGroup;
                entity.OwnerId = data.OwnerId;

                _context.ChatGroups.Add(entity);
                _context.SaveChanges();
                return entity.Id;
            }
            catch (Exception)
            {
                return Guid.Empty;
            }

        }

        public DiscussionDetailViewModel GetDiscussionDetail(Guid groupId, Guid userId, int pageIndex = 1)
        {
            var groupDetail = _context.ChatGroups.FirstOrDefault(x => x.Id == groupId);
            var listContact = GetListContact(userId);
            listContact.Add(userId);

            var queryListMembers = (from a in _context.Users
                                    join b in _context.UserInChatGroups on a.Id equals b.UserId
                                    where b.GroupId == groupId
                                    select new UserForGroupDetailViewModel()
                                    {
                                        Id = a.Id,
                                        AvatarLink = a.AvatarLink,
                                        Username = a.UserName,
                                        Address = !string.IsNullOrEmpty(a.Address) ? a.Address : string.Empty,
                                        IsFriend = listContact.Contains(a.Id) ? true : false //nếu user id có trong list contact thì là true, ngược lại là false
                                    }).OrderBy(x => x.Username)
                                    .Skip((pageIndex - 1) * SystemConstant.PageSize)
                                    .Take(SystemConstant.PageSize).ToList();

            var model = new DiscussionDetailViewModel()
            {
                GroupId = groupId,
                GroupAvatar = groupDetail.AvatarGroup,
                GroupName = groupDetail.Name,
                ListMembers = queryListMembers,
                DateCreated = groupDetail.CreatedAt.Value,
                TotalMember = GetTotalUserInGroup(groupId),
                OwnerId = groupDetail.OwnerId,
                UserId = userId
            };
            return model;
        }

        public async Task<bool> UpdateDiscussion(DiscussionDetailViewModel model)
        {
            var group = _context.ChatGroups.Where(x => x.Id == model.GroupId).FirstOrDefault();
            if (group == null)
            {
                return false;
            }

            group.Name = model.GroupName;
            group.LastModifyAt = DateTime.Now;
            if (model.AvatarFile != null)
            {
                group.AvatarGroup = await _userService.SaveFile(model.AvatarFile, "\\img\\");
            }
            if (model.ListInviteMemberIds != null && model.ListInviteMemberIds.Count > 0)
            {
                AddMemberToGroup(group.Id, model.ListInviteMemberIds);
            }
            _context.ChatGroups.Update(group);
            _context.SaveChanges();
            return true;
        }

        private void AddMemberToGroup(Guid groupId, List<Guid> listMembers)
        {
            try
            {
                var listEntities = new List<UserInChatGroup>();
                foreach (var item in listMembers)
                {
                    var entity = new UserInChatGroup()
                    {
                        UserId = item,
                        GroupId = groupId
                    };
                    listEntities.Add(entity);
                }
                _context.UserInChatGroups.AddRange(listEntities);
                _context.SaveChanges();
            }
            catch (Exception) { }
        }

        public LoadMemberViewModel LoadMembers(Guid groupId, Guid userId, int pageIndex = 1)
        {
            var groupDetail = _context.ChatGroups.FirstOrDefault(x => x.Id == groupId);
            var listContact = GetListContact(userId);
            listContact.Add(userId);

            var queryListMembers = (from a in _context.Users
                                    join b in _context.UserInChatGroups on a.Id equals b.UserId
                                    where b.GroupId == groupId
                                    select new UserForGroupDetailViewModel()
                                    {
                                        Id = a.Id,
                                        AvatarLink = a.AvatarLink,
                                        Username = a.UserName,
                                        Address = !string.IsNullOrEmpty(a.Address) ? a.Address : string.Empty,
                                        IsFriend = listContact.Contains(a.Id) ? true : false
                                    }).OrderBy(x => x.Username)
                                    .Skip((pageIndex - 1) * SystemConstant.PageSize)
                                    .Take(SystemConstant.PageSize).ToList();

            var model = new LoadMemberViewModel()
            {
                GroupId = groupId,
                ListMembers = queryListMembers,
                OwnerId = groupDetail.OwnerId,
                UserId = userId
            };
            return model;
        }

        public bool RemoveMember(Guid groupId, Guid memberId)
        {
            var find = _context.UserInChatGroups.Where(x => x.GroupId == groupId && x.UserId == memberId).FirstOrDefault();
            if (find == null)
            {
                return false;
            }
            _context.UserInChatGroups.Remove(find);
            _context.SaveChanges();
            return true;
        }

        private List<Guid> GetListContact(Guid userId)
        {
            var listContact = _context.Contacts.Where(x => x.UserId == userId).Select(x => x.FriendId).ToList();
            return listContact;
        }

        // delete all members of the group
        // delete all group calls
        // delete all group messages
        // delete group
        public bool RemoveDiscussion(Guid groupId)
        {
            try
            {
                var uicg = _context.UserInChatGroups.Where(x => x.GroupId == groupId).ToList();
                var callUser = _context.CallUsers.Where(x => x.GroupId == groupId).ToList();
                var chatlogs = _context.ChatLogs.Where(x => x.ChatGroupId == groupId).ToList();
                var group = _context.ChatGroups.Where(x => x.Id == groupId).FirstOrDefault();
                if (uicg != null)
                {
                    _context.UserInChatGroups.RemoveRange(uicg);
                }
                if (callUser != null)
                {
                    _context.CallUsers.RemoveRange(callUser);
                }
                if (chatlogs != null)
                {
                    _context.ChatLogs.RemoveRange(chatlogs);
                }
                if (group != null)
                {
                    _context.ChatGroups.Remove(group);
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
