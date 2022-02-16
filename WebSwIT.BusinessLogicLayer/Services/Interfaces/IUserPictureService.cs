using System;
using System.Threading.Tasks;
using WebSwIT.Shared.Models.File;
using WebSwIT.ViewModels.UserPictures;

namespace WebSwIT.BusinessLogicLayer.Services.Interfaces
{
    public interface IUserPictureService
    {
        public Task CreateAsync(CreateUserPictureModel model);
        public Task DeleteAsync();
        public Task<DownloadFileModel> GetByIdAsync(Guid id);
    }
}
