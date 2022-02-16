using System;
using System.Threading.Tasks;
using WebSwIT.Shared.Models.File;
using WebSwIT.ViewModels.WorkSamplePictures;

namespace WebSwIT.BusinessLogicLayer.Services.Interfaces
{
    public interface IWorkSamplePictureService
    {
        public Task<WorkSamplePictureModel> CreateAsync(CreateWorkSamplePictureModel model);
        public Task<DownloadFileModel> GetByIdAsync(Guid id);
        public Task DeleteAsync(Guid id);
    }
}
