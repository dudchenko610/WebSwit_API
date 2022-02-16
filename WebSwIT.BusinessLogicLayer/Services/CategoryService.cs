using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebSwIT.BusinessLogicLayer.Services.Interfaces;
using WebSwIT.DataAccessLayer.Interfaces.Repository;
using WebSwIT.DataAccessLayer.Models.Categories;
using WebSwIT.Entities.Entities;
using WebSwIT.Shared.Exceptions;
using WebSwIT.Shared.Models.Pagination;
using WebSwIT.ViewModels.Categories;

namespace WebSwIT.BusinessLogicLayer.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _imapper;
        public CategoryService(
            ICategoryRepository categoryRepository,
            IMapper imapper
            )
        {
            _categoryRepository = categoryRepository;
            _imapper = imapper;
        }

        public async Task<CategoryModel> CreateAsync(CreateCategoryModel model)
        {
            var existsCategory = await _categoryRepository.GetByNameAsync(model.Name);

            if (existsCategory is not null)
            {
                throw new ServerException("", HttpStatusCode.Conflict);
            }

            var newCategory = _imapper.Map<Category>(model);

            await _categoryRepository.CreateAsync(newCategory);

            return _imapper.Map<CategoryModel>(newCategory);
        }

        public async Task DeleteAsync(Guid id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category is null)
            {
                throw new ServerException("", HttpStatusCode.BadRequest);
            }

            await _categoryRepository.DeleteAsync(category);
        }

        public async Task<CategoryModel> GetByIdAsync(Guid id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category is null)
            {
                throw new ServerException("", HttpStatusCode.BadRequest);
            }

            var categoryModel = _imapper.Map<CategoryModel>(category);

            return categoryModel;
        }

        public async Task<PagedResponseModel<CategoryModel>> GetFilteredAsync(CategoryFilterModel model, PaginationFilterModel pagination)
        {
            var categories = _categoryRepository.GetFilteredCategories(model);

            var count = categories.Count();

            if ((pagination.PageNumber - 1) * pagination.PageSize >= count)
            {
                pagination.PageNumber = (int)Math.Ceiling(count / (float)pagination.PageSize);
            }

            pagination.PageNumber = pagination.PageNumber < 1 ? 1 : pagination.PageNumber;

            categories = categories.Skip((pagination.PageNumber - 1) * pagination.PageSize).Take(pagination.PageSize);
            var result = _imapper.Map<IEnumerable<CategoryModel>>(await categories.ToListAsync());

            var pagedResponse = new PagedResponseModel<CategoryModel>
            {
                Data = result,
                PageNumber = pagination.PageNumber,
                PageSize = pagination.PageSize,
                TotalItems = count
            };

            return pagedResponse;
        }

        public async Task<CategoryModel> UpdateAsync(UpdateCategoryModel model)
        {
            var category = await _categoryRepository.GetByIdAsync(model.Id);

            if (category is null)
            {
                throw new ServerException("", HttpStatusCode.BadRequest);
            }

            category = _imapper.Map<Category>(model);

            await _categoryRepository.UpdateAsync(category);

            var categoryModel = _imapper.Map<CategoryModel>(category);

            return categoryModel;

        }

        public async Task<IEnumerable<CategoryModel>> GetAllAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();

            return _imapper.Map<IEnumerable<CategoryModel>>(categories);
        }
    }
}
