using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System;
using WebSwIT.DataAccessLayer.AppContext;
using WebSwIT.DataAccessLayer.Interfaces.Repository;
using WebSwIT.DataAccessLayer.Interfaces.Services;
using WebSwIT.DataAccessLayer.Repositories;
using WebSwIT.DataAccessLayer.Services;
using WebSwIT.Entities.Entities;
using WebSwIT.Shared;
using WebSwIT.Shared.Options;

namespace WebSwIT.DataAccessLayer
{
    public static class StartupExtention
    {
        public static void DataAccessInitializer(this IServiceCollection service, IConfiguration configuration)
        {
            string sqlConnectionString = configuration.GetConnectionString(Constants.AppSettings.SqlServerConnection);
            string mongoConnectionString = configuration.GetConnectionString(Constants.AppSettings.MongoServerConnection);

            service.AddDbContext<ApplicationContext>(options =>
               options.UseSqlServer(sqlConnectionString, x => x.MigrationsAssembly("WebSwIT.DataAccessLayer")));

            service.AddTransient<IMongoDatabase>(options =>
            {
                MongoUrlBuilder connection = new MongoUrlBuilder(mongoConnectionString);
                MongoClient client = new MongoClient(mongoConnectionString);

                return client.GetDatabase(connection.DatabaseName);
            });

            service.Configure<AdminCredentialsOptions>(configuration.GetSection(Constants.AppSettings.AdminCredentials));
            service.AddIdentity<User, IdentityRole<Guid>>(opts =>
            {
                opts.Password.RequiredLength = 5;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;

                opts.User.RequireUniqueEmail = true;
                opts.User.AllowedUserNameCharacters = null;
            })
                .AddEntityFrameworkStores<ApplicationContext>()
                .AddTokenProvider<DataProtectorTokenProvider<User>>(TokenOptions.DefaultProvider);

            service.AddTransient<IUserRepository, UserRepository>();
            service.AddTransient<IEmployeeRepository, EmployeeRepository>();
            service.AddTransient<IEmployeeAndRoleEmployeeRepository, EmployeeAndRoleEmployeeRepository>();
            service.AddTransient<IRoleEmployeeRepository, RoleEmployeeRepository>();
            service.AddTransient<IOrderRepository, OrderRepository>();
            service.AddTransient<ICategoryRepository, CategoryRepository>();
            service.AddTransient<ITechnologyRepository, TechnologyRepository>();
            service.AddTransient<IWorkSampleRepository, WorkSampleRepository>();
            service.AddTransient<IWorkSamplePictureRepository, WorkSamplePictureRepository>();
            service.AddTransient<IMessageRepository, MessageRepository>();
            service.AddTransient<IContactRepository, ContactRepository>();
            service.AddTransient<IProposalRepository, ProposalRepository>();

            service.AddTransient<IDataSeederService, DataSeederService>();
            service.AddTransient<IFileService, FileService>();

            using (var context = service.BuildServiceProvider().GetService<ApplicationContext>())
            {
                context.Database.Migrate();
            }

            var dataSeederService = service.BuildServiceProvider().GetService<IDataSeederService>();
            dataSeederService.SeedDataAsync().Wait();
        }
    }
}
