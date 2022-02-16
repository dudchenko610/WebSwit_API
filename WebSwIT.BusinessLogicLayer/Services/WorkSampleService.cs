using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebSwIT.BusinessLogicLayer.Services.Interfaces;
using WebSwIT.DataAccessLayer.Interfaces.Repository;
using WebSwIT.DataAccessLayer.Models.WorkSamples;
using WebSwIT.Entities.Entities;
using WebSwIT.Shared.Exceptions;
using WebSwIT.Shared.Models.Pagination;
using WebSwIT.ViewModels.WorkSamples;

namespace WebSwIT.BusinessLogicLayer.Services
{
    public class WorkSampleService : IWorkSampleService
    {
        private readonly IWorkSampleRepository _workSampleRepository;
        private readonly IMapper _mapper;

        public WorkSampleService(
            IWorkSampleRepository workSampleRepository,
            IMapper mapper)
        {
            _workSampleRepository = workSampleRepository;
            _mapper = mapper;
        }

        public async Task<WorkSampleModel> CreateAsync(CreateWorkSampleModel model)
        {
            var newWorkSample = _mapper.Map<WorkSample>(model);
            await _workSampleRepository.CreateAsync(newWorkSample);

            return _mapper.Map<WorkSampleModel>(newWorkSample);
        }

        public async Task DeleteAsync(Guid id)
        {
            var workSample = await _workSampleRepository.GetByIdAsync(id);

            if (workSample is null)
            {
                throw new ServerException("", HttpStatusCode.BadRequest);
            }

            await _workSampleRepository.DeleteAsync(workSample);
        }

        public async Task<WorkSampleModel> GetByIdAsync(Guid id)
        {
            var workSample = await _workSampleRepository.GetByIdAsync(id);

            if (workSample is null)
            {
                throw new ServerException("", HttpStatusCode.BadRequest);
            }

            var workSampleModel = _mapper.Map<WorkSampleModel>(workSample);

            return workSampleModel;
        }

        public async Task<PagedResponseModel<WorkSampleModel>> GetFilteredAsync(WorkSampleFilterModel model, PaginationFilterModel pagination)
        {
            var workSamples = await _workSampleRepository.GetFilteredWorkSamplesAsync(model);

            var count = workSamples.Count();
            workSamples = workSamples.Skip((pagination.PageNumber - 1) * pagination.PageSize).Take(pagination.PageSize);

            var result = _mapper.Map<IEnumerable<WorkSampleModel>>(workSamples);

            var pagedResponse = new PagedResponseModel<WorkSampleModel>
            {
                Data = result,
                PageNumber = pagination.PageNumber,
                PageSize = pagination.PageSize,
                TotalItems = count
            };

            return pagedResponse;
        }

        public async Task<IEnumerable<WorkSampleModel>> GetOnHomeAsync()
        {
            var result =  await _workSampleRepository.GetOnHomeAsync();

            return _mapper.Map<IEnumerable<WorkSampleModel>>(result);
        }

        public async Task SetMainPictureAsync(SetMainWorkSamplePictureModel model)
        {
            var workSample = await _workSampleRepository.GetByIdAsync(model.WorkSampleId);
            workSample.MainPictureId = model.WorkSamplePictureId;

            await _workSampleRepository.UpdateAsync(workSample);
        }

        public async Task<WorkSampleModel> UpdateAsync(UpdateWorkSampleModel model)
        {
            var workSample = await _workSampleRepository.GetByIdAsync(model.Id);

            if (workSample is null)
            {
                throw new ServerException("", HttpStatusCode.BadRequest);
            }

            var updatedWorkSample = _mapper.Map<WorkSample>(model);
            updatedWorkSample.MainPictureId = workSample.MainPictureId;

            await _workSampleRepository.UpdateAsync(updatedWorkSample);

            var workSampleModel = _mapper.Map<WorkSampleModel>(updatedWorkSample);

            return workSampleModel;
        }
    }
}