using System;
using System.Collections.Generic;
using WebChat.Common.Utilities.Enums;
using WebChat.Common.ViewModels.Notification;

namespace WebChat.Service.IServices
{
    public interface INotificationService
    {
        /// <summary>
        /// Get list notifications from user id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="keyword"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        List<NotificationViewModel> GetListNotifications(Guid userId, string keyword = null, int pageIndex = 1);

        /// <summary>
        /// Add notification to user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        NewContactNotificationModel AddNotificationFromUser(NewNotificationViewModel model);

        /// <summary>
        /// Delete notification add new contact 
        /// </summary>
        /// <param name="notiId"></param>
        /// <returns></returns>
        bool DeleteNotification(int notiId);

        /// <summary>
        /// Check exist notification add new contact
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="contactId"></param>
        /// <returns></returns>
        bool CheckNewContactRequest(Guid userId, Guid contactId);

        /// <summary>
        /// Add notification for new message
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool AddNotiForNewChatLog(NewNotificationViewModel model);

        /// <summary>
        /// Delete notification when user readed message
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        bool DeleteWhenRead(Guid userId, Guid groupId);

        /// <summary>
        /// Count total message unread in chat group
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        int TotalUnread(Guid userId, Guid groupId);

        /// <summary>
        /// Update type notification when user accept new contact
        /// </summary>
        /// <param name="notiId"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        bool UpdateWhenAcceptContact(int notiId, string content = null);
    }
}
