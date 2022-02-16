using AutoMapper;
using WebSwIT.Entities.Entities;
using WebSwIT.Entities.Models;
using WebSwIT.ViewModels.Messages;

namespace WebSwIT.BusinessLogicLayer.MapperProfiles
{
    public class MessageProfile : Profile
    {
        public MessageProfile()
        {
            CreateMap<Message, MessageModel>();
            CreateMap<MessageModel, Message>();

            CreateMap<MessageState, MessageStateModel>();
            CreateMap<MessageStateModel, MessageState>();
        }
    }
}
