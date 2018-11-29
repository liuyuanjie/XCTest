using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Xcelerator.Common;
using Xcelerator.Data;
using Xcelerator.Data.Entity;
using Xcelerator.Model;
using Xcelerator.Repositories;
using Xcelerator.Repositories.Interfaces;
using Xcelerator.Server;
using Xcelerator.Server.Interface;
using Xcelerator.Server.Interfaces;

namespace Xcelerator
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            services.Configure<AppConfiguration>(Configuration.GetSection("AppConfiguration"));
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            services.AddCors();
            services.AddAuthorization(cfg =>
            {
                cfg.AddPolicy("RequiredAuditEdit", p => p.RequireClaim(CustomClaimTypes.Permission, Permissions.CreateAuditQuestionOperationName));
            });

            ConfigureServicesJwt(services, Configuration);

            RegisterDependencies(services);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }

        private static void ConfigureServicesJwt(IServiceCollection services, IConfiguration configureation)
        {
            var key = configureation.GetSection("AppConfiguration:Key").Value;
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(jwtBearerOptions =>
                {
                    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        //NameClaimType = JwtClaimTypes.Name,
                        //RoleClaimType = JwtClaimTypes.Role,
                        RequireExpirationTime = true,
                        ValidateLifetime = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configureation.GetSection("AppConfiguration:SiteUrl").Value,
                        ValidAudience = configureation.GetSection("AppConfiguration:SiteUrl").Value,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                    };
                });
        }

        private static void RegisterDependencies(IServiceCollection services)
        {
            services.AddTransient<IAccountManager, AccountManager>();
            services.AddTransient<IAuditRepository, AuditRepository>();
            services.AddTransient<IDatabaseInitializer, DatabaseInitializer>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}