using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebSwIT.BusinessLogicLayer.Helpers;
using WebSwIT.BusinessLogicLayer.Providers;
using WebSwIT.BusinessLogicLayer.Services.Interfaces;
using WebSwIT.DataAccessLayer.Interfaces.Repository;
using WebSwIT.Entities.Entities;
using WebSwIT.Shared.Exceptions;
using WebSwIT.Shared.Options;
using WebSwIT.ViewModels.Accounts;
using WebSwIT.ViewModels.Users;

namespace WebSwIT.BusinessLogicLayer.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly JwtProvider _jwtProvider;
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;
        private readonly EmailProvider _emailProvider;
        private readonly GeneratePasswordHelper _generatePasswordHelper;
        private readonly IOptions<ClientConnectionOptions> _clientOptions;

        public AccountService(
            UserManager<User> userManager, 
            JwtProvider jwtProvider,
            IUserRepository userRepository,
            IUserService userService,
            EmailProvider emailProvider,
            GeneratePasswordHelper generatePasswordHelper,
            IOptions<ClientConnectionOptions> clientOptions)
        {
            _userManager = userManager;
            _jwtProvider = jwtProvider;
            _userRepository = userRepository;
            _userService = userService;
            _emailProvider = emailProvider;
            _generatePasswordHelper = generatePasswordHelper;
            _clientOptions = clientOptions;
        }

        public async Task<TokenResponseModel> SignInAsync(LoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user is null)
            {
                throw new ServerException("User not found!", HttpStatusCode.NotFound);
            }

            if (!user.EmailConfirmed)
            {
                throw new ServerException("User not confirmed!", HttpStatusCode.BadRequest);
            }

            if (!await _userManager.CheckPasswordAsync(user, model.Password))
            {
                throw new ServerException("Wrong password!", HttpStatusCode.BadRequest);
            }

            var claims = await _jwtProvider.GetUserClaimsAsync(user.Email);
            string accessToken = _jwtProvider.GenerateAccessToken(claims);
            string refreshToken = _jwtProvider.GenerateRefreshToken();

            user.RefreshToken = refreshToken;

            await _userRepository.UpdateAsync(user);

            var tokens = new TokenResponseModel
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };

            return tokens;
        }

        public async Task SignUpAsync(CreateUserModel model)
        {
            await _userService.CreateClientAsync(model);
            await SendMessageToEmailAsync(model);
        }

        public async Task ConfirmEmailAsync(string email, string code)
        {
            var user = await _userManager.FindByEmailAsync(email);

            byte[] codeDecodeBytes = WebEncoders.Base64UrlDecode(code);
            string codeDecoded = Encoding.UTF8.GetString(codeDecodeBytes);

            var confirm = await _userManager.ConfirmEmailAsync(user, codeDecoded);
            if (!confirm.Succeeded)
            {
                throw new ServerException("Email not confirmed!", HttpStatusCode.InternalServerError);
            }

            await _userManager.UpdateAsync(user);

        }

        public async Task ForgotPasswordAsync(ForgotPasswordModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user is null)
            {
                throw new ServerException("User not found!", HttpStatusCode.InternalServerError);
            }

            string passwordReset = _generatePasswordHelper.GeneratePassword();

            await _userManager.RemovePasswordAsync(user);
            await _userManager.AddPasswordAsync(user, passwordReset);

            await _userManager.UpdateAsync(user);

            await _emailProvider.SendEmailAsync(model.Email, "New password",
                $"New password: {passwordReset}");
        }

        public async Task<TokenResponseModel> UpdateTokensAsync(TokenResponseModel model)
        {
            if (model is null)
            {
                throw new ServerException("Not correct data!", HttpStatusCode.BadRequest);
            }

            string refreshToken = model.RefreshToken;

            var principal = _jwtProvider.ValidateToken(model.AccessToken);
            string userName = principal.Identity.Name;

            var user = await _userManager.FindByNameAsync(userName);

            if (userName is null || !user.RefreshToken.Equals(refreshToken))
            {
                throw new ServerException("Please, repeat login!");
            }

            string newAccessToken = _jwtProvider.GenerateAccessToken(principal.Claims);
            string newRefreshToken = _jwtProvider.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            await _userRepository.UpdateAsync(user);

            var newTokens = new TokenResponseModel
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            };

            return newTokens;

        }

        private async Task<bool> SendMessageToEmailAsync(CreateUserModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            string code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            byte[] tokenGenerateBytes = Encoding.UTF8.GetBytes(code);
            string tokenCode = WebEncoders.Base64UrlEncode(tokenGenerateBytes);

            var callbackUrl = new StringBuilder();
            callbackUrl.Append($"{_clientOptions.Value.Url}{_clientOptions.Value.UrlConfirmEmail}"); 
            callbackUrl.Append($"?email={model.Email}&code={tokenCode}");

            await _emailProvider.SendEmailAsync(model.Email, "Confirmation yours Email",
                $"Follow the link below\n <a href='{callbackUrl}'></a>");

            return true;
        }
    }
}
