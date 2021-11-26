using System;
using System.Collections.Generic;
using System.Text;

namespace WebChat.Entities.Model
{
    public class ChatGroup
    {
        public Guid Id { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? LastModifyAt { get; set; }

        public string Name { get; set; }

        public string AvatarGroup { get; set; }

        public Guid? OwnerId { get; set; }

        public List<UserInChatGroup> UserInChatGroups { get; set; }

        public List<ChatLog> ChatLogs { get; set; }

        public List<CallUser> CallUsers { get; set; }
    }
}
