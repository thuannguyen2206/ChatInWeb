using System;
using System.Collections.Generic;
using System.Text;

namespace WebChat.Entities.Model
{
    public class Contact : BaseModel
    {
        public Guid UserId { get; set; }

        public Guid FriendId { get; set; }

        public bool IsBlock { get; set; } // user có bị friend block hay không


        public User User { get; set; }

    }
}
