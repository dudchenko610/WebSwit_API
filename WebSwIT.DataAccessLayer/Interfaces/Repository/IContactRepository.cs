using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebSwIT.DataAccessLayer.Models.Contacts;
using WebSwIT.Entities.Entities;

namespace WebSwIT.DataAccessLayer.Interfaces.Repository
{
    public interface IContactRepository : IBaseRepository<Contact>
    {
        Task CreateIfNotExistsAsync(Guid user1Id, Guid user2Id);
        Task<IEnumerable<ContactDto>> GetAllAsync(Guid meUserId);
        Task<ContactDto> GetAsync(Guid meUserId, Guid oppositeUserId);
    }
}
