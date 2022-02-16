using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using WebSwIT.BusinessLogicLayer.Services.Interfaces;
using WebSwIT.DataAccessLayer.Models.Users;
using WebSwIT.Shared;
using WebSwIT.Shared.Models.Pagination;
using WebSwIT.ViewModels.Users;

namespace WebSwIT.PresentationLayer.Controllers
{
    [Route(Constants.Routes.USER)]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route(Constants.Routes.UPDATE)]
        public async Task<IActionResult> UpdateUser(UpdateUserModel model)
        {
            var result = await _userService.UpdateClientAsync(model);
            return Ok(result);
        }

        [HttpGet]
        [Route(Constants.Routes.GET)]
        public async Task<IActionResult> GetUser([FromQuery] UserFilterModel filter, [FromQuery] PaginationFilterModel pagination)
        {
            var result = await _userService.GetClientAsync(filter, pagination);
            return Ok(result);
        }

        [HttpGet]
        [Route(Constants.Routes.DELETE)]
        public async Task<IActionResult> Delete(string id)
        {
            await _userService.DeleteAsync(id);
            return Ok();
        }

        [HttpGet]
        [Route(Constants.Routes.GET_BY_ID)]
        public async Task<IActionResult> GetById(string id)
        {
            var user = await _userService.GetByIdAsync(id);
            return Ok(user);
        }
    }
}
