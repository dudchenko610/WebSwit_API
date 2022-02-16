using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using WebSwIT.DataAccessLayer.AppContext;

namespace WebSwIT.DataAccessLayer.Factories
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
    {
        public ApplicationContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(@Directory.GetParent(@Directory.GetCurrentDirectory())
                + "/WebSwIT.PresentationLayer/appsettings.json").Build();
            var builder = new DbContextOptionsBuilder<ApplicationContext>();
            var connectionString = configuration.GetConnectionString("SQLServerConnection");

            builder.UseSqlServer(connectionString);

            return new ApplicationContext(builder.Options);
        }
    }
}