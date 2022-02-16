using System.Threading.Tasks;
using WebSwIT.ViewModels.Accounts;
using WebSwIT.ViewModels.Users;

namespace WebSwIT.BusinessLogicLayer.Services.Interfaces
{
    public interface IAccountService
    {
        public Task<TokenResponseModel> SignInAsync(LoginModel model);
        public Task SignUpAsync(CreateUserModel model);
        public Task ConfirmEmailAsync(string email, string code);
        public Task ForgotPasswordAsync(ForgotPasswordModel model);
        public Task<TokenResponseModel> UpdateTokensAsync(TokenResponseModel model);
    }
}
