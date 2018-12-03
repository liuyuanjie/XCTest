using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Xcelerator.Common;
using Xcelerator.Entity;
using Xcelerator.Model;
using Xcelerator.Model.View;
using Xcelerator.Service.Interfaces;

namespace Xcelerator.Service
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserService(UserManager<User> userManager, RoleManager<Role> roleManager, IPasswordHasher<User> passwordHasher)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _passwordHasher = passwordHasher;
        }

        public IEnumerable<string> GetUserPermissions(int userId)
        {
            IEnumerable<string> permissions = new List<string>();

            var groups = _userManager
                   .Users
                   .First(x => x.Id == userId)
                   .UserRoles?.Select(x =>
                       x.Role
                        ?.Claims
                        .Where(c => c.ClaimType == CustomClaimTypes.Permission)
                        .Select(y => y.ClaimValue));

            if (groups != null)
            {
                foreach (var group in groups.Where(x => x != null))
                {
                    permissions = permissions.Union(group);
                }
            }

            return permissions;
        }

        public async Task<IdentityResult> CreateAsync(RegisterViewModel registerViewModel)
        {
            return await _userManager.CreateAsync(Mapper.Map<RegisterViewModel, User>(registerViewModel), registerViewModel.Password);
        }

        public async Task<UserDTO> FindByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return null;
            }

            var userDto = Mapper.Map<User, UserDTO>(user);
            userDto.Roles = GetUserRoles(userDto.Id);
            userDto.Claims = GetUserClaims(userDto.Id);

            return userDto;
        }

        public PasswordVerificationResult VerifyHashedPassword(UserDTO userDto, string providedPassword)
        {
            var user = _userManager.Users.First(x => x.Id == userDto.Id);

            return _userManager.PasswordHasher.VerifyHashedPassword(Mapper.Map<UserDTO, User>(userDto), user.PasswordHash, providedPassword);
        }

        private IEnumerable<Claim> GetUserClaims(int userId)
        {
            return _userManager
                       .Users
                       .First(x => x.Id == userId)
                       .Claims?.Select(x => new Claim(x.ClaimType, x.ClaimValue))
                   ?? new List<Claim>();
        }

        private IEnumerable<RoleDTO> GetUserRoles(int userId)
        {
            return _userManager
                       .Users
                       .First(x => x.Id == userId)
                       .UserRoles?.Select(x => Mapper.Map<Role, RoleDTO>(x.Role))
                   ?? new List<RoleDTO>();
        }
    }
}