using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using BLL.App;
using Contracts.BLL.App;
using Contracts.DAL.App;
using DAL.App.EF;
using DAL.App.EF.Helpers;
using Domain.App.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebApp
{
    /// <summary>
    /// Startup
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Startup
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Startup config
        /// </summary>
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        /// <summary>
        /// Startup configure services
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
                options
                    .UseSqlServer(
                        Configuration.GetConnectionString("LocalDBConnection"))
                    .EnableDetailedErrors()
                    .EnableSensitiveDataLogging()
            );
            
            services.AddScoped<IAppUnitOfWork, AppUnitOfWork>();
            services.AddScoped<IAppBLL, AppBLL>();

            services.AddDatabaseDeveloperPageExceptionFilter();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services
                .AddAuthentication()
                .AddCookie(options => { options.SlidingExpiration = true; }
                )
                .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.SaveToken = true;
                        options.TokenValidationParameters = new TokenValidationParameters()
                        {
                            ValidIssuer = Configuration["JWT:Issuer"],
                            ValidAudience = Configuration["JWT:Issuer"],

                            IssuerSigningKey =
                                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Key"])),
                            ClockSkew = TimeSpan.Zero
                        };
                    }
                );


            services
                .AddIdentity<AppUser, AppRole>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddDefaultUI()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.AddControllersWithViews();

            services.AddCors(options =>
                {
                    options.AddPolicy("CorsAllowAll", builder =>
                    {
                        builder.AllowAnyHeader();
                        builder.AllowAnyMethod();
                        builder.AllowAnyOrigin();
                    });
                }
            );

            services.AddAutoMapper(
                typeof(DAL.App.DTO.MappingProfiles.AutoMapperProfile),
                typeof(BLL.App.DTO.MappingProfiles.AutoMapperProfile),
                typeof(PublicApi.DTO.v1.MappingProfiles.AutoMapperProfile)
            );

            // add support for api versioning
            services.AddApiVersioning(options => { options.ReportApiVersions = true; });
            // add support for m2m api documentation
            services.AddVersionedApiExplorer(options => { options.GroupNameFormat = "'v'VVV"; });
            // add support to generate human readable documentation from m2m docs
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen();


            services.Configure<RequestLocalizationOptions>(options =>
            {
                // TODO: should be in appsettings.json
                var appSupportedCultures = new[]
                {
                    new CultureInfo("et"),
                    new CultureInfo("en-GB"),
                };

                options.SupportedCultures = appSupportedCultures;
                options.SupportedUICultures = appSupportedCultures;
                options.DefaultRequestCulture = new RequestCulture("en-GB", "en-GB");
                options.SetDefaultCulture("en-GB");
                options.RequestCultureProviders = new List<IRequestCultureProvider>()
                {
                    new QueryStringRequestCultureProvider(),
                    new CookieRequestCultureProvider()
                };
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
        /// Configure startup
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="apiVersionDescriptionProvider"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            IApiVersionDescriptionProvider apiVersionDescriptionProvider)
        {
            SetupAppData(app, Configuration);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                foreach (var apiVersionDescription in apiVersionDescriptionProvider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint(
                        $"/swagger/{apiVersionDescription.GroupName}/swagger.json",
                        apiVersionDescription.GroupName.ToUpperInvariant()
                    );
                }
            });

            app.UseStaticFiles();

            app.UseCors("CorsAllowAll");

            app.UseRouting();

            app.UseRequestLocalization(
                app.ApplicationServices
                    .GetService<IOptions<RequestLocalizationOptions>>()?.Value
            );

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }

        private static void SetupAppData(IApplicationBuilder app, IConfiguration configuration)
        {
            using var serviceScope =
                app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using var ctx = serviceScope.ServiceProvider.GetService<AppDbContext>();
            if (ctx != null)
            {
                if (configuration.GetValue<bool>("AppData:DropDatabase"))
                {
                    Console.Write("Drop database");
                    DataInitializers.DeleteDatabase(ctx);
                    Console.WriteLine(" - done");
                }

                if (configuration.GetValue<bool>("AppData:Migrate"))
                {
                    Console.Write("Migrate database");
                    DataInitializers.MigrateDatabase(ctx);
                    Console.WriteLine(" - done");
                }
                if (configuration.GetValue<bool>("AppData:SeedData"))
                {
                    Console.Write("Seed database");
                    DataInitializers.SeedData(ctx);
                    Console.WriteLine(" - done");
                }
                
                if (configuration.GetValue<bool>("AppData:SeedIdentity"))
                {
                    using var userManager = serviceScope.ServiceProvider.GetService<UserManager<AppUser>>();
                    using var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<AppRole>>();

                    if (userManager != null && roleManager != null)
                    {
                        DataInitializers.SeedIdentity(userManager, roleManager, ctx);
                    }
                    else
                    {
                        Console.Write(
                            $"No user manager {(userManager == null ? "null" : "ok")} or role manager {(roleManager == null ? "null" : "ok")}!");
                    }
                }


            }

            //C# will dispose all the usings here
        }
    }
}