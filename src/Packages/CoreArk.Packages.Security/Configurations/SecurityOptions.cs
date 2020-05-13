namespace CoreArk.Packages.Security.Configurations
{
    public class SecurityOptions
    {
        public string JwtSigningKey { get; set; }

        public string Audience { get; set; }

        public string Issuer { get; set; }
        
        public int JwtLifetimeInSeconds { get; set; }
    }
}