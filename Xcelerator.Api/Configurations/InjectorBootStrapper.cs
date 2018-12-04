using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Xcelerator.Api.Configurations.Attribute;
using Xcelerator.Api.Configurations.Authorization;
using Xcelerator.Model.ErrorHandler;
using Xcelerator.Repositories;
using Xcelerator.Repositories.Interfaces;
using Xcelerator.Service;
using Xcelerator.Service.Interfaces;

namespace Xcelerator.Api.Configurations
{
    public class InjectorBootStrapper
    {
        public static void RegisterDependencies(IServiceCollection services)
        {
            services.AddTransient<IAuditRepository, AuditRepository>();

            services.AddTransient<IAuditService, AuditService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IDatabaseInitializer, DatabaseInitializer>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddTransient<IErrorHandler, ErrorMessages>();
            services.AddSingleton<IAuthorizationHandler, ClaimsRequirementHandler>();

            services.AddScoped<ModelValidationAttribute>();
        }
    }
}
