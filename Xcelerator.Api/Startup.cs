using System;
using System.IO;
using IdentityModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Xcelerator.Api.Configurations.Attribute;
using Xcelerator.Api.Configurations.Authorization;
using Xcelerator.Api.Configurations.Middlewares;
using Xcelerator.Api.Model;
using Xcelerator.Data;
using Xcelerator.Data.Entity;
using Xcelerator.Entity;
using Xcelerator.Repositories;
using Xcelerator.Repositories.Interfaces;
using Xcelerator.Server;
using Xcelerator.Server.Interfaces;

namespace Xcelerator.Api
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
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            ConfigureAuthorization(services);
            ConfigureAuthentication(services, Configuration);
            ConfigureCors(services);
            RegisterDependencies(services);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            app.UseMvc();
        }

        private static void ConfigureAuthorization(IServiceCollection services)
        {
            services.AddAuthorization(cfg =>
            {
                cfg.AddPolicy(Policies.RequiredAuditEditPolicy, Policies.HasRequiredAuditEdit);
            });
        }

        private static void ConfigureAuthentication(IServiceCollection services, IConfiguration configureation)
        {
            var tokenAuth = new TokenAuthentication(
                configureation["Authentication:JwtBearer:SecurityKey"],
                configureation["Authentication:JwtBearer:Issuer"],
                configureation["Authentication:JwtBearer:Audience"],
                new TimeSpan(0, int.Parse(configureation["Authentication:JwtBearer:TimeoutMinutes"]), 0));

            services
                .AddSingleton(tokenAuth)
                .Configure<IdentityOptions>(options =>
                {
                    options.ClaimsIdentity.UserNameClaimType = JwtClaimTypes.Name;
                    options.ClaimsIdentity.UserIdClaimType = JwtClaimTypes.Subject;
                    options.ClaimsIdentity.RoleClaimType = JwtClaimTypes.Role;
                })
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
                        RequireExpirationTime = true,
                        ValidateLifetime = true,

                        ValidateIssuer = true,
                        ValidIssuer = tokenAuth.Issuer,

                        ValidateAudience = true,
                        ValidAudience = tokenAuth.Audience,

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = tokenAuth.SecurityKey
                    };
                });
        }

        private static void ConfigureCors(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });
        }

        private static void RegisterDependencies(IServiceCollection services)
        {
            services.AddSingleton<IAuthorizationHandler, ClaimsRequirementHandler>();
            services.AddTransient<IAuditRepository, AuditRepository>();
            services.AddTransient<IDatabaseInitializer, DatabaseInitializer>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ModelValidationAttribute>();
        }
    }
}