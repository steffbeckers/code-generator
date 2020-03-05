using AutoMapper;
using GraphQL;
using GraphQL.Server;
using GraphQL.Server.Authorization.AspNetCore;
using GraphQL.Server.Ui.Altair;
using GraphQL.Server.Ui.GraphiQL;
using GraphQL.Server.Ui.Playground;
using GraphQL.Server.Ui.Voyager;
using GraphQL.Validation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Test.API.BLL;
using Test.API.DAL;
using Test.API.DAL.Repositories;
using Test.API.Framework.Exceptions;
using Test.API.GraphQL;
using Test.API.Models;
using Test.API.Services;

namespace Test.API
{
    public class Startup
    {
        public IConfiguration configuration { get; }

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
		    // CORS
            services.AddCors();

            // Connection to the Test database
            services.AddDbContext<TestContext>(options =>
                options.UseSqlServer(this.configuration.GetConnectionString("TestContext")));

            // HttpContext
            services.AddHttpContextAccessor();

            // Repositories
			services.AddScoped<AccountRepository>();
			services.AddScoped<ProductRepository>();
			services.AddScoped<SupplierRepository>();
			services.AddScoped<ProductDetailRepository>();
			services.AddScoped<ProductSupplierRepository>();

			// BLLs
			services.AddScoped<AccountBLL>();
			services.AddScoped<ProductBLL>();
			services.AddScoped<SupplierBLL>();
			services.AddScoped<ProductDetailBLL>();

            // Services
            services.AddSingleton<IEmailService, EmailService>();

            // GraphQL
            services.AddScoped<IDependencyResolver>(s =>
                new FuncDependencyResolver(s.GetRequiredService));
            services.AddScoped<TestSchema>();
            services.AddGraphQL(options =>
            {
                options.EnableMetrics = true;
                options.ExposeExceptions = true; // TODO: Only in DEV?
            })
            .AddGraphTypes(ServiceLifetime.Scoped)
            .AddWebSockets();

			// AutoMapper
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

			// MVC
            services.AddControllers()
                .AddNewtonsoftJson(options => {
                    options.SerializerSettings.MaxDepth = 5;
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                }
            );

			// Swagger
			// Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Test Web API",
                    Version = "v1"
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            // Kestrel
            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            // IIS Express
            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
		    // CORS
            app.UseCors(options =>
			{
                options.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });

            //// Error handling
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            //    app.UseExceptionHandler(appBuilder =>
            //    {
            //        appBuilder.Run(async context =>
            //        {
            //            context.Response.StatusCode = 500;
            //            await context.Response.WriteAsync("An unexpected fault happened. Try again later.");
            //        });
            //    });
            //}

            // Update database migrations on startup
            UpdateDatabase(app);

            // Authentication
            app.UseAuthentication();

            // Error handling
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            app.UseRouting();

			// Authorization
            app.UseAuthorization();

            // Web sockets
            app.UseWebSockets();

            // GraphQL
            app.UseGraphQLWebSockets<TestSchema>("/graphql");
            app.UseGraphQL<TestSchema>("/graphql");
            app.UseGraphiQLServer(new GraphiQLOptions
            {
                GraphiQLPath = "/ui/graphiql",
                GraphQLEndPoint = "/graphql"
            });
            app.UseGraphQLAltair(new GraphQLAltairOptions());
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions());
            app.UseGraphQLVoyager(new GraphQLVoyagerOptions());

			// Swagger
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger()
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            .UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("./swagger/v1/swagger.json", "Test Web API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }

        private void UpdateDatabase(IApplicationBuilder app)
        {
            using (IServiceScope serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (TestContext context = serviceScope.ServiceProvider.GetService<TestContext>())
                {
                    context.Database.Migrate();
                }
            }
        }


    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
        }

        private static Task HandleException(HttpContext context, Exception ex)
        {
            HttpStatusCode code = HttpStatusCode.InternalServerError; // 500 if unexpected

            // Specify different custom exceptions here

            string result = JsonConvert.SerializeObject(new { error = ex.Message });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(result);
        }
    }
}
