using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebSwIT.BusinessLogicLayer.Services.Interfaces;
using WebSwIT.Shared;

namespace WebSwIT.PresentationLayer.Controllers
{
    [Route(Constants.Routes.CONTACT)]
    [ApiController]
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [Authorize(AuthenticationSchemes = Constants.Token.BEARER)]
        [HttpGet]
        [Route(Constants.Routes.GET_BY_ID)]
        public async Task<IActionResult> GetById(Guid oppositeUserId)
        {
            var contact = await _contactService.GetAsync(oppositeUserId);
            return Ok(contact);
        }

        [Authorize(AuthenticationSchemes = Constants.Token.BEARER)]
        [HttpGet]
        [Route(Constants.Routes.GET_ALL)]
        public async Task<IActionResult> GetAll()
        {
            var contacts = await _contactService.GetAllAsync();
            return Ok(contacts);
        }
    }
}
