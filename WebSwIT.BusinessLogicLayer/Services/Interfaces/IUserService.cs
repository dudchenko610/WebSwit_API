using System.Threading.Tasks;
using WebSwIT.DataAccessLayer.Models.Users;
using WebSwIT.Shared.Models.Pagination;
using WebSwIT.ViewModels.Users;

namespace WebSwIT.BusinessLogicLayer.Services.Interfaces
{
    public interface IUserService
    {
        public Task CreateClientAsync(CreateUserModel model);
        public Task<UserModel> UpdateClientAsync(UpdateUserModel model);
        public Task<PagedResponseModel<UserModel>> GetClientAsync(UserFilterModel filter, PaginationFilterModel pagination);
        public Task DeleteAsync(string id);
        public Task<UserModel> GetByIdAsync(string id);
    }
}
