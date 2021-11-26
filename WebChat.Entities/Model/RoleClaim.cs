namespace WebChat.Entities.Model
{
    using System;
    using Microsoft.AspNetCore.Identity;

    public class RoleClaim : IdentityRoleClaim<Guid>
    {
        public Role Role { get; set; }
    }
}
