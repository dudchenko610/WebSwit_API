using System;
using System.Threading.Tasks;
using WebSwIT.DataAccessLayer.Models.Employees;
using WebSwIT.Shared.Models.Pagination;
using WebSwIT.ViewModels.Employees;

namespace WebSwIT.BusinessLogicLayer.Services.Interfaces
{
    public interface IEmployeeService
    {
        public Task<EmployeeModel> CreateAsync(CreateEmployeeModel model);
        public Task<EmployeeModel> UpdateAsync(UpdateEmployeeModel model);
        public Task<EmployeeModel> GetByIdAsync(Guid id);
        public Task DeleteAsync(Guid id);
        public Task<PagedResponseModel<EmployeeModel>> GetFilteredAsync(EmployeeFilterModel model, PaginationFilterModel pagination);
    }
}
