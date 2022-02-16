using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using WebSwIT.BusinessLogicLayer.Services.Interfaces;
using WebSwIT.Shared;
using WebSwIT.ViewModels.Accounts;
using WebSwIT.ViewModels.Users;

namespace WebSwIT.PresentationLayer.Controllers
{
    [ApiController]
    [Route(Constants.Routes.ACCOUNT)]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        [Route(Constants.Routes.SIGN_IN)]
        public async Task<IActionResult> SignIn(LoginModel model)
        {
            var result = await _accountService.SignInAsync(model);
            return Ok(result);
        }

        [HttpPost]
        [Route(Constants.Routes.SIGN_UP)]
        [Produces(Constants.CookieParams.JSON_TYPE)]
        public async Task<IActionResult> SignUp(CreateUserModel model)
        {
            await _accountService.SignUpAsync(model);
            return Ok();
        }

        [HttpGet]
        [Route(Constants.Routes.CONFIRM_EMAIL)]
        public async Task<IActionResult> ConfirmEmail(string email, string code)
        {
            await _accountService.ConfirmEmailAsync(email, code);
            return Ok();
        }

        [HttpPost]
        [Route(Constants.Routes.FORGOT_PASSOWRD)]
        public async Task<IActionResult> ForgotPasswordAsync(ForgotPasswordModel model)
        {
            await _accountService.ForgotPasswordAsync(model);
            return Ok();
        }

        [HttpPost]
        [Route(Constants.Routes.UPDATE_TOKENS)]
        public async Task<IActionResult> UpdateTokensAsync(TokenResponseModel model)
        {
            var newTokens = await _accountService.UpdateTokensAsync(model);
            return Ok(newTokens);
        }
    }
}
