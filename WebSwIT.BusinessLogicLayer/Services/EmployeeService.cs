using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebSwIT.BusinessLogicLayer.Services.Interfaces;
using WebSwIT.DataAccessLayer.Interfaces.Repository;
using WebSwIT.DataAccessLayer.Models.Employees;
using WebSwIT.Entities.Entities;
using WebSwIT.Shared.Exceptions;
using WebSwIT.Shared.Models.Pagination;
using WebSwIT.ViewModels.Employees;

namespace WebSwIT.BusinessLogicLayer.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmployeeAndRoleEmployeeService _employeeAndRoleEmployeeService;
        private readonly IMapper _imapper;

        public EmployeeService(
            IEmployeeRepository employeeRepository,
            IEmployeeAndRoleEmployeeService employeeAndRoleEmployeeService,
            IMapper imapper)
        {
            _employeeRepository = employeeRepository;
            _employeeAndRoleEmployeeService = employeeAndRoleEmployeeService;
            _imapper = imapper;
        }

        public async Task<EmployeeModel> CreateAsync(CreateEmployeeModel model)
        {
            var existEmployee = await _employeeRepository.GetEmployeeByEmailAsync(model.Email);

            if (existEmployee is not null)
            {
                throw new ServerException("", HttpStatusCode.Conflict);
            }

            var employee = _imapper.Map<Employee>(model);

            await _employeeRepository.CreateAsync(employee);

            await _employeeAndRoleEmployeeService.AddRoleEmployeeToEmployeeAsync(model.RoleIds, employee.Id);

            var employeeModel = _imapper.Map<EmployeeModel>(employee);

            return employeeModel;
        }

        public async Task<EmployeeModel> UpdateAsync(UpdateEmployeeModel model)
        {
            var employee = await _employeeRepository.GetByIdAsync(model.Id);

            if (employee is null)
            {
                throw new ServerException("", HttpStatusCode.BadRequest);
            }

            if (model.RoleIds.Any())
            {
                employee.EmployeeInRoleEmployees.Clear();

                model.RoleIds.ForEach(id => employee.EmployeeInRoleEmployees.Add(new EmployeeInRoleEmployee
                {
                    EmployeeId = employee.Id,
                    RoleEmployeeId = id
                }));
            }

            employee.FirstName = model.FirstName;
            employee.LastName = model.LastName;
            employee.PhoneNumber = model.PhoneNumber;
            employee.Email = model.Email;
            employee.Description = model.Description;

            await _employeeRepository.UpdateAsync(employee);

            var updatedEmployee = _imapper.Map<EmployeeModel>(employee);

            return updatedEmployee;
        }

        public async Task<EmployeeModel> GetByIdAsync(Guid id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);

            if (employee is null)
            {
                throw new ServerException("", HttpStatusCode.BadRequest);
            }

            var employeeModel = _imapper.Map<EmployeeModel>(employee);

            return employeeModel;
        }

        public async Task DeleteAsync(Guid id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);

            if (employee is null)
            {
                throw new ServerException("", HttpStatusCode.BadRequest);
            }

            await _employeeRepository.DeleteAsync(employee);
        }

        public async Task<PagedResponseModel<EmployeeModel>> GetFilteredAsync(EmployeeFilterModel model, PaginationFilterModel pagination)
        {
            var employees = await _employeeRepository.GetFilteredEmployeeAsync(model);

            int employeeCount = employees.Count();

            employees = employees.Skip((pagination.PageNumber - 1) * pagination.PageSize).Take(pagination.PageSize);

            var result = _imapper.Map<IEnumerable<EmployeeModel>>(employees);

            var pagedResponse = new PagedResponseModel<EmployeeModel>
            {
                Data = result,
                PageNumber = pagination.PageNumber,
                PageSize = pagination.PageSize,
                TotalItems = employeeCount
            };

            return pagedResponse;
        }
    }
}
