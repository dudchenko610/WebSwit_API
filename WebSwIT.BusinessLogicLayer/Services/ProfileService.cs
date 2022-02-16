using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using WebSwIT.BusinessLogicLayer.Services.Interfaces;
using WebSwIT.DataAccessLayer.Interfaces.Repository;
using WebSwIT.ViewModels.Users;

namespace WebSwIT.BusinessLogicLayer.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IMapper _imapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;

        public ProfileService(
            IMapper imapper,
            IHttpContextAccessor httpContextAccessor,
            IUserRepository userRepository,
            IUserService userService)
        {
            _imapper = imapper;
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
            _userService = userService;
        }

        public async Task<UserModel> GetMyUserAsync()
        {
            var user = await _userRepository.GetByIdAsync(new Guid(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value));
            var userModel = _imapper.Map<UserModel>(user);
            return userModel;
        }

        public async Task<UserModel> UpdateAsync(UpdateUserModel model)
        {
            var myUser = await GetMyUserAsync();
            model.Id = myUser.Id.ToString();

            var updatedUser = await _userService.UpdateClientAsync(model);
            return updatedUser;
        }
    }
}
