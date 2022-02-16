using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSwIT.DataAccessLayer.AppContext;
using WebSwIT.DataAccessLayer.Helpers;
using WebSwIT.DataAccessLayer.Interfaces.Repository;
using WebSwIT.DataAccessLayer.Models.Contacts;
using WebSwIT.Entities.Entities;

namespace WebSwIT.DataAccessLayer.Repositories
{
    public class ContactRepository : BaseRepository<Contact>, IContactRepository
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IUserRepository _userRepository;

        public ContactRepository(ApplicationContext context, 
            IMessageRepository messageRepository,
            IUserRepository userRepository) : base(context)
        {
            _messageRepository = messageRepository;
            _userRepository = userRepository;
        }

        public async Task CreateIfNotExistsAsync(Guid user1Id, Guid user2Id)
        {
            var contactInDb = await _dbSet
                .FirstOrDefaultAsync(c => (c.UserId1 == user1Id && c.UserId2 == user2Id) 
                || (c.UserId2 == user1Id && c.UserId1 == user2Id));

            if (contactInDb == null)
            {
                var contact = new Contact
                {
                    UserId1 = user1Id,
                    UserId2 = user2Id
                };

                // relative to meUser in Contacts1 User1 always is id of meUser
                await CreateAsync(contact);
            }
        }

        public async Task<IEnumerable<ContactDto>> GetAllAsync(Guid meUserId)
        {
            var user = await _userRepository.GetUserWithContactsByIdAsync(meUserId);
            var contactUsers = new List<User>();

            contactUsers.AddRange(user.Contacts1.Select(ch => ch.User2)); // I started chat
            contactUsers.AddRange(user.Contacts2.Select(ch => ch.User1)); // Opposite user started chat

            var contactDtos = new List<ContactDto>();
            var dt1970 = new DateTime(1970, 1, 1);

            foreach (var oppositeUser in contactUsers)
            {
                var collection = ContactHelper.GetContactCollectionName(meUserId, oppositeUser.Id);

                var lastMessage = await _messageRepository.GetLastAsync(collection);
                var milliseconds = lastMessage != null ? (lastMessage.LastUpdate - dt1970).TotalSeconds : 0;

                contactDtos.Add(
                    new ContactDto
                    {
                        User = oppositeUser,
                        UnreadCount = await _messageRepository.GetUnreadCountAsync(meUserId, collection), // INEFFECTIVE OR MAYBE NOT :))
                        MillisecondsSince1970 = (long) milliseconds
                    }
                );
            }

            return contactDtos;
        }

        public async Task<ContactDto> GetAsync(Guid meUserId, Guid oppositeUserId)
        {
            var contact = await _dbSet.FirstOrDefaultAsync(c => (c.UserId1 == meUserId && c.UserId2 == oppositeUserId)
                || (c.UserId2 == meUserId && c.UserId1 == oppositeUserId));

            if (contact == null)
            {
                throw new Exception("Contact does not exists");
            }

            var collection = ContactHelper.GetContactCollectionName(meUserId, oppositeUserId);

            var dt1970 = new DateTime(1970, 1, 1);
            var lastMessage = await _messageRepository.GetLastAsync(collection);
            var timeSpan = lastMessage.LastUpdate - dt1970;

            var contactDto = new ContactDto
            {
                User = await _userRepository.GetByIdAsync(oppositeUserId),
                UnreadCount = await _messageRepository.GetUnreadCountAsync(meUserId, collection), // INEFFECTIVE
                MillisecondsSince1970 = (long) timeSpan.TotalMilliseconds
            };

            return contactDto;
        }
    }
}
