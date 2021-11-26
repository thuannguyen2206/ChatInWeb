using System;
using System.Collections.Generic;
using System.Text;

namespace WebChat.Common.ViewModels.Chat
{
    public class ChatGroupViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public bool Status { get; set; }

        public Guid? OwnerId { get; set; }

        public bool IsOwner { get; set; }

        public bool IsBlock { get; set; } // Are you block this user?

        public string AvatarLink { get; set; }

        public int TotalMember { get; set; }
    }
}
