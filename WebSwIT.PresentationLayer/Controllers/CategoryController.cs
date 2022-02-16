using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebSwIT.BusinessLogicLayer.Services.Interfaces;
using WebSwIT.DataAccessLayer.Models.Categories;
using WebSwIT.Shared;
using WebSwIT.Shared.Models.Pagination;
using WebSwIT.ViewModels.Categories;

namespace WebSwIT.PresentationLayer.Controllers
{
    [Route(Constants.Routes.CATEGORY)]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        [Route(Constants.Routes.CREATE)]
        public async Task<IActionResult> Create(CreateCategoryModel model)
        {
            var result = await _categoryService.CreateAsync(model);
            return Ok(result);
        }

        [HttpPost]
        [Route(Constants.Routes.UPDATE)]
        public async Task<IActionResult> Update(UpdateCategoryModel model)
        {
            var result = await _categoryService.UpdateAsync(model);
            return Ok(result);
        }

        [HttpGet]
        [Route(Constants.Routes.GET_BY_ID)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _categoryService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpGet]
        [Route(Constants.Routes.DELETE)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _categoryService.DeleteAsync(id);
            return Ok();
        }

        [HttpGet]
        [Route(Constants.Routes.GET)]
        public async Task<IActionResult> GetFileted([FromQuery] CategoryFilterModel model, [FromQuery] PaginationFilterModel pagination)
        {
            var result = await _categoryService.GetFilteredAsync(model, pagination);
            return Ok(result);
        }

        [HttpGet]
        [Route(Constants.Routes.GET_ALL)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _categoryService.GetAllAsync();
            return Ok(result);
        }
    }
}
