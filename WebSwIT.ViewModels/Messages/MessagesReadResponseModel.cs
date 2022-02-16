using System;
using System.Collections.Generic;

namespace WebSwIT.ViewModels.Messages
{
    public class MessagesReadResponseModel
    {
        public List<string> MessagesIds { get; set; }
        public Guid ReaderUserId { get; set; }
        public int UnreadCount { get; set; }
    }
}
