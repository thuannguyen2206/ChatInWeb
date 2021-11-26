using System;
using System.Collections.Generic;
using System.Text;

namespace WebChat.Common.ViewModels.Discussion
{
    public class DiscussionViewModel
    {
        public Guid GroupId { get; set; }

        public string AvatarDiscussion { get; set; }

        public string NameDiscussion { get; set; }

        public string ShortNameDiscussion
        {
            get
            {
                if (!string.IsNullOrEmpty(this.NameDiscussion) && this.NameDiscussion.Length > 15)
                {
                    return string.Format("{0}...", this.NameDiscussion.Substring(0, 15));
                }

                return this.NameDiscussion;
            }
        }

        public string LastMessage { get; set; }

        public string ShortMessage
        {
            get
            {
                if (!string.IsNullOrEmpty(this.LastMessage) && this.LastMessage.Length > 50)
                {
                    return string.Format("{0}...", this.LastMessage.Substring(0, 50));
                }

                return this.LastMessage;
            }
        }

        public DateTime? TimeSended { get; set; }

        public string TimeSentAsString
        {
            get
            {
                var dateNow = DateTime.Now;
                if (dateNow.Year == this.TimeSended.Value.Year)
                {
                    if (dateNow.Date == this.TimeSended.Value.Date)
                    {
                        return this.TimeSended.Value.ToString("HH:mm");
                    }

                    return this.TimeSended.Value.ToString("dd/MM HH:mm");
                }

                return this.TimeSended.Value.ToString("dd/MM/yyyy");
            }
        }

        public int Notification { get; set; }

        public string NotificationAsString {
            get
            {
                if (this.Notification > 999)
                {
                    return "+999";
                }

                return this.Notification.ToString();
            }
        }

        public bool Status { get; set; }
    }
}
