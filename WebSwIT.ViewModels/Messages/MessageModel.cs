using System;
using System.Collections.Generic;

namespace WebSwIT.ViewModels.Messages
{
    public class MessageModel
    {
        public string Id { get; set; }
        public Guid AuthorUserId { get; set; }
        public Guid ReceiverUserId { get; set; }
        public string Content { get; set; }
        public DateTime LastUpdate { get; set; }
        public List<MessageStateModel> States { get; set; }
    }
}
