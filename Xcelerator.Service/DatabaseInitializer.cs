using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Xcelerator.Common;
using Xcelerator.Data;
using Xcelerator.Entity;
using Xcelerator.Model.Permissions;
using Xcelerator.Repositories.Interfaces;
using Xcelerator.Service.Interfaces;

namespace Xcelerator.Service
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
            await _context.Database.MigrateAsync().ConfigureAwait(false);
            if (!_userManager.Users.Any())
            {
                _logger.LogInformation("Generating inbuilt accounts");

                string clientRoleName = "Client";
                string chiefAuditorRoleName = "ChiefAutditor";
                string auditFacilitatorRoleName = "AuditFacilitator";
                string adminRoleName = "STPSystemAdmin";
                string techRoleName = "STPAMTech";
                string organizationName = "STP";

                var organizationId = await EnsureOrganizationAsync(organizationName);
                await EnsureRoleAsync(adminRoleName, "STPSystemAdmin", ApplicationPermissionHelper.GetAllSTPSystemAdminValues());
                await EnsureRoleAsync(clientRoleName, "Client", ApplicationPermissionHelper.GetClientPermissionValues());
                await EnsureRoleAsync(chiefAuditorRoleName, "ChiefAutditor", ApplicationPermissionHelper.GetChiefAutditorPermissionValues());
                await EnsureRoleAsync(auditFacilitatorRoleName, "AuditFacilitator", ApplicationPermissionHelper.GetAuditFacilitatorPermissionValues());
                await EnsureRoleAsync(techRoleName, "STPAMTech", new string[] { });

                await CreateUserAsync(null, "STPSystemAdmin", "Stp123$", "admin@stp.com", "+1 (123) 000-0000", new string[] { adminRoleName });
                await CreateUserAsync(organizationId, "Client", "Stp123$", "client@stp.com", "+1 (123) 000-0001", new string[] { clientRoleName });
                await CreateUserAsync(organizationId, "ChiefAutditor", "Stp123$", "chief@stp.com", "+1 (123) 000-0001", new string[] { chiefAuditorRoleName });
                await CreateUserAsync(organizationId, "AuditFacilitator", "Stp123$", "facilitator@stp.com", "+1 (123) 000-0001", new string[] { auditFacilitatorRoleName });
                await CreateUserAsync(null, "STPAMTech", "Stp123$", "techer@stp.com", "+1 (123) 000-0001", new string[] { techRoleName });

                var templateId = await CreateTempateAsync(organizationId, "Test Template");
                await CreateAuditAsync(organizationId, templateId, "Test Addit");

                _logger.LogInformation("Inbuilt account generation completed");
            }
        }

        private async Task CreateAuditAsync(int organizationId, int templateId, string name)
        {
            _context.Audits.Add(new Audit { OrganizationId = organizationId, TemplateId = templateId, Name = name });
            await _context.SaveChangesAsync();
        }

        private async Task<int> CreateTempateAsync(int organizationId, string name)
        {
            var template = new Template { OrganizationId = organizationId, Name = name };
            _context.Templates.Add(template);
            await _context.SaveChangesAsync();

            return template.Id;
        }

        private async Task<int> EnsureOrganizationAsync(string organizationName)
        {
            var organization = new Organization { Name = organizationName, SecurityStamp = Guid.NewGuid().ToString() };
            _context.Organizations.Add(organization);
            await _context.SaveChangesAsync();

            return organization.Id;
        }

        private async Task EnsureRoleAsync(string roleName, string description, string[] claims)
        {
            if ((await _roleManager.FindByNameAsync(roleName)) == null)
            {
                Role role = new Role(roleName, description)
                {
                    Claims = claims
                        .Select(x => new RoleClaim
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

        private async Task CreateUserAsync(int? organizationId, string userName, string password, string email, string phoneNumber, string[] roles)
        {
            User user = new User
            {
                OrganizationId = organizationId,
                UserName = userName,
                Email = email,
                PhoneNumber = phoneNumber,
                EmailConfirmed = true,
                IsEnabled = true,
                UserRoles = _roleManager
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
