using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using WebSwIT.Entities.Models;

namespace WebSwIT.Entities.Entities
{
    public class Message
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public Guid AuthorUserId { get; set; }
        public Guid ReceiverUserId { get; set; }
        public string Content { get; set; }
        public DateTime LastUpdate { get; set; }
        public List<MessageState> States { get; set; }

        public Message()
        {
            States = new List<MessageState>();
        }
    }
}
