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
        private readonly IAccountManager _accountManager;
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher;
        private readonly TokenAuthentication _tokenAuthentication;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            IAccountManager accountManager,
            IPasswordHasher<ApplicationUser> passwordHasher,
            TokenAuthentication tokenAuthentication)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _accountManager = accountManager;
            _passwordHasher = passwordHasher;
            _tokenAuthentication = tokenAuthentication;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Create([FromBody] RegisterViewModel model)
        {
            var user = new ApplicationUser { UserName = model.UserName, Email = model.Email };
            var result = await _accountManager.CreateUserAsync(user, new string[] { }, model.Password);

            if (!result.Item1)
            {
                return BadRequest(result.Item2);
            }

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Token([FromBody] LoginViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Login)??
                       await _userManager.FindByNameAsync(model.Login);

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
            user = await _accountManager.GetUserLoadRelatedAsync(user.NormalizedUserName);
            var role = await _accountManager.GetRoleByIdAsync(user.Roles.First().RoleId.ToString());
            var claims = GetTokenClaims(user)
                .Union(user.Claims.Select(x => new Claim(x.ClaimType, x.ClaimValue)))
                .Union(_userManager.GetRolesAsync(user).Result.Select(x => new Claim(JwtClaimTypes.Role, x)))
                .Union(_roleManager.GetClaimsAsync(role).Result.Select(x => new Claim(x.Type, x.Value)));

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