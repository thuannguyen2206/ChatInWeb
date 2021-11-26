using System;
using System.Collections.Generic;
using System.Text;

namespace WebChat.Common.ViewModels.Account
{
    public class RegisterViewModel
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string PasswordConfirm { get; set; }

        public string Email { get; set; }

        public string AvatarLink { get; set; }
    }
}
