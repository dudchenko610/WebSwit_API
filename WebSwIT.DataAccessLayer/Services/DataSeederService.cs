using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebSwIT.DataAccessLayer.AppContext;
using WebSwIT.DataAccessLayer.Interfaces.Services;
using WebSwIT.Entities.Entities;
using WebSwIT.Shared;
using WebSwIT.Shared.Enums;
using WebSwIT.Shared.Extensions;
using WebSwIT.Shared.Helpers;
using WebSwIT.Shared.Options;

namespace WebSwIT.DataAccessLayer.Services
{
    public class DataSeederService : IDataSeederService
    {
        private readonly ApplicationContext _appContext;
        private readonly AdminCredentialsOptions _adminCredentials;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public DataSeederService(
            ApplicationContext appContext, 
            IOptions<AdminCredentialsOptions> adminCredentialsOptions, 
            UserManager<User> userManager, 
            RoleManager<IdentityRole<Guid>> roleManager)
        {
            _appContext = appContext;
            _adminCredentials = adminCredentialsOptions.Value;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedDataAsync()
        {
            await SeedRolesAsync();
            await SeedAdminUserAsync();
        }

        private async Task SeedRolesAsync()
        {
            var enumRolesList = EnumHelper.GetValues<Enums.UserRole>().Select(x => new IdentityRole<Guid> { Name = x.ToString() });
            var rolesInDb = _appContext.Roles.ToList();

            var rolesToDelete = rolesInDb.Except(enumRolesList, x => x.Name, y => y.Name);
            var rolesToAdd = enumRolesList.Except(rolesInDb, x => x.Name, y => y.Name);

            if (rolesToAdd.Any())
            {
                foreach (var role in rolesToAdd)
                {
                    await _roleManager.CreateAsync(role);
                }
            }

            if (rolesToDelete.Any())
            {
                foreach (var role in rolesToDelete)
                {
                    await _roleManager.DeleteAsync(role);
                }
            }
        }

        private async Task SeedAdminUserAsync()
        {
            if (await _userManager.FindByEmailAsync(_adminCredentials.Email) is null)
            {
                var adminUser = new User
                {
                    UserName = _adminCredentials.Email,
                    Email = _adminCredentials.Email,
                    EmailConfirmed = true,
                    FirstName = _adminCredentials.Nickname,
                    LastName = _adminCredentials.Nickname
                };

                var result = await _userManager.CreateAsync(adminUser, _adminCredentials.Password);

                if (!result.Succeeded)
                {
                    throw new System.Exception(Constants.ErrorMessages.SeedData.AdminUserWasNotCreated);
                }

                result = await _userManager.AddToRoleAsync(adminUser, Enums.UserRole.Admin.ToString());

                if (!result.Succeeded)
                {
                    throw new System.Exception(Constants.ErrorMessages.SeedData.AdminUserWasNotAddedToAdminRole);
                }
            }
        }
    }
}
