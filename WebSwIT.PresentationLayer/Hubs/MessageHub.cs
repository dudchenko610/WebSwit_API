using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Threading;
using System.Threading.Tasks;
using WebSwIT.BusinessLogicLayer.Services.Interfaces;
using WebSwIT.Shared;
using WebSwIT.ViewModels.Messages;

namespace WebSwIT.PresentationLayer.Hubs
{
    public class MessageHub : Hub
    {
        private readonly IMessageService _messangeService;

        public MessageHub(IMessageService messangeService) 
        {
            _messangeService = messangeService;
        }

        [Authorize(AuthenticationSchemes = Constants.Token.BEARER)]
        public async Task<MessageSentModel> SendMessage(MessageModel model)
        {
            Thread.Sleep(100); 
            return await _messangeService.SendAsync(model, async (data, userId) => await Clients.User(userId).SendAsync(Constants.Methods.RECEIVE_MESSAGE, data));
        }

        [Authorize(AuthenticationSchemes = Constants.Token.BEARER)]
        public async Task<MessagesReadResponseModel> ReadMessages(MessagesReadRequestModel model)
        {
            Thread.Sleep(100);
            return await _messangeService.ReadAsync(model, async (data, userId) => await Clients.User(userId).SendAsync(Constants.Methods.OPPOSITE_USER_READ_MESSAGES, data));
        }
    }
}
