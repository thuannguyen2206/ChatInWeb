using System;
using System.Collections.Generic;
using System.Text;

namespace WebChat.Common.ViewModels.Discussion
{
    public class NewChatViewModel
    {
        public int Id { get; set; }

        public string NameGroup { get; set; }

        public Guid OwnerId { get; set; }

        public List<Guid> ListMembers { get; set; }

    }
}
