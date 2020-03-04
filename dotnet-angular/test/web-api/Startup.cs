using AutoMapper;
using GraphQL;
using GraphQL.Server;
using GraphQL.Server.Authorization.AspNetCore;
using GraphQL.Server.Ui.Playground;
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

            // Authentication
            services.AddIdentity<User, IdentityRole<Guid>>()
                .AddRoleManager<RoleManager<IdentityRole<Guid>>>()
                .AddEntityFrameworkStores<TestContext>()
                .AddDefaultTokenProviders();

            //// Options
            services.Configure<IdentityOptions>(options =>
            {
                // Sign in
                options.SignIn.RequireConfirmedEmail = this.configuration.GetSection("Authentication").GetValue<bool>("EmailConfirmation");

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
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration.GetSection("Authentication").GetValue<string>("Secret"))),
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
            services.AddScoped<AuthBLL>();

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
            // TODO
            //.AddGraphQLAuthorization(options =>
            //{
            //    options.AddPolicy("Authorized", p => p.RequireAuthenticatedUser());
            //    //var policy = new AuthorizationPolicyBuilder()
            //    //options.AddPolicy("SteffOnly", p => p.RequireClaim(ClaimTypes.Name, "steff"));
            //})
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
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description =
                        "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Authentication / Authorization
            CreateRolesAndAdminUser(serviceProvider);
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

        private void CreateRolesAndAdminUser(IServiceProvider serviceProvider)
        {
            RoleManager<IdentityRole<Guid>> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
            UserManager<User> userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            // Roles
            Task<IdentityRole<Guid>> adminRole = roleManager.FindByNameAsync("Admin");
            adminRole.Wait();
            if (adminRole.Result == null)
            {
                IdentityRole<Guid> newAdminRole = new IdentityRole<Guid>()
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                };

                Task<IdentityResult> createAdminRole = roleManager.CreateAsync(newAdminRole);
                createAdminRole.Wait();
            }

            Task<IdentityRole<Guid>> itRole = roleManager.FindByNameAsync("IT");
            itRole.Wait();
            if (itRole.Result == null)
            {
                IdentityRole<Guid> newITRole = new IdentityRole<Guid>()
                {
                    Name = "IT",
                    NormalizedName = "IT"
                };

                Task<IdentityResult> createITRole = roleManager.CreateAsync(newITRole);
                createITRole.Wait();
            }

            Task<IdentityRole<Guid>> salesRole = roleManager.FindByNameAsync("Sales");
            salesRole.Wait();
            if (salesRole.Result == null)
            {
                IdentityRole<Guid> newSalesRole = new IdentityRole<Guid>()
                {
                    Name = "Sales",
                    NormalizedName = "SALES"
                };

                Task<IdentityResult> createSalesRole = roleManager.CreateAsync(newSalesRole);
                createSalesRole.Wait();
            }

            Task<IdentityRole<Guid>> hrRole = roleManager.FindByNameAsync("HR");
            hrRole.Wait();
            if (hrRole.Result == null)
            {
                IdentityRole<Guid> newHRRole = new IdentityRole<Guid>()
                {
                    Name = "HR",
                    NormalizedName = "HR"
                };

                Task<IdentityResult> createHRRole = roleManager.CreateAsync(newHRRole);
                createHRRole.Wait();
            }

            Task<IdentityRole<Guid>> ceoRole = roleManager.FindByNameAsync("CEO");
            ceoRole.Wait();
            if (ceoRole.Result == null)
            {
                IdentityRole<Guid> newCEORole = new IdentityRole<Guid>()
                {
                    Name = "CEO",
                    NormalizedName = "CEO"
                };

                Task<IdentityResult> createCEORole = roleManager.CreateAsync(newCEORole);
                createCEORole.Wait();
            }

            // Default admin user
            Task<User> adminUser = userManager.FindByNameAsync(configuration.GetSection("Authentication").GetSection("Admin").GetValue<string>("Username"));
            adminUser.Wait();
            if (adminUser.Result == null)
            {
                User newAdminUser = new User()
                {
                    Email = configuration.GetSection("Authentication").GetSection("Admin").GetValue<string>("Email"),
                    UserName = configuration.GetSection("Authentication").GetSection("Admin").GetValue<string>("Username"),
                    FirstName = configuration.GetSection("Authentication").GetSection("Admin").GetValue<string>("FirstName"),
                    LastName = configuration.GetSection("Authentication").GetSection("Admin").GetValue<string>("LastName"),
                    EmailConfirmed = true
                };

                Task<IdentityResult> newUser = userManager.CreateAsync(newAdminUser, configuration.GetSection("Authentication").GetSection("Admin").GetValue<string>("Password"));
                newUser.Wait();
                if (newUser.Result.Succeeded)
                {
                    // Add to the Admin role
                    Task<IdentityResult> newUserRole = userManager.AddToRoleAsync(newAdminUser, "Admin");
                    newUserRole.Wait();
                }
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
            if (ex is LoginFailedException) code = HttpStatusCode.BadRequest;
            if (ex is RegistrationFailedException) code = HttpStatusCode.BadRequest;
            if (ex is ConfirmEmailFailedException) code = HttpStatusCode.BadRequest;
            if (ex is ForgotPasswordFailedException) code = HttpStatusCode.BadRequest;
            if (ex is ResetPasswordFailedException) code = HttpStatusCode.BadRequest;

            string result = JsonConvert.SerializeObject(new { error = ex.Message });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(result);
        }
    }
}
