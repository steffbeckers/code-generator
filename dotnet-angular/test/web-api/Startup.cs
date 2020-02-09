using AutoMapper;
using GraphQL;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
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
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using Test.API.BLL;
using Test.API.DAL;
using Test.API.DAL.Repositories;
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

            // Authentication
            services.AddIdentity<User, IdentityRole>()
                .AddRoleManager<RoleManager<IdentityRole>>()
                .AddEntityFrameworkStores<TestContext>()
                .AddDefaultTokenProviders();
            services.AddAuthentication();

            //// Options
            services.Configure<IdentityOptions>(options =>
            {
                // Sign in
                options.SignIn.RequireConfirmedEmail = false; // ANONYMOUS

                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 10;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredUniqueChars = 6;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = true;
            });

            //// JWT's
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("Authentication").GetValue<string>("Secret"))),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero,
                    ValidateLifetime = true
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        // Allow the access token to be set by query param
                        if (context.Request.Method.Equals("GET") && context.Request.Query.ContainsKey("access_token"))
                            context.Token = context.Request.Query["access_token"];

                        return Task.CompletedTask;
                    }
                };
            });

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
                options.ExposeExceptions = true; // TODO: Only in DEV
            }).AddGraphTypes(ServiceLifetime.Scoped)
            .AddUserContextBuilder(httpContext => httpContext.User)
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
                    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
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
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
		    // CORS
            app.UseCors(options =>
			{
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

            // Web sockets
            app.UseWebSockets();

            // GraphQL
            app.UseGraphQLWebSockets<TestSchema>("/graphql");
            app.UseGraphQL<TestSchema>();
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions());

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

            // Authentication
            app.UseAuthentication();

            app.UseRouting();

			// Authorization
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
