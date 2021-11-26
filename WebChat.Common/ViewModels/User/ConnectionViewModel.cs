using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebChat.Common.ViewModels.User
{
    public class ConnectionViewModel
    {
        public int Id { get; set; }

        public Guid UserId { get; set; }

        public string ConnectionId { get; set; }

        public string IPAddress { get; set; }

        public DateTime? CreatedAt { get; set; }
    }
}
