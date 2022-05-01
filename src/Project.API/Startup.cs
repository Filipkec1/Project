using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Project.Api.Extensions;
using Project.API.Middlewares;
using Project.Core.Constants;
using Project.Core.Settings;
using Project.Core.UnitsOfWork;
using Project.Infrastructure.EfModels;
using Project.Infrastructure.EfUnitsOfWork;
using System.Reflection;

namespace Project.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddServicesToDependencyInjection();
            services.AddSettingsToDependencyInjection(Configuration);
            services.AddValidatorsToDependencyInjection();
            services.AddControllersWithViews();

            services.AddDbContext<ProjectDbContext>(builder =>
            {
                builder.UseSqlServer(CreateConnectionString());
                builder.EnableSensitiveDataLogging(false);
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Project.API", Version = "v1" });
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Project.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        /// <summary>
        /// Used to create the connection string for the database.
        /// </summary>
        /// <returns>
        /// Set connection string.
        /// </returns>
        private string CreateConnectionString()
        {
            DatabaseSettings databaseSettings = Configuration.GetSection(ProjectConstants.DatabaseSection).Get<DatabaseSettings>();

            string connectionString = "Server={Host};Database={Database};User ID={User};Password={Password};{AdditionalOptions}";

            PropertyInfo[] properties = databaseSettings.GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                connectionString = connectionString.Replace("{" + property.Name + "}", (string)property.GetValue(databaseSettings, null));
            }

            return connectionString;
        }
    }
}
