namespace WebChat.Entities.Model
{
    using System;
    using Microsoft.AspNetCore.Identity;

    public class UserClaim : IdentityUserClaim<Guid>
    {
        public User User { get; set; }
    }
}
