using System;
using System.Collections.Generic;
using System.Text;

namespace WebChat.Common.ViewModels.User
{
    public class ChangePasswordViewModel
    {
        public Guid Id { get; set; } // Id user

        public string PresentPassword { get; set; }

        public string NewPassword { get; set; }

        public string ConfirmNewPassword { get; set; }
    }
}
