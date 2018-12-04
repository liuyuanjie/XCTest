using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Xcelerator.Api.Model;
using Xcelerator.Common;
using Xcelerator.Model;
using Xcelerator.Model.ErrorHandler;
using Xcelerator.Model.View;
using Xcelerator.Service.Interfaces;

namespace Xcelerator.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly TokenAuthentication _tokenAuthentication;
        private readonly IErrorHandler _errorHandler;
        private readonly IUserService _userService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(
            IUserService userService,
            TokenAuthentication tokenAuthentication,
            IErrorHandler errorHandler,
            ILogger<AccountController> logger)
        {
            _userService = userService;

            _tokenAuthentication = tokenAuthentication;
            _errorHandler = errorHandler;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            var result = await _userService.CreateAsync(model);

            if (!result.Succeeded)
            {
                return BadRequest(_errorHandler.GetCustomException(ErrorCode.FailedToRegister).ResponeMessage);
            }

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            var user = await _userService.FindByEmailAsync(model.Email);

            if (user == null)
            {
                throw _errorHandler.GetCustomException(ErrorCode.InvalidEmail);
                return BadRequest(_errorHandler.GetCustomException(ErrorCode.InvalidEmail).ResponeMessage);
            }

            if (_userService.VerifyHashedPassword(user, model.Password) != PasswordVerificationResult.Success)
            {
                return BadRequest(_errorHandler.GetCustomException(ErrorCode.FailedToLogin).ResponeMessage);
            }

            var token = GetJwtSecurityToken(user);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });
        }

        /// <summary>
        /// Generate JWT Token based on valid User
        /// </summary>
        private JwtSecurityToken GetJwtSecurityToken(UserDTO user)
        {
            var permissionClaims = _userService.GetUserPermissions(user.Id);

            var claims = GetTokenClaims(user)
                .Union(user.Claims)
                .Union(user.Roles.Select(x => new Claim(JwtClaimTypes.Role, x.Name)))
                .Union(permissionClaims.Select(x => new Claim(CustomClaimTypes.Permission, x)));

            return new JwtSecurityToken(
                        _tokenAuthentication.Issuer,
                        _tokenAuthentication.Audience,
                        claims,
                        expires: DateTime.UtcNow.Add(_tokenAuthentication.TimeOut),
                        signingCredentials: _tokenAuthentication.SigningCredentials
            );
        }

        private static IEnumerable<Claim> GetTokenClaims(UserDTO user)
        {
            //ClaimTypes
            return new List<Claim>
            {
                new Claim(JwtClaimTypes.JwtId, Guid.NewGuid().ToString()),
                new Claim(JwtClaimTypes.Subject, user.UserName),
                new Claim(JwtClaimTypes.Name, user.UserName),
                new Claim(JwtClaimTypes.NickName, user.NickName),
                new Claim(JwtClaimTypes.Email, user.Email)
            };
        }
    }
}