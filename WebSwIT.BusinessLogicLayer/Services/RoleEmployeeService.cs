using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebSwIT.BusinessLogicLayer.Services.Interfaces;
using WebSwIT.DataAccessLayer.Interfaces.Repository;
using WebSwIT.Entities.Entities;
using WebSwIT.Shared.Exceptions;
using WebSwIT.Shared.Models.Pagination;
using WebSwIT.ViewModels.RoleEmployees;

namespace WebSwIT.BusinessLogicLayer.Services
{
    public class RoleEmployeeService : IRoleEmployeeService
    {
        private readonly IRoleEmployeeRepository _roleEmployeeRepository;
        private readonly IMapper _imapper;

        public RoleEmployeeService(
            IRoleEmployeeRepository roleEmployeeRepository,
             IMapper imapper)
        {
            _roleEmployeeRepository = roleEmployeeRepository;
            _imapper = imapper;
        }

        public async Task<RoleEmployeeModel> CreateAsync(CreateRoleEmployeeModel model)
        {
            var existsRole = await _roleEmployeeRepository.GetByNameAsync(model.Name);

            if (existsRole is not null)
            {
                throw new ServerException("", HttpStatusCode.Conflict);
            }

            var role = _imapper.Map<RoleEmployee>(model);
            await _roleEmployeeRepository.CreateAsync(role);

            var roleModel = _imapper.Map<RoleEmployeeModel>(role);
            return roleModel;
        }

        public async Task DeleteAsync(Guid id)
        {
            var role = await _roleEmployeeRepository.GetByIdAsync(id);

            if (role is null)
            {
                throw new ServerException("", HttpStatusCode.BadRequest);
            }

            await _roleEmployeeRepository.DeleteAsync(role);
        }

        public async Task<RoleEmployeeModel> GetByIdAsync(Guid id)
        {
            var role = await _roleEmployeeRepository.GetByIdAsync(id);

            if (role is null)
            {
                throw new ServerException("", HttpStatusCode.BadRequest);
            }

            var roleModel = _imapper.Map<RoleEmployeeModel>(role);

            return roleModel;
        }

        public async Task<PagedResponseModel<RoleEmployeeModel>> GetFilteredAsync(PaginationFilterModel pagination)
        {
            var roles = await _roleEmployeeRepository.GetAllAsync();

            var rolesCount = roles.Count();

            roles = roles.Skip((pagination.PageNumber - 1) * pagination.PageSize).Take(pagination.PageSize);

            var result = _imapper.Map<IEnumerable<RoleEmployeeModel>>(roles);

            var pagedResponse = new PagedResponseModel<RoleEmployeeModel>
            {
                Data = result,
                PageNumber = pagination.PageNumber,
                PageSize = pagination.PageSize,
                TotalItems = rolesCount
            };

            return pagedResponse;
        }

        public async Task<RoleEmployeeModel> UpdateAsync(UpdateRoleEmployeeModel model)
        {
            var roleEmployee = await _roleEmployeeRepository.GetByIdAsync(model.Id);

            if (roleEmployee is null)
            {
                throw new ServerException("", HttpStatusCode.BadRequest);
            }

            roleEmployee = _imapper.Map<RoleEmployee>(model);
            await _roleEmployeeRepository.UpdateAsync(roleEmployee);

            var roleModel = _imapper.Map<RoleEmployeeModel>(roleEmployee);

            return roleModel;
        }

        public async Task<IEnumerable<RoleEmployeeModel>> GetAllAsync()
        {
            var roles = await _roleEmployeeRepository.GetAllAsync();

            return _imapper.Map<IEnumerable<RoleEmployeeModel>>(roles);
        }
    }
}
