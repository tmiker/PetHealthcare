namespace PetHealthcare.API.JwtAuth
{
    public class JwtOptions
    {
        public string? JwtIssuer { get; set; }
        public string? JwtSecret { get; set; }
        public int JwtExpireMinutes { get; set; }
    }
}
