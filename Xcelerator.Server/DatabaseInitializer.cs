using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Xcelerator.Common;
using Xcelerator.Data;
using Xcelerator.Data.Entity;
using Xcelerator.Server.Interface;
using Xcelerator.Server.Interfaces;

namespace Xcelerator.Server
{
    public class DatabaseInitializer : IDatabaseInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly IAccountManager _accountManager;
        private readonly ILogger _logger;

        public DatabaseInitializer(ApplicationDbContext context, IAccountManager accountManager, ILogger<DatabaseInitializer> logger)
        {
            _context = context;
            _accountManager = accountManager;
            _logger = logger;
        }

        public async Task SeedAsync()
        {
            await _context.Database.MigrateAsync().ConfigureAwait(false);
            if (!await _accountManager.AnyUserAsync(null))
            {
                _logger.LogInformation("Generating inbuilt accounts");

                const string clientRoleName = "Client";
                const string chiefAuditorRoleName = "ChiefAutditor";
                const string auditFacilitatorRoleName = "AuditFacilitator";
                const string adminRoleName = "STPSystemAdmin";
                const string techRoleName = "STPAMTech";

                await EnsureRoleAsync(adminRoleName, "STPSystemAdmin", ApplicationPermissions.GetAllSTPSystemAdminValues());
                await EnsureRoleAsync(clientRoleName, "Client", ApplicationPermissions.GetClientPermissionValues());
                await EnsureRoleAsync(chiefAuditorRoleName, "ChiefAutditor", ApplicationPermissions.GetChiefAutditorPermissionValues());
                await EnsureRoleAsync(auditFacilitatorRoleName, "AuditFacilitator", ApplicationPermissions.GetAuditFacilitatorPermissionValues());
                await EnsureRoleAsync(techRoleName, "STPAMTech", new string[] { });

                await CreateUserAsync("STPSystemAdmin", "Stp123$", "STP System Admin", "admin@stp.com", "+1 (123) 000-0000", new string[] { adminRoleName });
                await CreateUserAsync("Client", "Stp123$", "Client", "client@stp.com", "+1 (123) 000-0001", new string[] { clientRoleName });
                await CreateUserAsync("ChiefAutditor", "Stp123$", "Chief Autditor", "chief@stp.com", "+1 (123) 000-0001", new string[] { chiefAuditorRoleName });
                await CreateUserAsync("AuditFacilitator", "Stp123$", "Audit Facilitator", "facilitator@stp.com", "+1 (123) 000-0001", new string[] { auditFacilitatorRoleName });
                await CreateUserAsync("STPAMTech", "Stp123$", "STP AM / Tech", "techer@stp.com", "+1 (123) 000-0001", new string[] { techRoleName });

                _logger.LogInformation("Inbuilt account generation completed");
            }
        }

        private async Task EnsureRoleAsync(string roleName, string description, string[] claims)
        {
            if ((await _accountManager.GetRoleByNameAsync(roleName)) == null)
            {
                ApplicationRole applicationRole = new ApplicationRole(roleName, description);

                var result = await _accountManager.CreateRoleAsync(applicationRole, claims);

                if (!result.Item1)
                {
                    throw new Exception($"Seeding \"{description}\" role failed. Errors: {string.Join(Environment.NewLine, result.Item2)}");
                }
            }
        }

        private async Task<ApplicationUser> CreateUserAsync(string userName, string password, string fullName, string email, string phoneNumber, string[] roles)
        {
            ApplicationUser applicationUser = new ApplicationUser
            {
                UserName = userName,
                FullName = fullName,
                Email = email,
                PhoneNumber = phoneNumber,
                EmailConfirmed = true,
                IsEnabled = true
            };

            var result = await _accountManager.CreateUserAsync(applicationUser, roles, password);

            if (!result.Item1)
            {
                throw new Exception($"Seeding \"{userName}\" user failed. Errors: {string.Join(Environment.NewLine, result.Item2)}");
            }

            return applicationUser;
        }
    }
}
