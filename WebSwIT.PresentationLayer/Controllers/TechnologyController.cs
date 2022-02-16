using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebSwIT.BusinessLogicLayer.Services.Interfaces;
using WebSwIT.DataAccessLayer.Models.Technologies;
using WebSwIT.Shared;
using WebSwIT.Shared.Models.Pagination;
using WebSwIT.ViewModels.Technologies;

namespace WebSwIT.PresentationLayer.Controllers
{
    [Route(Constants.Routes.TECHNOLOGY)]
    [ApiController]
    public class TechnologyController : Controller
    {
        private readonly ITechnologyService _technologyService;
        public TechnologyController(ITechnologyService technologyService)
        {
            _technologyService = technologyService;
        }

        [HttpPost]
        [Route(Constants.Routes.CREATE)]
        public async Task<IActionResult> Create(CreateTechnologyModel model)
        {
            var result = await _technologyService.CreateAsync(model);
            return Ok(result);
        }

        [HttpPost]
        [Route(Constants.Routes.UPDATE)]
        public async Task<IActionResult> Update(UpdateTechnologyModel model)
        {
            var result = await _technologyService.UpdateAsync(model);
            return Ok(result);
        }

        [HttpGet]
        [Route(Constants.Routes.GET_BY_ID)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _technologyService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpGet]
        [Route(Constants.Routes.DELETE)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _technologyService.DeleteAsync(id);
            return Ok();
        }

        [HttpGet]
        [Route(Constants.Routes.GET)]
        public async Task<IActionResult> GetFileted([FromQuery] TechnologyFilterModel model, [FromQuery] PaginationFilterModel pagination)
        {
            var result = await _technologyService.GetFilteredAsync(model, pagination);
            return Ok(result);
        }

        [HttpGet]
        [Route(Constants.Routes.GET_ALL)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _technologyService.GetAllAsync();
            return Ok(result);
        }
    }
}
