using System;

namespace WebChat.Entities.Model
{
    public class NotificationUser : BaseModel
    {
        public int? NotificationId { get; set; }

        public Guid? ReceiverUserId { get; set; }

        public bool IsRead { get; set; }

        public DateTime? TimeRead { get; set; }

        //foreign key
        public Notification Notification { get; set; }
    }
}
