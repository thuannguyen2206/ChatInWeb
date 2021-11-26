using System;
using System.Collections.Generic;
using System.Text;
using WebChat.Common.Utilities.Enums;

namespace WebChat.Common.ViewModels.Notification
{
    public class NotificationViewModel
    {
        public int NotificationId { get; set; }

        public Guid SenderId { get; set; }

        public string AvatarLink { get; set; }

        public string ContentNotify { get; set; }

        public DateTime TimeSend { get; set; }

        public string TimeSendAsString
        {
            get
            {
                var dateNow = DateTime.Now;
                if (dateNow.Year == this.TimeSend.Year)
                {
                    if (dateNow.Date == this.TimeSend.Date)
                    {
                        return this.TimeSend.ToString("HH:mm");
                    }

                    return this.TimeSend.ToString("dd/MM HH:mm");
                }

                return this.TimeSend.ToString("dd/MM/yyyy HH:mm");
            }
        }

        public string UserName { get; set; }

        public TypeNotification TypeNotify { get; set; }
    }
}
