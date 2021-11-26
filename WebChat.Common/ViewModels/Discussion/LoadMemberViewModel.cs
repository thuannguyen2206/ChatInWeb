using System;
using System.Collections.Generic;
using System.Text;
using WebChat.Common.ViewModels.User;

namespace WebChat.Common.ViewModels.Discussion
{
    public class LoadMemberViewModel
    {
        public Guid GroupId { get; set; }

        public Guid? OwnerId { get; set; }

        public Guid UserId { get; set; }

        public List<UserForGroupDetailViewModel> ListMembers { get; set; }
    }
}
