using System;
using System.Collections.Generic;
using System.Text;

namespace WebChat.Entities.Model
{
    public class ChatLog : BaseModel
    {
        public Guid SenderId { get; set; }

        public Guid ChatGroupId { get; set; }

        public string Content { get; set; }

        public string AttachedFiles { get; set; }

        public int? CallId { get; set; } //id của cuộc gọi


        //foreign key
        public ChatGroup ChatGroup { get; set; }

        public User User { get; set; }
    
    }
}
