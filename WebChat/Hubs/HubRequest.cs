using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebChat.WebApp.Hubs
{
    public class HubRequest<T>
    {
        public Guid UserId { get; set; }

        public string ConnectionId { get; set; }

        public T Data { get; set; }
    }

    public class ChatData
    {
        public Guid ChatGroupId { get; set; }

        public string Message { get; set; }

    }

}
