using System.Security.Claims;

namespace CoreArk.Packages.Security.Services.SecurityTokens.Interfaces
{
    public interface IJwtService
    {
        string GetJwt(string subjectId, string role, string companyId, string firstName, string lastName, string email);

        ClaimsPrincipal GetIdentity(string encodedJwt);
    }
}