using AutoMapper;
using WebSwIT.DataAccessLayer.Models.Contacts;
using WebSwIT.ViewModels.Contacts;

namespace WebSwIT.BusinessLogicLayer.MapperProfiles
{
    public class ContactProfile : Profile
    {
        public ContactProfile()
        {
            CreateMap<ContactDto, ContactModel>();
        }
    }
}
