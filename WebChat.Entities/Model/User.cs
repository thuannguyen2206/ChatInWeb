
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebChat.Entities.Model
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsLocked { get; set; }

        public string AvatarLink { get; set; }

        public DateTime DateCreate { get; set; }

        /// <summary>
        /// Online (true) - Offline (false)
        /// </summary>
        public bool Status { get; set; }

        public List<UserClaim> UserClaims { get; set; }

        public List<UserLogin> UserLogins { get; set; }

        public List<UserToken> UserTokens { get; set; }

        public List<UserRole> UserRoles { get; set; }


        public List<UserInChatGroup> UserInChatGroups { get; set; }

        public List<CallUser> CallUsers { get; set; }

        public List<ChatLog> ChatLogs { get; set; }

        public List<Contact> Contacts { get; set; }

    }
}
