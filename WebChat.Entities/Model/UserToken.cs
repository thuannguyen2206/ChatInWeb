namespace WebChat.Entities.Model
{
    using System;
    using Microsoft.AspNetCore.Identity;

    public class UserToken : IdentityUserToken<Guid>
    {
        public User User { get; set; }
    }
}
