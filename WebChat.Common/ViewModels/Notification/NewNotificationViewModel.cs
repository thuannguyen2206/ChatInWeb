using System;
using System.Collections.Generic;
using System.Text;
using WebChat.Common.Utilities.Enums;

namespace WebChat.Common.ViewModels.Notification
{
    public class NewNotificationViewModel
    {
        public int NotiId { get; set; }

        public Guid SenderId { get; set; }

        public Guid ReceiverId { get; set; }

        public Guid ReceiverGroupId { get; set; }

        public string ContentNotify { get; set; }

        public TypeNotification TypeNotify { get; set; }

        public int TotalUnread { get; set; }
    }
}
