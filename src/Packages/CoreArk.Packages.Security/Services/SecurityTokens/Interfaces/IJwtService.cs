namespace CoreArk.Packages.Security.Services.SecurityTokens.Interfaces
{
    public interface IJwtService
    {
        string GetJwt(string subjectId, string role, string companyId);
    }
}