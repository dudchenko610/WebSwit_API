using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebSwIT.BusinessLogicLayer.Services.Interfaces;
using WebSwIT.DataAccessLayer.Helpers;
using WebSwIT.DataAccessLayer.Interfaces.Repository;
using WebSwIT.DataAccessLayer.Models.Messages;
using WebSwIT.Entities.Entities;
using WebSwIT.Shared.Exceptions;
using WebSwIT.Shared.Models.ListPagination;
using WebSwIT.ViewModels.Messages;

namespace WebSwIT.BusinessLogicLayer.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IContactRepository _contactRepository;
        private readonly IProfileService _profileService;
        private readonly IMapper _mapper;

        public MessageService(IMessageRepository messageRepository, IContactRepository contactRepository, IProfileService profileService, IMapper mapper)
        {
            _messageRepository = messageRepository;
            _contactRepository = contactRepository;
            _profileService = profileService;
            _mapper = mapper;
        }

        public async Task<PagedListResponseModel<MessageModel>> GetFilteredAsync(PaginationListFilterModel model, MessageFilterModel filter)
        {
            var myUser = await _profileService.GetMyUserAsync();
            var collection = ContactHelper.GetContactCollectionName(myUser.Id, filter.OppositeUserId);

            var pagedMessages = await _messageRepository.GetFilteredAsync(model, collection);
            var response = new PagedListResponseModel<MessageModel>
            {
                Data = _mapper.Map<IEnumerable<Message>, IEnumerable<MessageModel>>(pagedMessages.Data),
                HasMore = pagedMessages.HasMore
            };

            return response;
        }

        public async Task<MessagesReadResponseModel> ReadAsync(MessagesReadRequestModel request, SendFeedbackDelegate callback)
        {
            var myUser = await _profileService.GetMyUserAsync();
            var collection = ContactHelper.GetContactCollectionName(myUser.Id, request.AuthorUserId);

            // READ MESSAGES WITH IDS
            await _messageRepository.ReadAsync(request.MessagesIds, myUser.Id, collection);

            // CALCULATE UNREAD AMOUNT
            int unreadCount = await _messageRepository.GetUnreadCountAsync(myUser.Id, collection);

            // NOTIFY OPPOSITE USER THAT I READ THAT MESSAGES
            var readResponse = new MessagesReadResponseModel
            {
                ReaderUserId = myUser.Id,
                MessagesIds = request.MessagesIds,
                UnreadCount = unreadCount
            };

            await callback(readResponse, request.AuthorUserId.ToString());

            // NOTIFY ME THAT I READ THAT MESSAGES (update list with users)
            return readResponse;
        }

        public async Task<MessageSentModel> SendAsync(MessageModel messageModel, SendFeedbackDelegate callback)
        {
            var myUser = await _profileService.GetMyUserAsync();

            if (messageModel.AuthorUserId != myUser.Id)
            {
                throw new ServerException("You are not author of the message!");
            }

            if (messageModel.AuthorUserId == messageModel.ReceiverUserId)
            {
                throw new ServerException("You cannot be a receiver of your own messages!");
            }

            await _contactRepository.CreateIfNotExistsAsync(myUser.Id, messageModel.ReceiverUserId);

            // INSERT MESSAGE INTO DATABASE
            messageModel.LastUpdate = DateTime.Now;
            messageModel.States = new List<MessageStateModel>();
            messageModel.States.Add(new MessageStateModel { UserId = messageModel.AuthorUserId, IsRead = true });
            messageModel.States.Add(new MessageStateModel { UserId = messageModel.ReceiverUserId, IsRead = false });

            var collection = ContactHelper.GetContactCollectionName(messageModel.AuthorUserId, messageModel.ReceiverUserId);
            var message = _mapper.Map<MessageModel, Message>(messageModel);

            await _messageRepository.CreateAsync(message, collection);

            // CALCULATE UNREAD AMOUNT FOR OPPOSITE USER
            int unreadCount = await _messageRepository.GetUnreadCountAsync(message.ReceiverUserId, collection);

            // NOTIFY OPPOSITE USER
            var messageCameModel = new MessageCameModel
            {
                Message = _mapper.Map<Message, MessageModel>(message),
                UnreadCount = unreadCount
            };

            await callback(messageCameModel, message.ReceiverUserId.ToString());

            var model = new MessageSentModel
            {
                LastUpdate = message.LastUpdate,
                MessageId = message.Id
            };

            return model;
        }
    }
}
