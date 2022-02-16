using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebSwIT.BusinessLogicLayer.Services.Interfaces;
using WebSwIT.Shared;
using WebSwIT.ViewModels.Users;

namespace WebSwIT.PresentationLayer.Controllers
{
    [Route(Constants.Routes.PROFILE)]
    [ApiController]
    public class ProfileController : Controller
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [Authorize(AuthenticationSchemes = Constants.Token.BEARER)]
        [HttpPost]
        [Route(Constants.Routes.UPDATE)]
        public async Task<IActionResult> UpdateUser(UpdateUserModel model)
        {
            var result = await _profileService.UpdateAsync(model);
            return Ok(result);
        }

        [Authorize(AuthenticationSchemes = Constants.Token.BEARER)]
        [HttpGet]
        [Route(Constants.Routes.GET)]
        public async Task<IActionResult> Get()
        {
            var user = await _profileService.GetMyUserAsync();
            return Ok(user);
        }
    }
}
