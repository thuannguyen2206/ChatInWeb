namespace WebChat.Entities.Model
{
    using System;
    using Microsoft.AspNetCore.Identity;

    public class UserLogin : IdentityUserLogin<Guid>
    {
        public User User { get; set; }
    }
}
