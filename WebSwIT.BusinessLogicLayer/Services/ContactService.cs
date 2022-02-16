using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebSwIT.BusinessLogicLayer.Services.Interfaces;
using WebSwIT.DataAccessLayer.Interfaces.Repository;
using WebSwIT.DataAccessLayer.Models.Contacts;
using WebSwIT.ViewModels.Contacts;

namespace WebSwIT.BusinessLogicLayer.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRespository;
        private readonly IProfileService _profileService;
        private readonly IMapper _mapper;

        public ContactService(IContactRepository contactRespository, IProfileService profileService, IMapper mapper)
        {
            _contactRespository = contactRespository;
            _profileService = profileService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ContactModel>> GetAllAsync()
        {
            var myUser = await _profileService.GetMyUserAsync();
            var contactDtos = await _contactRespository.GetAllAsync(myUser.Id);
            var contactsList = _mapper.Map<IEnumerable<ContactDto>, IEnumerable<ContactModel>>(contactDtos);

            return contactsList;
        }

        public async Task<ContactModel> GetAsync(Guid oppositeUserId)
        {
            var myUser = await _profileService.GetMyUserAsync();
            var contactDto = await _contactRespository.GetAsync(myUser.Id, oppositeUserId);
            var contactModel = _mapper.Map<ContactDto, ContactModel>(contactDto);

            return contactModel;
        }
    }
}
