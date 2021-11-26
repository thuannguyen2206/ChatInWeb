using System;
using System.Collections.Generic;
using System.Text;

namespace WebChat.Common.ViewModels.User
{
    public class ContactViewModel
    {
        public Guid ContactId { get; set; }

        public string AvatarLink { get; set; }

        public string Address { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public bool Status { get; set; }

        public DateTime CreateAt { get; set; }
    }
}
