using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebSwIT.BusinessLogicLayer.Services.Interfaces;
using WebSwIT.DataAccessLayer.Models.Proposals;
using WebSwIT.Shared;
using WebSwIT.Shared.Models.Pagination;
using WebSwIT.ViewModels.Proposals;

namespace WebSwIT.PresentationLayer.Controllers
{
    [Route(Constants.Routes.PROPOSAL)]
    [ApiController]
    public class ProposalController : Controller
    {
        private readonly IProposalService _proposalService;

        public ProposalController(IProposalService proposalService)
        {
            _proposalService = proposalService;
        }

        [HttpPost]
        [Route(Constants.Routes.CREATE)]
        public async Task<IActionResult> Create(CreateProposalModel model)
        {
            var result = await _proposalService.CreateAsync(model);

            return Ok(result);
        }

        [HttpGet]
        [Route(Constants.Routes.GET_BY_ID)]
        [Authorize(AuthenticationSchemes = Constants.Token.BEARER)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _proposalService.GetByIdAsync(id);

            return Ok(result);
        }


        [HttpGet]
        [Route(Constants.Routes.DELETE)]
        [Authorize(AuthenticationSchemes = Constants.Token.BEARER)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _proposalService.DeleteAsync(id);

            return Ok();
        }

        [HttpGet]
        [Route(Constants.Routes.GET)]
        [Authorize(AuthenticationSchemes = Constants.Token.BEARER)]
        public async Task<IActionResult> GetFileted([FromQuery] ProposalFilterModel model, [FromQuery] PaginationFilterModel pagination)
        {
            var result = await _proposalService.GetFilteredAsync(model, pagination);

            return Ok(result);
        }
    }
}
