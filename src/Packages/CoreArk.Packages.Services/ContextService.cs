using System;
using System.Linq;
using System.Security.Claims;
using CoreArk.Packages.Common.Enums;
using Microsoft.AspNetCore.Http;

namespace CoreArk.Packages.Services
{
    public class ContextService : IContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        private const string CompanyClaim = "company";

        public ContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid? UserId
        {
            get
            {
                var idClaim =
                    _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(
                        c => c.Type == ClaimTypes.NameIdentifier);

                if (idClaim != null && Guid.TryParse(idClaim.Value, out var id))
                {
                    return id;
                }

                return null;
            }
        }

        public UserRole? UserRole
        {
            get
            {
                var roleClaim =
                    _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
                
                if (roleClaim != null && Enum.TryParse(roleClaim.Value, out UserRole id))
                {
                    return id;
                }

                return null;
            }
        }

        public Guid? CompanyId
        {
            get
            {
                var roleClaim =
                    _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == CompanyClaim);
                
                if (roleClaim != null && Guid.TryParse(roleClaim.Value, out Guid id))
                {
                    return id;
                }

                return null;
            }
        }
    }
}