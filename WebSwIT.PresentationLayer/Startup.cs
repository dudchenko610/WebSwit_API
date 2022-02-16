using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using WebSwIT.PresentationLayer.Extentions;
using WebSwIT.PresentationLayer.Hubs;
using WebSwIT.PresentationLayer.Providers;
using WebSwIT.Shared;
using WebSwIT.Shared.Options;

namespace WebSwIT.PresentationLayer
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            BusinessLogicLayer.StartupExtention.BusinessLogicInitializer(services, Configuration);

            services.Configure<JwtConnectionOptions>(Configuration.GetSection(Constants.AppSettings.JwtConfiguration));
            services.Configure<StringConnectionOptions>(Configuration.GetSection(Constants.AppSettings.SqlServerConnection));
            services.Configure<EmailConnectionOptions>(Configuration.GetSection(Constants.AppSettings.EmailConfiguration));
            services.Configure<ClientConnectionOptions>(Configuration.GetSection(Constants.AppSettings.ClientConnection));

            services.AddControllers().AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddSingleton<IUserIdProvider, NameUserIdProvider>();

            services.AddSignalR();
            services.AddCors();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebSwIT.PresentationLayer", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebSwIT.PresentationLayer v1"));

            app.ConfigureExceptionHandler();

            app.UseStaticFiles();
            app.UseDefaultFiles();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(builder =>
            {
                builder.WithOrigins("http://localhost:4200", "https://websvit-angular-client.azurewebsites.net")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.ConfigureLogHandler();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<MessageHub>(Constants.Routes.CHATTING);
            });
        }
    }
}
