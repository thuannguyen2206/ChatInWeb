using System;
using System.Collections.Generic;
using System.Text;
using WebChat.Common.Utilities.Enums;

namespace WebChat.Entities.Model
{
    public class CallUser : BaseModel
    {
        public DateTime? TimeStart { get; set; }

        public DateTime? TimeStop { get; set; }

        public Guid SenderCallId { get; set; } //người gọi

        public Guid GroupId { get; set; } //nhóm được gọi

        public bool Status { get; set; } //gọi thành công (true) - gọi nhỡ (false)

        public CallType CallType { get; set; }

        public ChatGroup ChatGroup { get; set; }

        public User User { get; set; }

    }
}
