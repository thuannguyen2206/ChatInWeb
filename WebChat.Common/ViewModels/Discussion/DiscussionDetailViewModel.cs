using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using WebChat.Common.ViewModels.User;

namespace WebChat.Common.ViewModels.Discussion
{
    public class DiscussionDetailViewModel : LoadMemberViewModel
    {
        public string GroupName { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? ModifyAt { get; set; }

        public string GroupAvatar { get; set; }

        public int TotalMember { get; set; }

        public IFormFile AvatarFile { get; set; }

        public List<Guid> ListInviteMemberIds { get; set; }

    }
}
