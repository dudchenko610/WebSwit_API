using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebSwIT.BusinessLogicLayer.Services.Interfaces;
using WebSwIT.Shared;
using WebSwIT.Shared.Models.Pagination;
using WebSwIT.ViewModels.RoleEmployees;

namespace WebSwIT.PresentationLayer.Controllers
{
    [Route(Constants.Routes.ROLE_EMPLOYEE)]
    [ApiController]
    public class RoleEmployeeController : Controller
    {
        private readonly IRoleEmployeeService _roleEmployeeService;
        public RoleEmployeeController(IRoleEmployeeService roleEmployeeService)
        {
            _roleEmployeeService = roleEmployeeService;
        }

        [HttpPost]
        [Route(Constants.Routes.CREATE)]
        public async Task<IActionResult> Create(CreateRoleEmployeeModel model)
        {
            var result = await _roleEmployeeService.CreateAsync(model);
            return Ok(result);
        }

        [HttpPost]
        [Route(Constants.Routes.UPDATE)]
        public async Task<IActionResult> Update(UpdateRoleEmployeeModel model)
        {
            var result = await _roleEmployeeService.UpdateAsync(model);
            return Ok(result);
        }

        [HttpGet]
        [Route(Constants.Routes.GET_BY_ID)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _roleEmployeeService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpGet]
        [Route(Constants.Routes.DELETE)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _roleEmployeeService.DeleteAsync(id);
            return Ok();
        }

        [HttpGet]
        [Route(Constants.Routes.GET)]
        public async Task<IActionResult> GetFileted([FromQuery] PaginationFilterModel pagination)
        {
            var result = await _roleEmployeeService.GetFilteredAsync(pagination);
            return Ok(result);
        }

        [HttpGet]
        [Route(Constants.Routes.GET_ALL)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _roleEmployeeService.GetAllAsync();
            return Ok(result);
        }
    }
}
