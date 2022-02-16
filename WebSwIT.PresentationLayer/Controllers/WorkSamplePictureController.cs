using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebSwIT.BusinessLogicLayer.Services.Interfaces;
using WebSwIT.Shared;
using WebSwIT.ViewModels.WorkSamplePictures;

namespace WebSwIT.PresentationLayer.Controllers
{
    [Route(Constants.Routes.WORK_SAMPLE_PICTURE)]
    [ApiController]
    public class WorkSamplePictureController : Controller
    {
        private readonly IWorkSamplePictureService _workSamplePictureService;

        public WorkSamplePictureController(IWorkSamplePictureService workSamplePictureService) 
        {
            _workSamplePictureService = workSamplePictureService;
        }

        [HttpPost]
        [Consumes(Constants.CookieParams.MULTIPART_FORM_DATA)]
        [Route(Constants.Routes.CREATE)]
        public async Task<IActionResult> Create([FromForm] CreateWorkSamplePictureModel model)
        {
            var result = await _workSamplePictureService.CreateAsync(model);
            return Ok(result);
        }

        [HttpGet, DisableRequestSizeLimit]
        [Route(Constants.Routes.GET_BY_ID)]
        public async Task<FileResult> GetById(Guid id)
        {
            var result = await _workSamplePictureService.GetByIdAsync(id);
            return File(result.File, System.Net.Mime.MediaTypeNames.Application.Octet, result.Name);
        }

        [HttpGet]
        [Route(Constants.Routes.DELETE)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _workSamplePictureService.DeleteAsync(id);
            return Ok();
        }
    }
}
