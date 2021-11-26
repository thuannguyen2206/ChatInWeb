﻿namespace WebChat.Entities.Model
{
    using System;
    using Microsoft.AspNetCore.Identity;

    public class UserRole : IdentityUserRole<Guid>
    {

        public virtual User User { get; set; }

        public virtual Role Role { get; set; }
    }
}
