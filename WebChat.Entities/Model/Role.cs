using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebChat.Entities.Model
{
    public class Role : IdentityRole<Guid>
    {
        public string Description { get; set; }

        /// <summary>
        /// Hoạt động (true) - Khóa (false)
        /// </summary>
        public bool Status { get; set; }

        public List<RoleClaim> RoleClaims { get; set; }

        public List<UserRole> UserRoles { get; set; }
    }
}
