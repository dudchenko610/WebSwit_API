using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSwIT.DataAccessLayer.Interfaces.Repository;
using WebSwIT.Entities.Entities;
using WebSwIT.Shared.Models.ListPagination;

namespace WebSwIT.DataAccessLayer.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly IMongoDatabase _mongoDatabase;

        public MessageRepository(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
        }

        public async Task CreateAsync(Message message, string collection)
        {
            var messagesCollection = _mongoDatabase.GetCollection<Message>(collection);
            await messagesCollection.InsertOneAsync(message);
        }

        public async Task<PagedListResponseModel<Message>> GetFilteredAsync(PaginationListFilterModel paginationModel, string collection)
        {
            var messagesCollection = _mongoDatabase.GetCollection<Message>(collection);

            var messagesFilter = string.IsNullOrEmpty(paginationModel.LastId) ? new BsonDocument() :
                   new BsonDocument("_id", new BsonDocument("$lt", new ObjectId(paginationModel.LastId)));

            var messages = await messagesCollection
               .Find(messagesFilter)
               .Sort(new SortDefinitionBuilder<Message>().Descending("$natural"))
               .Limit(paginationModel.Count)
               .ToListAsync();

            bool hasMore = false;

            if (messages.Count == paginationModel.Count)
            {
                var remainderFilter = new BsonDocument("_id", new BsonDocument("$lt", new ObjectId(messages[paginationModel.Count - 1].Id)));

                long count = await messagesCollection
                    .Find(remainderFilter)
                    .Sort(new SortDefinitionBuilder<Message>().Descending("$natural"))
                    .Limit(1)
                    .CountDocumentsAsync();

                if (count != 0)
                {
                    hasMore = true;
                }
            }

            var response = new PagedListResponseModel<Message>
            {
                Data = messages,
                HasMore = hasMore
            };

            return response;
        }

        public async Task<Message> GetLastAsync(string collection)
        {
            var messagesCollection = _mongoDatabase.GetCollection<Message>(collection);

            List<Message> msgs = await messagesCollection
                .Find(new BsonDocument())
                .Sort(new SortDefinitionBuilder<Message>().Descending("$natural"))
                .Limit(1)
                .ToListAsync();

            return msgs.Count == 0 ? null : msgs[0];
        }

        public async Task<int> GetUnreadCountAsync(Guid userId, string collection)
        {
            var messagesCollection = _mongoDatabase.GetCollection<Message>(collection);

            var filter = Builders<Message>.Filter.ElemMatch(
                    message => message.States,
                    messageState => messageState.UserId == userId.ToString() && !messageState.IsRead);

            return (int) await messagesCollection.CountDocumentsAsync(filter);
        }

        public async Task ReadAsync(IEnumerable<string> messageIds, Guid meUserId, string collection)
        {
            var messagesCollection = _mongoDatabase.GetCollection<Message>(collection);

            foreach (string unreadMessageId in messageIds)
            {
                await messagesCollection.FindOneAndUpdateAsync(
                    x => x.Id == unreadMessageId && x.States.Any(c => c.UserId == meUserId.ToString() && !c.IsRead), // find this match
                    Builders<Message>.Update.Set(c => c.States[-1].IsRead, true)); // -1 means update first matching array element
            }
        }
    }
}
