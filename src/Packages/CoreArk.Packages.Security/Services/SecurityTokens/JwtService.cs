using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using CoreArk.Packages.Security.Configurations;
using CoreArk.Packages.Security.Services.SecurityTokens.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CoreArk.Packages.Security.Services.SecurityTokens
{
    public class JwtService : IJwtService
    {
        private readonly SecurityOptions _securityOptions;

        public JwtService(IOptions<SecurityOptions> securityOptions)
        {
            _securityOptions = securityOptions.Value;
        }

        public string GetJwt(string subjectId, string role, string companyId, string firstName, string lastName, string email)
        {
            var jwtHandler = new JwtSecurityTokenHandler();

            var claims = new List<Claim>
            {
                new Claim("sub", subjectId),
                new Claim(ClaimTypes.Role, role),
                new Claim("company", companyId),
                new Claim(ClaimTypes.GivenName, firstName),
                new Claim(ClaimTypes.Surname, lastName),
                new Claim(ClaimTypes.Email, email)
            };

            var jwt = new JwtSecurityToken(
                issuer: _securityOptions.Issuer,
                audience: _securityOptions.Audience,
                claims: claims,
                DateTime.UtcNow,
                DateTime.UtcNow.AddSeconds(_securityOptions.JwtLifetimeInSeconds),
                new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_securityOptions.JwtSigningKey)),
                    SecurityAlgorithms.HmacSha256Signature)
            );
            
            return jwtHandler.WriteToken(jwt);
        }

        public ClaimsPrincipal GetIdentity(string encodedJwt)
        {
            var token = new JwtSecurityToken(encodedJwt);

            var firstName = token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName)?.Value;
            var lastName = token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Surname)?.Value;
            
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, $"{firstName} {lastName}"),
                new Claim(ClaimTypes.Email, token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value),
                new Claim(ClaimTypes.Actor, token.Claims.FirstOrDefault(c => c.Type == "sub")?.Value),
                new Claim(ClaimTypes.Role, token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value)
            }, "Oauth");
            
            return new ClaimsPrincipal(identity);
        }
    }
}