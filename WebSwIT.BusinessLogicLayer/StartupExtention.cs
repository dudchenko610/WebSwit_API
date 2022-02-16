using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using System.Threading.Tasks;
using WebSwIT.BusinessLogicLayer.Helpers;
using WebSwIT.BusinessLogicLayer.MapperProfiles;
using WebSwIT.BusinessLogicLayer.Providers;
using WebSwIT.BusinessLogicLayer.Services;
using WebSwIT.BusinessLogicLayer.Services.Interfaces;
using WebSwIT.Shared;

namespace WebSwIT.BusinessLogicLayer
{
    public static class StartupExtention
    {
        public static void BusinessLogicInitializer(this IServiceCollection service,
                                                    IConfiguration configuration)
        {
            DataAccessLayer.StartupExtention.DataAccessInitializer(service, configuration);

            #region Service
            service.AddTransient<IAccountService, AccountService>();
            service.AddTransient<IAdminService, AdminService>();
            service.AddTransient<IUserService, UserService>();
            service.AddTransient<IEmployeeService, EmployeeService>();
            service.AddTransient<IEmployeeAndRoleEmployeeService, EmployeeAndRoleEmployeeService>();
            service.AddTransient<IRoleEmployeeService, RoleEmployeeService>();
            service.AddTransient<ICategoryService, CategoryService>();
            service.AddTransient<ITechnologyService, TechnologyService>();
            service.AddTransient<IOrderService, OrderService>();
            service.AddTransient<IWorkSampleService, WorkSampleService>();
            service.AddTransient<IWorkSamplePictureService, WorkSamplePictureService>();
            service.AddTransient<IUserPictureService, UserPictureService>();
            service.AddTransient<IProfileService, ProfileService>();
            service.AddTransient<IMessageService, MessageService>();
            service.AddTransient<IContactService, ContactService>();
            service.AddTransient<IProposalService, ProposalService>();
            #endregion

            #region Provider
            service.AddTransient<JwtProvider>();
            service.AddTransient<EmailProvider>();
            #endregion

            #region Helper
            service.AddTransient<GeneratePasswordHelper>();
            #endregion

            service.AddHttpContextAccessor();

            var mapperConfig = new MapperConfiguration(config =>
            {
                config.AddProfile(new UserProfile());
                config.AddProfile(new EmployeeProfile());
                config.AddProfile(new RoleEmployeeProfile());
                config.AddProfile(new CategoryProfile());
                config.AddProfile(new TechnologyProfile());
                config.AddProfile(new OrderProfile());
                config.AddProfile(new EmployeeInRoleEmployeeProfile());
                config.AddProfile(new WorkSampleProfile());
                config.AddProfile(new WorkSamplePictureProfile());
                config.AddProfile(new MessageProfile());
                config.AddProfile(new ContactProfile());
                config.AddProfile(new ProposalProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            service.AddSingleton(mapper);

            service.AddAuthentication(options =>
            {
                // Identity made Cookie authentication the default.
                // However, we want JWT Bearer Auth to be the default.
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ClockSkew = TimeSpan.FromMinutes(0),
                        ValidateIssuer = true,
                        ValidIssuer = configuration[$"{Constants.AppSettings.JwtConfiguration}:Issuer"],
                        ValidateAudience = true,
                        ValidAudience = configuration[$"{Constants.AppSettings.JwtConfiguration}:Audience"],
                        ValidateLifetime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration[$"{Constants.AppSettings.JwtConfiguration}:SecretKey"])),
                        ValidateIssuerSigningKey = true,
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query[Constants.Token.ACCESS_TOKEN];
                            var path = context.HttpContext.Request.Path;

                            if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hubs"))
                            {
                                context.Token = accessToken;
                            }

                            return Task.CompletedTask;
                        }
                    };
                });
        }
    }
}
