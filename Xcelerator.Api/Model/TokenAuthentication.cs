using System;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Xcelerator.Api.Model
{
    public class TokenAuthentication
    {
        public TokenAuthentication(string securityKey, string issuer, string audience, TimeSpan timeOut)
        {
            SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
            SigningCredentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);
            Issuer = issuer;
            Audience = audience;
            TimeOut = timeOut;
        }
        public SymmetricSecurityKey SecurityKey { get; }

        public string Issuer { get; }

        public string Audience { get; }

        public SigningCredentials SigningCredentials { get; }

        public TimeSpan TimeOut { get; }
    }
}
