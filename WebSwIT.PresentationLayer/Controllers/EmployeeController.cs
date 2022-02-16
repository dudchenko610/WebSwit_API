using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebSwIT.BusinessLogicLayer.Services.Interfaces;
using WebSwIT.DataAccessLayer.Models.Employees;
using WebSwIT.Shared;
using WebSwIT.Shared.Models.Pagination;
using WebSwIT.ViewModels.Employees;

namespace WebSwIT.PresentationLayer.Controllers
{
    [Route(Constants.Routes.EMPLOYEE)]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost]
        [Route(Constants.Routes.CREATE)]
        public async Task<IActionResult> Create(CreateEmployeeModel model)
        {
            var result = await _employeeService.CreateAsync(model);
            return Ok(result);
        }

        [HttpPost]
        [Route(Constants.Routes.UPDATE)]
        public async Task<IActionResult> Update(UpdateEmployeeModel model)
        {
            var result = await _employeeService.UpdateAsync(model);
            return Ok(result);
        }

        [HttpGet]
        [Route(Constants.Routes.GET_BY_ID)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _employeeService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpGet]
        [Route(Constants.Routes.DELETE)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _employeeService.DeleteAsync(id);
            return Ok();
        }

        [HttpGet]
        [Route(Constants.Routes.GET)]
        public async Task<IActionResult> GetFileted([FromQuery] EmployeeFilterModel model, [FromQuery] PaginationFilterModel pagination)
        {
            var result = await _employeeService.GetFilteredAsync(model, pagination);
            return Ok(result);
        }
    }
}
