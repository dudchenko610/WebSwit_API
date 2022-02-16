using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebSwIT.Entities.Entities;
using WebSwIT.Shared.Models.ListPagination;

namespace WebSwIT.DataAccessLayer.Interfaces.Repository
{
    public interface IMessageRepository
    {
        Task CreateAsync(Message message, string collection);
        Task ReadAsync(IEnumerable<string> messageIds, Guid meUserId, string collection);
        Task<PagedListResponseModel<Message>> GetFilteredAsync(PaginationListFilterModel paginationModel, string collection);
        Task<int> GetUnreadCountAsync(Guid userId, string collection);
        Task<Message> GetLastAsync(string collection);
    }
}
