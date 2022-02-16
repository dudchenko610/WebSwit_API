using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebSwIT.Shared.Models.Pagination;
using WebSwIT.ViewModels.RoleEmployees;

namespace WebSwIT.BusinessLogicLayer.Services.Interfaces
{
    public interface IRoleEmployeeService
    {
        public Task<RoleEmployeeModel> CreateAsync(CreateRoleEmployeeModel model);
        public Task<RoleEmployeeModel> UpdateAsync(UpdateRoleEmployeeModel model);
        public Task<RoleEmployeeModel> GetByIdAsync(Guid id);
        public Task DeleteAsync(Guid id);
        public Task<PagedResponseModel<RoleEmployeeModel>> GetFilteredAsync(PaginationFilterModel pagination);
        public Task<IEnumerable<RoleEmployeeModel>> GetAllAsync();
    }
}
