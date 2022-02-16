using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebSwIT.ViewModels.Contacts;

namespace WebSwIT.BusinessLogicLayer.Services.Interfaces
{
    public interface IContactService
    {
        Task<IEnumerable<ContactModel>> GetAllAsync();
        Task<ContactModel> GetAsync(Guid oppositeUserId);
    }
}
