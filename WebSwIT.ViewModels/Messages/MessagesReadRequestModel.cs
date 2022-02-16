using System;
using System.Collections.Generic;

namespace WebSwIT.ViewModels.Messages
{
    public class MessagesReadRequestModel
    {
        public List<string> MessagesIds { get; set; }
        public Guid AuthorUserId { get; set; }
    }
}
