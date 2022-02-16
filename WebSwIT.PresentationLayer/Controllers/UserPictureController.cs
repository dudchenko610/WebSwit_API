using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebSwIT.BusinessLogicLayer.Services.Interfaces;
using WebSwIT.Shared;
using WebSwIT.ViewModels.UserPictures;

namespace WebSwIT.PresentationLayer.Controllers
{
    [Route(Constants.Routes.USER_PICTURE)]
    [ApiController]
    public class UserPictureController : Controller
    {
        private readonly IUserPictureService _userPictureService;

        public UserPictureController(IUserPictureService userPictureService)
        {
            _userPictureService = userPictureService;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = Constants.Token.BEARER)]
        [Consumes(Constants.CookieParams.MULTIPART_FORM_DATA)]
        [Route(Constants.Routes.CREATE)]
        public async Task<IActionResult> Create([FromForm] CreateUserPictureModel model)
        {
            await _userPictureService.CreateAsync(model);
            return Ok();
        }

        [HttpGet, DisableRequestSizeLimit]
        [Route(Constants.Routes.GET_BY_ID)]
        public async Task<FileResult> GetById(Guid userId)
        {
            var result = await _userPictureService.GetByIdAsync(userId);
            return File(result.File, System.Net.Mime.MediaTypeNames.Application.Octet, result.Name);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = Constants.Token.BEARER)]
        [Route(Constants.Routes.DELETE)]
        public async Task<IActionResult> Delete()
        {
            await _userPictureService.DeleteAsync();
            return Ok();
        }
    }
}
