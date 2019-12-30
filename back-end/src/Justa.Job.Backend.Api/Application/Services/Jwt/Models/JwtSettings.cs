using System;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Justa.Job.Backend.Api.Application.Services.Jwt.Models
{
    public class JwtSettings
    {
        private SigningCredentials _signingCredentials;

        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int ValidForMinutes { get; set; }
        public string SigningKey { get; set; }
        public SigningCredentials SigningCredentials
        {
            get
            {
                if (_signingCredentials is null)
                {
                    var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SigningKey));
                    _signingCredentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256Signature);
                }

                return _signingCredentials;
            }
        }

        public DateTime IssuedAt => DateTime.UtcNow;
        public DateTime NotBefore => IssuedAt;
        public DateTime AccessTokenExpiration => IssuedAt.AddMinutes(ValidForMinutes);
    }
}