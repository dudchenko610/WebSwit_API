using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebSwIT.BusinessLogicLayer.Services.Interfaces;
using WebSwIT.DataAccessLayer.Interfaces.Repository;
using WebSwIT.DataAccessLayer.Models.Technologies;
using WebSwIT.Entities.Entities;
using WebSwIT.Shared.Exceptions;
using WebSwIT.Shared.Models.Pagination;
using WebSwIT.ViewModels.Technologies;

namespace WebSwIT.BusinessLogicLayer.Services
{
    public class TechnologyService : ITechnologyService
    {
        private readonly ITechnologyRepository _technologyRepository;
        private readonly IMapper _imapper;

        public TechnologyService(
            ITechnologyRepository technologyRepository,
            IMapper imapper)
        {
            _technologyRepository = technologyRepository;
            _imapper = imapper;
        }

        public async Task<TechnologyModel> CreateAsync(CreateTechnologyModel model)
        {
            var existsTechnologe = await _technologyRepository.GetByNameAsync(model.Name);

            if (existsTechnologe is not null)
            {
                throw new ServerException("", HttpStatusCode.Conflict);
            }

            var newTechnology = _imapper.Map<Technology>(model);

            await _technologyRepository.CreateAsync(newTechnology);

            return _imapper.Map<TechnologyModel>(newTechnology);
        }

        public async Task DeleteAsync(Guid id)
        {
            var technology = await _technologyRepository.GetByIdAsync(id);

            if (technology is null)
            {
                throw new ServerException("", HttpStatusCode.BadRequest);
            }

            await _technologyRepository.DeleteAsync(technology);
        }

        public async Task<TechnologyModel> GetByIdAsync(Guid id)
        {
            var technology = await _technologyRepository.GetByIdAsync(id);

            if (technology is null)
            {
                throw new ServerException("", HttpStatusCode.BadRequest);
            }

            var technologyModel = _imapper.Map<TechnologyModel>(technology);

            return technologyModel;
        }

        public async Task<PagedResponseModel<TechnologyModel>> GetFilteredAsync(TechnologyFilterModel model, PaginationFilterModel pagination)
        {
            var technologies = _technologyRepository.GetFilteredTechonologies(model);

            var count = technologies.Count();

            if ((pagination.PageNumber - 1) * pagination.PageSize >= count)
            {
                pagination.PageNumber = (int)Math.Ceiling(count / (float)pagination.PageSize);
            }

            pagination.PageNumber = pagination.PageNumber < 1 ? 1 : pagination.PageNumber;

            technologies = technologies.Skip((pagination.PageNumber - 1) * pagination.PageSize).Take(pagination.PageSize);

            var result = _imapper.Map<IEnumerable<TechnologyModel>>(await technologies.ToListAsync());

            var pagedResponse = new PagedResponseModel<TechnologyModel>
            {
                Data = result,
                PageNumber = pagination.PageNumber,
                PageSize = pagination.PageSize,
                TotalItems = count
            };

            return pagedResponse;
        }

        public async Task<TechnologyModel> UpdateAsync(UpdateTechnologyModel model)
        {
            var technology = await _technologyRepository.GetByIdAsync(model.Id);

            if (technology is null)
            {
                throw new ServerException("", HttpStatusCode.BadRequest);
            }

            technology = _imapper.Map<Technology>(model);
            await _technologyRepository.UpdateAsync(technology);

            var roleModel = _imapper.Map<TechnologyModel>(technology);

            return roleModel;
        }

        public async Task<IEnumerable<TechnologyModel>> GetAllAsync()
        {
            var technologies = await _technologyRepository.GetAllAsync();

            return _imapper.Map<IEnumerable<TechnologyModel>>(technologies);
        }
    }
}
