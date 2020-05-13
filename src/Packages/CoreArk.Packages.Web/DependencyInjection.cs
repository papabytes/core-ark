using System;
using System.Text;
using CoreArk.Packages.Security.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace CoreArk.Packages.Web
{
    public static class DependencyInjection
    {
        public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var securityOptions = new SecurityOptions();

            configuration.GetSection("Security").Bind(securityOptions);

            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey =
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityOptions.JwtSigningKey)),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ClockSkew = TimeSpan.FromMinutes(5),
                        ValidAudience = securityOptions.Audience,
                        ValidIssuer = securityOptions.Issuer
                    };
                });
        }
    }
}