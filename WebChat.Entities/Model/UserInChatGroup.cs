using System;
using System.Collections.Generic;
using System.Text;

namespace WebChat.Entities.Model
{
    public class UserInChatGroup
    {
        public Guid UserId { get; set; }

        public User User { get; set; }


        public Guid GroupId { get; set; }

        public ChatGroup ChatGroup { get; set; }
    }
}
