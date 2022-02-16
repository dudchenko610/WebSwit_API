using AutoMapper;
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
using WebSwIT.ViewModels.WorkSamplePictures;

namespace WebSwIT.BusinessLogicLayer.Services
{
    public class WorkSamplePictureService : IWorkSamplePictureService
    {
        private readonly IWorkSamplePictureRepository _workSamplePictureRepository;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;

        public WorkSamplePictureService(
            IWorkSamplePictureRepository workSamplePictureRepository,
            IFileService fileService,
            IMapper mapper)
        {
            _workSamplePictureRepository = workSamplePictureRepository;
            _fileService = fileService;
            _mapper = mapper;
        }

        public async Task<WorkSamplePictureModel> CreateAsync(CreateWorkSamplePictureModel model)
        {
            await _fileService.CreateAsync(
                model.File,
                Path.Combine("workSample", model.WorkSampleId.ToString()),
                model.File.FileName
            );

            var workSamplePicture = _mapper.Map<WorkSamplePicture>(model);
            await _workSamplePictureRepository.CreateAsync(workSamplePicture);

            return _mapper.Map<WorkSamplePictureModel>(workSamplePicture);
        }

        public async Task DeleteAsync(Guid id)
        {
            var workSamplePicture = await _workSamplePictureRepository.GetByIdAsync(id);

            if (workSamplePicture is null)
            {
                throw new ServerException("", HttpStatusCode.BadRequest);
            }

            _fileService.Delete(
                Path.Combine("workSample", workSamplePicture.WorkSampleId.ToString()),
                workSamplePicture.Name
            );

            await _workSamplePictureRepository.DeleteAsync(workSamplePicture);
        }

        public async Task<DownloadFileModel> GetByIdAsync(Guid id)
        {
            var workSamplePicture = await _workSamplePictureRepository.GetByIdAsync(id);

            if (workSamplePicture is null)
            {
                throw new ServerException("", HttpStatusCode.BadRequest);
            }

            var downloadModel = _fileService.Get(
                Path.Combine("workSample", workSamplePicture.WorkSampleId.ToString()),
                workSamplePicture.Name
            );

            return downloadModel;
        }
    }
}
