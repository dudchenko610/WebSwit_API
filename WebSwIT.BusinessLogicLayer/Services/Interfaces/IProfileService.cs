using System.Threading.Tasks;
using WebSwIT.ViewModels.Users;

namespace WebSwIT.BusinessLogicLayer.Services.Interfaces
{
    public interface IProfileService
    {
        public Task<UserModel> GetMyUserAsync();
        public Task<UserModel> UpdateAsync(UpdateUserModel model);
    }
}
