using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebSwIT.BusinessLogicLayer.Services.Interfaces;
using WebSwIT.DataAccessLayer.Models.Orders;
using WebSwIT.Shared;
using WebSwIT.Shared.Models.Pagination;
using WebSwIT.ViewModels.Orders;

namespace WebSwIT.PresentationLayer.Controllers
{
    [Route(Constants.Routes.ORDER)]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [Authorize(Roles = Constants.AuthRole.ADMIN, AuthenticationSchemes = Constants.Token.BEARER)]
        [HttpPost]
        [Route(Constants.Routes.CREATE)]
        public async Task<IActionResult> Create(CreateOrderModel model)
        {
            var result = await _orderService.CreateAsync(model);
            return Ok(result);
        }

        [HttpPost]
        [Route(Constants.Routes.UPDATE)]
        [Authorize(Roles = Constants.AuthRole.ADMIN, AuthenticationSchemes = Constants.Token.BEARER)]
        public async Task<IActionResult> Update(UpdateOrderModel model)
        {
            var result = await _orderService.UpdateAsync(model);
            return Ok(result);
        }

        [HttpGet]
        [Route(Constants.Routes.GET_BY_ID)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _orderService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpGet]
        [Route(Constants.Routes.DELETE)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _orderService.DeleteAsync(id);
            return Ok();
        }
        [Authorize(Roles = Constants.AuthRole.ADMIN, AuthenticationSchemes = Constants.Token.BEARER)]
        [HttpGet]
        [Route(Constants.Routes.GET)]
        public async Task<IActionResult> GetFileted([FromQuery] OrderFilterModel model, [FromQuery] PaginationFilterModel pagination)
        {
            var result = await _orderService.GetFilteredAsync(model, pagination);
            return Ok(result);
        }
    }
}
