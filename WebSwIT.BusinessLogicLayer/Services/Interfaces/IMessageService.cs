using System.Threading.Tasks;
using WebSwIT.DataAccessLayer.Models.Messages;
using WebSwIT.Shared.Models.ListPagination;
using WebSwIT.ViewModels.Messages;

namespace WebSwIT.BusinessLogicLayer.Services.Interfaces
{
    public delegate Task SendFeedbackDelegate(object payload, string userId);

    public interface IMessageService
    {
        Task<PagedListResponseModel<MessageModel>> GetFilteredAsync(PaginationListFilterModel model, MessageFilterModel filter);
        Task<MessageSentModel> SendAsync(MessageModel message, SendFeedbackDelegate callback);
        Task<MessagesReadResponseModel> ReadAsync(MessagesReadRequestModel request, SendFeedbackDelegate callback);
    }
}
