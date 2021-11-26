using System;
using System.Collections.Generic;
using System.Text;

namespace WebChat.Common.ViewModels.User
{
    public class UserForGroupDetailViewModel
    {
        public Guid Id { get; set; } // user id

        public string Username { get; set; }

        public string Address { get; set; }

        public string AvatarLink { get; set; }

        public bool IsFriend { get; set; }
    }
}
