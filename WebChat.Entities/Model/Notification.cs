using System;
using System.Collections.Generic;
using System.Text;
using WebChat.Common.Utilities.Enums;

namespace WebChat.Entities.Model
{
    public class Notification : BaseModel
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public Guid? SenderId { get; set; }

        public Guid? ReceiverGroupId { get; set; }

        public TypeNotification TypeNotification { get; set; }

        public List<NotificationUser> NotificationUsers { get; set; }

    }
}