using Microsoft.AspNetCore.Identity;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using WebSwIT.BusinessLogicLayer.Services.Interfaces;
using WebSwIT.DataAccessLayer.Interfaces.Repository;
using WebSwIT.DataAccessLayer.Interfaces.Services;
using WebSwIT.Entities.Entities;
using WebSwIT.Shared.Exceptions;
using WebSwIT.Shared.Models.File;
using WebSwIT.ViewModels.UserPictures;

namespace WebSwIT.BusinessLogicLayer.Services
{
    public class UserPictureService : IUserPictureService
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserRepository _userRepository;
        private readonly IFileService _fileService;
        private readonly IProfileService _profileService;

        public UserPictureService(
            UserManager<User> userManager, 
            IUserRepository userRepository, 
            IFileService fileService,
            IProfileService profileService)
        {
            _userRepository = userRepository;
            _fileService = fileService;
            _userManager = userManager;
            _profileService = profileService;
        }

        public async Task CreateAsync(CreateUserPictureModel model)
        {
            var myUser = await _profileService.GetMyUserAsync();

            await _fileService.CreateAsync(
                model.File,
                Path.Combine("user", myUser.Id.ToString()),
                model.File.FileName
            );

            var user = await _userManager.FindByIdAsync(myUser.Id.ToString());
            user.PictureName = model.File.FileName;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                throw new ServerException("", HttpStatusCode.InternalServerError);
            }
        }

        public async Task DeleteAsync()
        {
            var myUser = await _profileService.GetMyUserAsync();

            if (myUser is null)
            {
                throw new ServerException("", HttpStatusCode.BadRequest);
            }

            _fileService.Delete(
                Path.Combine("user", myUser.Id.ToString()),
                myUser.PictureName
            );

            var user = await _userManager.FindByIdAsync(myUser.Id.ToString());
            user.PictureName = null;
            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                throw new ServerException("", HttpStatusCode.InternalServerError);
            }
        }

        public async Task<DownloadFileModel> GetByIdAsync(Guid userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);

            if (user is null)
            {
                throw new ServerException("", HttpStatusCode.BadRequest);
            }

            var downloadModel = _fileService.Get(
                Path.Combine("user", user.Id.ToString()),
                user.PictureName
            );

            return downloadModel;
        }
    }
}
