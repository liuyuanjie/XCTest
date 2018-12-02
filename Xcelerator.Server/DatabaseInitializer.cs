using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Xcelerator.Common;
using Xcelerator.Common.Permissions;
using Xcelerator.Data;
using Xcelerator.Data.Entity;
using Xcelerator.Entity;
using Xcelerator.Server.Interfaces;

namespace Xcelerator.Server
{
    public class DatabaseInitializer : IDatabaseInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly ILogger _logger;

        public DatabaseInitializer(ApplicationDbContext context, UserManager<User> userManager, RoleManager<Role> roleManager, ILogger<DatabaseInitializer> logger)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        public async Task SeedAsync()
        {
            //await _context.Database.MigrateAsync().ConfigureAwait(false);
            if (!_userManager.Users.Any())
            {
                _logger.LogInformation("Generating inbuilt accounts");

                const string clientRoleName = "Client";
                const string chiefAuditorRoleName = "ChiefAutditor";
                const string auditFacilitatorRoleName = "AuditFacilitator";
                const string adminRoleName = "STPSystemAdmin";
                const string techRoleName = "STPAMTech";

                await EnsureRoleAsync(adminRoleName, "STPSystemAdmin", ApplicationPermissionHelper.GetAllSTPSystemAdminValues());
                await EnsureRoleAsync(clientRoleName, "Client", ApplicationPermissionHelper.GetClientPermissionValues());
                await EnsureRoleAsync(chiefAuditorRoleName, "ChiefAutditor", ApplicationPermissionHelper.GetChiefAutditorPermissionValues());
                await EnsureRoleAsync(auditFacilitatorRoleName, "AuditFacilitator", ApplicationPermissionHelper.GetAuditFacilitatorPermissionValues());
                await EnsureRoleAsync(techRoleName, "STPAMTech", new string[] { });

                await CreateUserAsync("STPSystemAdmin", "Stp123$", "admin@stp.com", "+1 (123) 000-0000", new string[] { adminRoleName });
                await CreateUserAsync("Client", "Stp123$", "client@stp.com", "+1 (123) 000-0001", new string[] { clientRoleName });
                await CreateUserAsync("ChiefAutditor", "Stp123$", "chief@stp.com", "+1 (123) 000-0001", new string[] { chiefAuditorRoleName });
                await CreateUserAsync("AuditFacilitator", "Stp123$", "facilitator@stp.com", "+1 (123) 000-0001", new string[] { auditFacilitatorRoleName });
                await CreateUserAsync("STPAMTech", "Stp123$", "techer@stp.com", "+1 (123) 000-0001", new string[] { techRoleName });

                _logger.LogInformation("Inbuilt account generation completed");
            }
        }

        private async Task EnsureRoleAsync(string roleName, string description, string[] claims)
        {
            if ((await _roleManager.FindByNameAsync(roleName)) == null)
            {
                Role role = new Role(roleName, description)
                {
                    Claims = claims
                        .Select(x => new IdentityRoleClaim<int>
                        {
                            ClaimType = CustomClaimTypes.Permission,
                            ClaimValue = x
                        })
                        .ToList()
                };

                var result = await _roleManager.CreateAsync(role);

                if (!result.Succeeded)
                {
                    throw new Exception($"Seeding \"{description}\" role failed. Errors: {result}");
                }
            }
        }

        private async Task CreateUserAsync(string userName, string password, string email, string phoneNumber, string[] roles)
        {
            User user = new User
            {
                UserName = userName,
                Email = email,
                PhoneNumber = phoneNumber,
                EmailConfirmed = true,
                IsEnabled = true,
                Roles = _roleManager
                    .Roles
                    .Where(x => roles.Contains(x.Name))
                    .Select(x => new UserRole
                    {
                        RoleId = x.Id
                    })
                    .ToList()
            };

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                throw new Exception($"Seeding \"{email}\" role failed. Errors: {result}");
            }
        }
    }
}
