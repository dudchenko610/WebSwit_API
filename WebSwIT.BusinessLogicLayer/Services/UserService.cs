using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using WebSwIT.BusinessLogicLayer.Services.Interfaces;
using WebSwIT.DataAccessLayer.Interfaces.Repository;
using WebSwIT.DataAccessLayer.Models.Users;
using WebSwIT.Entities.Entities;
using WebSwIT.Shared.Enums;
using WebSwIT.Shared.Exceptions;
using WebSwIT.Shared.Models.Pagination;
using WebSwIT.ViewModels.Users;

namespace WebSwIT.BusinessLogicLayer.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _imapper;
        private readonly IUserRepository _userRepository;

        public UserService(
            UserManager<User> userManager,
            IMapper imapper,
            IUserRepository userRepository)
        {
            _userManager = userManager;
            _imapper = imapper;
            _userRepository = userRepository;
        }

        public async Task CreateClientAsync(CreateUserModel model)
        {
            var existingUser = await _userManager.FindByEmailAsync(model.Email);

            if (existingUser is not null)
            {
                throw new ServerException("User already exist!", HttpStatusCode.InternalServerError);
            }

            var user = _imapper.Map<User>(model);
            user.UserName = model.Email;

            var newUser = await _userManager.CreateAsync(user, model.Password);

            if (!newUser.Succeeded)
            {
                throw new ServerException("User was not register!", HttpStatusCode.InternalServerError);
            }

            var addedToRole = await _userManager.AddToRoleAsync(user, Enums.UserRole.Client.ToString());

            if (!addedToRole.Succeeded)
            {
                var createdUserNow = await _userManager.FindByEmailAsync(user.Email);
                await _userManager.DeleteAsync(createdUserNow);

                throw new ServerException("Role no added fo user!/\nPlease, repeat registration!", HttpStatusCode.InternalServerError);
            }
        }

        public async Task<PagedResponseModel<UserModel>> GetClientAsync(UserFilterModel filter, PaginationFilterModel pagination)
        {
            var users = await _userRepository.GetFilterUserAsync(filter);

            int countElement = users.Count();

            users = users.Skip((pagination.PageNumber - 1) * pagination.PageSize).Take(pagination.PageSize);

            var result = _imapper.Map<IEnumerable<UserModel>>(users);

            var pagedResponse = new PagedResponseModel<UserModel>
            {
                Data = result,
                PageNumber = pagination.PageNumber,
                PageSize = pagination.PageSize,
                TotalItems = countElement
            };

            return pagedResponse;
        }

        public async Task<UserModel> UpdateClientAsync(UpdateUserModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);

            if (user is null)
            {
                throw new ServerException("User with this email does not exest!", HttpStatusCode.InternalServerError);
            }

            if (!string.IsNullOrWhiteSpace(model.FirstName))
            {
                user.FirstName = model.FirstName;
            }

            if (!string.IsNullOrWhiteSpace(model.LastName))
            {
                user.LastName = model.LastName;
            }

            if (!string.IsNullOrWhiteSpace(model.PhoneNumber))
            {
                user.PhoneNumber = model.PhoneNumber;
            }

            if (!string.IsNullOrWhiteSpace(model.Email))
            {
                user.Email = model.Email;
            }

            if (!string.IsNullOrWhiteSpace(model.Password))
            {
                await _userManager.RemovePasswordAsync(user);
                await _userManager.AddPasswordAsync(user, model.Password);
            }

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                throw new ServerException("", HttpStatusCode.InternalServerError);
            }

            var userModel = _imapper.Map<UserModel>(user);

            return userModel;
        }

        public async Task DeleteAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user is null)
            {
                throw new ServerException("", HttpStatusCode.BadRequest);
            }

            await _userRepository.DeleteAsync(user);
        }

        public async Task<UserModel> GetByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            return _imapper.Map<UserModel>(user);
        }
    }
}
