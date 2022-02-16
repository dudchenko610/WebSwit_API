using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebSwIT.BusinessLogicLayer.Services.Interfaces;
using WebSwIT.DataAccessLayer.Models.Messages;
using WebSwIT.Shared;
using WebSwIT.Shared.Models.ListPagination;

namespace WebSwIT.PresentationLayer.Controllers
{
    [Route(Constants.Routes.MESSAGE)]
    [ApiController]
    public class MessageController : Controller
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [Authorize(AuthenticationSchemes = Constants.Token.BEARER)]
        [HttpGet]
        [Route(Constants.Routes.GET)]
        public async Task<IActionResult> GetFileted([FromQuery] PaginationListFilterModel model, [FromQuery] MessageFilterModel filter)
        {
            var response = await _messageService.GetFilteredAsync(model, filter);
            return Ok(response);
        }
    }
}
