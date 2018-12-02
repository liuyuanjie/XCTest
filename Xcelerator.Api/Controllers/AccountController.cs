using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Xcelerator.Api.Model;
using Xcelerator.Api.Model.View;
using Xcelerator.Common;
using Xcelerator.Data.Entity;
using Xcelerator.Entity;
using Xcelerator.Server.Interfaces;

namespace Xcelerator.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher;
        private readonly TokenAuthentication _tokenAuthentication;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            IPasswordHasher<ApplicationUser> passwordHasher,
            TokenAuthentication tokenAuthentication)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _passwordHasher = passwordHasher;
            _tokenAuthentication = tokenAuthentication;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Create([FromBody] RegisterViewModel model)
        {
            var user = new ApplicationUser { UserName = model.UserName, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.ToString());
            }

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Token([FromBody] LoginViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null || _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, model.Password) != PasswordVerificationResult.Success)
            {
                return BadRequest();
            }

            var token = await GetJwtSecurityToken(user);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });
        }

        /// <summary>
        /// Generate JWT Token based on valid User
        /// </summary>
        private async Task<JwtSecurityToken> GetJwtSecurityToken(ApplicationUser user)
        {
            user = _userManager.Users.Single(x => x.Id == user.Id);
            var roleNames = await _userManager.GetRolesAsync(user);
            var permissionNames = new List<string>();
            //user.Roles
            //    .ToList()
            //    .ForEach(x =>
            //        x.Role
            //         .Claims
            //         .Where(c => c.ClaimType == CustomClaimTypes.Permission)
            //         .ToList()
            //         .ForEach(f =>
            //         {
            //             if (permissionNames.All(p => p != f.ClaimValue))
            //             {
            //                 permissionNames.Add(f.ClaimValue);
            //             }
            //         }));

            var claims = GetTokenClaims(user)
                //.Union(user.Claims.Select(x => new Claim(x.ClaimType, x.ClaimValue)))
                .Union(roleNames.Select(x => new Claim(JwtClaimTypes.Role, x)))
                .Union(permissionNames.Select(x => new Claim(CustomClaimTypes.Permission, x)));

            return new JwtSecurityToken(
                        _tokenAuthentication.Issuer,
                        _tokenAuthentication.Audience,
                        claims,
                        expires: DateTime.UtcNow.Add(_tokenAuthentication.TimeOut),
                        signingCredentials: _tokenAuthentication.SigningCredentials
            );
        }

        private static IEnumerable<Claim> GetTokenClaims(ApplicationUser user)
        {
            //ClaimTypes
            return new List<Claim>
            {
                new Claim(JwtClaimTypes.JwtId, Guid.NewGuid().ToString()),
                new Claim(JwtClaimTypes.Subject, user.UserName),
                new Claim(JwtClaimTypes.Name, user.UserName),
                //new Claim(JwtClaimTypes.NickName, user.FriendlyName),
                new Claim(JwtClaimTypes.Email, user.NormalizedEmail)
            };
        }
    }
}