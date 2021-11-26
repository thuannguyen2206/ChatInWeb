using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebChat.Common.ViewModels.User
{
    public class UserViewModel
    {
        public Guid Id { get; set; } // user id

        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string AvatarLink { get; set; }

        public IFormFile File { get; set; }
    }
}
