using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Linq;
using Xcelerator.Entity;
using Xcelerator.Model;
using Xcelerator.Model.View;

namespace Xcelerator.Service.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> FindByEmailAsync(string email);
        IEnumerable<string> GetUserPermissions(int userId);
        Task<IdentityResult> CreateAsync(RegisterViewModel registerViewModel);
        PasswordVerificationResult VerifyHashedPassword(UserDTO userDto, string providedPassword);
    }
}
