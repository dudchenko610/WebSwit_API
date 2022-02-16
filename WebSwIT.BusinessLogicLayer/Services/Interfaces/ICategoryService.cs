using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebSwIT.DataAccessLayer.Models.Categories;
using WebSwIT.Shared.Models.Pagination;
using WebSwIT.ViewModels.Categories;

namespace WebSwIT.BusinessLogicLayer.Services.Interfaces
{
    public interface ICategoryService
    {
        public Task<CategoryModel> CreateAsync(CreateCategoryModel model);
        public Task DeleteAsync(Guid id);
        public Task<CategoryModel> GetByIdAsync(Guid id);
        public Task<PagedResponseModel<CategoryModel>> GetFilteredAsync(CategoryFilterModel model, PaginationFilterModel pagination);
        public Task<CategoryModel> UpdateAsync(UpdateCategoryModel model);
        public Task<IEnumerable<CategoryModel>> GetAllAsync();
    }
}
