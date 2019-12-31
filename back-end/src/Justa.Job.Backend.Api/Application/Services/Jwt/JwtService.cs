using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using Justa.Job.Backend.Api.Application.Services.Jwt.Interfaces;
using Justa.Job.Backend.Api.Application.Services.Jwt.Models;
using Justa.Job.Backend.Api.Identity.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Justa.Job.Backend.Api.Application.Services.Jwt
{
    public class JwtService : IJwtService
    {
        private readonly JwtSettings _jwtSetting;

        public JwtService(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSetting = jwtSettings.Value;
        }

        public JsonWebToken CreateJsonWebToken(ApplicationUser user)
        {
            var identity = GetClaimsIdentity(user);
            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Subject = identity,
                Issuer = _jwtSetting.Issuer,
                Audience = _jwtSetting.Audience,
                IssuedAt = _jwtSetting.IssuedAt,
                NotBefore = _jwtSetting.NotBefore,
                Expires = _jwtSetting.AccessTokenExpiration,
                SigningCredentials = _jwtSetting.SigningCredentials
            });

            var accessToken = handler.WriteToken(securityToken);

            return new JsonWebToken
            {
                AccessToken = accessToken,
                ExpiresIn = (long)TimeSpan.FromMinutes(_jwtSetting.ValidForMinutes).TotalSeconds
            };
        }

        private ClaimsIdentity GetClaimsIdentity(ApplicationUser user)
        {
            var identity = new ClaimsIdentity
            (
                new GenericIdentity(user.Email),
                new[] {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName)
                }
            );

            return identity;
        }
    }
}