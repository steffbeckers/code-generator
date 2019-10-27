using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using Test.API.BLL;
using Test.API.DAL;
using Test.API.DAL.Repositories;

namespace Test.API
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
		    // CORS
            services.AddCors();

            // Connection to the Test database
            services.AddDbContext<TestContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("TestContext")));

            // Repositories
			services.AddScoped<AccountRepository>();
			services.AddScoped<ContactRepository>();
			services.AddScoped<CallRepository>();
			services.AddScoped<NoteRepository>();
			services.AddScoped<DocumentRepository>();

			// BLLs
			services.AddScoped<AccountBLL>();
			services.AddScoped<ContactBLL>();
			services.AddScoped<CallBLL>();
			services.AddScoped<NoteBLL>();
			services.AddScoped<DocumentBLL>();

			// AutoMapper
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
		    // CORS
            app.UseCors(options => {
                options.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("An unexpected fault happened. Try again later.");
                    });
                });
            }

			// Authentication and Authorization
            app.UseAuthorization();

			// MVC
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
