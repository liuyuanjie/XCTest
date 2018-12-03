using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Text;

namespace Xcelerator.Model
{
    public class UserDTO
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(256)]
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JobTitle { get; set; }
        public bool IsEnabled { get; set; }
        public int? OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        public IEnumerable<Claim> Claims { get; set; }
        public IEnumerable<RoleDTO> Roles { get; set; }

        public string NickName => $"{FirstName ?? string.Empty} {LastName ?? string.Empty}";
    }
}
