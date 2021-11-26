using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebChat.Common.ViewModels.Chat
{
    public class ChatLogViewModel
    {
        public int Id { get; set; }

        public DateTime TimeSent { get; set; }

        public Guid SenderId { get; set; }

        public Guid ChatGroupId { get; set; }

        public string Content { get; set; }

        public string AttachedFile { get; set; }

        public int TypeFile { get; set; }

        public string FileName { get; set; }

        public int? CallId { get; set; } //id của cuộc gọi

        public string TimeSentAsString
        {
            get
            {
                var dateNow = DateTime.Now;
                if (dateNow.Year == this.TimeSent.Year)
                {
                    if (dateNow.Date == this.TimeSent.Date)
                    {
                        return this.TimeSent.ToString("HH:mm");
                    }

                    return this.TimeSent.ToString("dd/MM HH:mm");
                }

                return this.TimeSent.ToString("dd/MM/yyyy HH:mm");
            }
        }

        public string AvatarLink { get; set; }

        public string UserName { get; set; }

    }
}
