using System;
using System.Collections.Generic;
using System.Linq;
using WebChat.Common.Utilities.Constants;
using WebChat.Common.Utilities.Enums;
using WebChat.Common.ViewModels.Notification;
using WebChat.DataAccess.EF;
using WebChat.Entities.Model;
using WebChat.Service.IServices;

namespace WebChat.Service.Sevices
{
    public class NotificationService : INotificationService
    {
        private readonly WebChatDbContext _context;
        private readonly IUserService _userService;

        public NotificationService(WebChatDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        /// <summary>
        /// Noti yêu cầu thêm contact
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public NewContactNotificationModel AddNotificationFromUser(NewNotificationViewModel model)
        {
            try
            {
                var checkId = IsExistNotiContact(model.SenderId, model.ReceiverId);
                if (checkId > 0)
                {
                    return new NewContactNotificationModel() { Result = NewNotifResult.Exist };
                }

                var noti = new Notification()
                {
                    Content = model.ContentNotify,
                    CreatedAt = DateTime.Now,
                    SenderId = model.SenderId,
                    TypeNotification=TypeNotification.NewContact
                };
                var notiId = CreateNotification(noti);
                var notiUser = new NotificationUser()
                {
                    CreatedAt = DateTime.Now,
                    ReceiverUserId = model.ReceiverId,
                    NotificationId = notiId
                };
                var notiUserId = CreateNotificationUser(notiUser);
                return new NewContactNotificationModel() { Result = NewNotifResult.Success, NotificationId = notiId };
            }
            catch (Exception)
            {
                return new NewContactNotificationModel() { Result = NewNotifResult.Error };
            }
        }

        /// <summary>
        /// Dựa vào id group để tìm id user của group ngoại trừ user gửi tin nhắn
        /// thêm vào Notification 1 noti
        /// thêm vào NotificationUser số record bằng với số id user của group (type của noti là 1: chat)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddNotiForNewChatLog(NewNotificationViewModel model)
        {
            try
            {
                var listUserIds = _context.UserInChatGroups.Where(x => x.GroupId == model.ReceiverGroupId && x.UserId != model.SenderId)
                    .Select(x => x.UserId).ToList();

                var noti = new Notification()
                {
                    Content = model.ContentNotify,
                    CreatedAt = DateTime.Now,
                    SenderId = model.SenderId,
                    ReceiverGroupId = model.ReceiverGroupId,
                    TypeNotification=TypeNotification.Chat
                };
                var notiId = CreateNotification(noti);

                var listNotiUsers = new List<NotificationUser>();
                foreach (var receiverId in listUserIds)
                {
                    var notiUser = new NotificationUser()
                    {
                        CreatedAt = DateTime.Now,
                        ReceiverUserId = receiverId,
                        NotificationId = notiId,
                        IsRead = false
                    };
                    listNotiUsers.Add(notiUser);
                }
                _context.NotificationUsers.AddRange(listNotiUsers);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Kiểm tra xem user muốn kết bạn có gửi yêu cầu kết bạn tới mình chưa
        /// => nếu chưa thì gửi kết bạn
        /// => nếu rồi thì add contact và xóa noti đến từ contact
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="contactId"></param>
        /// <returns></returns>
        public bool CheckNewContactRequest(Guid userId, Guid contactId)
        {
            var notiId = IsExistNotiContact(contactId, userId);
            if (notiId > 0)
            {
                var addContact = _userService.AcceptNewContactRequest(userId, contactId);
                if (addContact == NewContactResult.Successed)
                {
                    DeleteNotification(notiId);
                    return true;
                }
            }
            return false;
        }

        public bool DeleteNotification(int notiId)
        {
            var noti = _context.Notifications.Where(x => x.Id == notiId).FirstOrDefault();
            var notiUser = _context.NotificationUsers.Where(x => x.NotificationId == notiId).FirstOrDefault();
            if (noti != null && notiUser != null)
            {
                _context.NotificationUsers.Remove(notiUser);
                _context.Notifications.Remove(noti);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public List<NotificationViewModel> GetListNotifications(Guid userId, string keyword, int pageIndex)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == userId);
            if (user == null)
            {
                return null;
            }

            var queryNoti = _context.Notifications.Where(x => x.TypeNotification != TypeNotification.Chat).AsQueryable();
            var queryNotiUser = _context.NotificationUsers.Where(x => x.ReceiverUserId == user.Id).AsQueryable();
            var queryUser = _context.Users.Select(x => new { x.Id, x.AvatarLink, x.UserName }).AsQueryable();
            if (!string.IsNullOrEmpty(keyword))
            {
                keyword = keyword.Trim().ToLower();
                queryNoti = queryNoti.Where(x => x.Content.ToLower().Contains(keyword));
            }
            var queryResult = (from a in queryNoti
                               join b in queryNotiUser on a.Id equals b.NotificationId
                               join c in queryUser on a.SenderId equals c.Id
                               select new NotificationViewModel()
                               {
                                   AvatarLink = c.AvatarLink,
                                   UserName = c.UserName,
                                   SenderId = a.SenderId.Value,
                                   TimeSend = a.CreatedAt.Value,
                                   ContentNotify = a.Content,
                                   TypeNotify=a.TypeNotification,
                                   NotificationId = a.Id
                               }).OrderByDescending(x => x.TimeSend)
                               .Skip((pageIndex - 1) * SystemConstant.PageSize)
                               .Take(SystemConstant.PageSize).ToList();

            return queryResult;
        }

        // Dựa vào groupid trong Notification và userId trong NotificationUser để tìm tất cả noti của user hiện tại
        // Nếu số lượng user đã đọc noti còn lại 1 (user hiện tại) thì xóa NotificationUser sau đó xóa luôn Notification
        // Ngược lại, còn nhiều hơn 1 người chưa đọc thì chỉ xóa tất cả noti gửi tới user hiện tại trong NotificationUser
        public bool DeleteWhenRead(Guid userId, Guid groupId)
        {
            var noti = _context.Notifications.Where(x => x.ReceiverGroupId == groupId && x.TypeNotification == TypeNotification.Chat).AsQueryable();
            var notiUser = _context.NotificationUsers.Where(x => x.ReceiverUserId == userId && x.IsRead == false ).AsQueryable();
            var totalNotiIds = (from a in noti
                                join b in notiUser on a.Id equals b.NotificationId
                                select new { b.Id, b.NotificationId }).ToList();

            if (totalNotiIds == null)
            {
                return false;
            }

            var listNotis = new List<Notification>();
            var listNotiUsers = new List<NotificationUser>();
            var query = _context.NotificationUsers.AsQueryable();
            foreach (var item in totalNotiIds)
            {
                var entityNotiUser = query.FirstOrDefault(x => x.Id == item.Id);
                listNotiUsers.Add(entityNotiUser);
                var count = query.Where(x => x.NotificationId == item.NotificationId);
                if (count.Count() == 1)
                {
                    var entityNoti = noti.FirstOrDefault(x => x.Id == item.NotificationId);
                    listNotis.Add(entityNoti);
                }
            }
            _context.NotificationUsers.RemoveRange(listNotiUsers);
            if (listNotis != null)
            {
                _context.Notifications.RemoveRange(listNotis);
            }
            _context.SaveChanges();
            return true;
        }

        private int IsExistNotiContact(Guid senderId, Guid receiverId)
        {
            var result = (from a in _context.Notifications
                          join b in _context.NotificationUsers on a.Id equals b.NotificationId
                          where a.SenderId == senderId && b.ReceiverUserId == receiverId && a.TypeNotification == TypeNotification.NewContact
                          select a.Id).FirstOrDefault();
            return result;
        }

        private int CreateNotification(Notification entity)
        {
            _context.Notifications.Add(entity);
            _context.SaveChanges();
            return entity.Id;
        }

        private int CreateNotificationUser(NotificationUser entity)
        {
            _context.NotificationUsers.Add(entity);
            _context.SaveChanges();
            return entity.Id;
        }

        public int TotalUnread(Guid userId, Guid groupId)
        {
            var noti = _context.Notifications.Where(x => x.ReceiverGroupId == groupId && x.TypeNotification == TypeNotification.Chat).AsQueryable();
            var notiUser = _context.NotificationUsers.Where(x => x.ReceiverUserId == userId && x.IsRead == false).AsQueryable();
            var totalUnread = (from a in noti
                               join b in notiUser on a.Id equals b.NotificationId
                               select a.Id).Count();

            return totalUnread;
        }

        /// <summary>
        /// Cập nhật noti thành thông báo hệ thống (type = 1) khi đồng ý kết bạn
        /// và cả nội dung của thông báo đó 
        /// </summary>
        /// <param name="notiId"></param>
        /// <returns></returns>
        public bool UpdateWhenAcceptContact(int notiId, string content = null)
        {
            var noti = _context.Notifications.FirstOrDefault(x => x.Id == notiId);
            var recieveUserId = _context.NotificationUsers.Where(x => x.NotificationId == notiId).Select(x=>x.ReceiverUserId).FirstOrDefault();
            if (noti != null && recieveUserId != null)
            {
                var senderRequest = _context.Users.Where(x => x.Id == recieveUserId).Select(x => x.UserName).FirstOrDefault();
                noti.Content = !string.IsNullOrEmpty(content) ? content : string.Format("Đã đồng ý kết bạn với {0}", senderRequest);
                noti.TypeNotification = TypeNotification.System;
                _context.Notifications.Update(noti);

                _context.SaveChanges();
                return true;
            }
            return false;
        }

    }
}
