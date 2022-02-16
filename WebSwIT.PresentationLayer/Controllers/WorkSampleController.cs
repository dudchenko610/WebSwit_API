using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using WebSwIT.BusinessLogicLayer.Services.Interfaces;
using WebSwIT.DataAccessLayer.Models.WorkSamples;
using WebSwIT.Shared;
using WebSwIT.Shared.Models.Pagination;
using WebSwIT.ViewModels.WorkSamples;

namespace WebSwIT.PresentationLayer.Controllers
{
    [Route(Constants.Routes.WORK_SAMPLE)]
    [ApiController]
    public class WorkSampleController : Controller
    {
        private readonly IWorkSampleService _workSampleService;

        public WorkSampleController(IWorkSampleService workSampleService)
        {
            _workSampleService = workSampleService;
        }

        [HttpPost]
        [Route(Constants.Routes.CREATE)]
        public async Task<IActionResult> Create(CreateWorkSampleModel model)
        {
            var result = await _workSampleService.CreateAsync(model);

            return Ok(result);
        }

        [HttpPost]
        [Route(Constants.Routes.UPDATE)]
        public async Task<IActionResult> Update(UpdateWorkSampleModel model)
        {
            var result = await _workSampleService.UpdateAsync(model);

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = Constants.AuthRole.ADMIN, AuthenticationSchemes = Constants.Token.BEARER)]
        [Route("setMainPicture")]
        public async Task<IActionResult> SetMainPicture(SetMainWorkSamplePictureModel model)
        {
            await _workSampleService.SetMainPictureAsync(model);

            return Ok();
        }

        [HttpGet]
        [Route(Constants.Routes.GET_BY_ID)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _workSampleService.GetByIdAsync(id);

            return Ok(result);
        }

        [HttpGet]
        [Route(Constants.Routes.DELETE)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _workSampleService.DeleteAsync(id);

            return Ok();
        }

        [HttpGet]
        [Route(Constants.Routes.GET)]
        public async Task<IActionResult> GetFileted([FromQuery] WorkSampleFilterModel model, [FromQuery] PaginationFilterModel pagination)
        {
            var result = await _workSampleService.GetFilteredAsync(model, pagination);

            return Ok(result);
        }

        [HttpGet]
        [Route("getOnHome")]
        public async Task<IActionResult> GetOnHome()
        {
            var result = await _workSampleService.GetOnHomeAsync();

            return Ok(result);
        }
    }
}
