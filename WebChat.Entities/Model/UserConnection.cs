namespace WebChat.Entities.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class UserConnection : BaseModel
    {
        public Guid UserId { get; set; }

        public string ConnectionId { get; set; }

        public string IPAddress { get; set; }

    }
}
