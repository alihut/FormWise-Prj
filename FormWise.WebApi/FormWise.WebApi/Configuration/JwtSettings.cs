namespace FormWise.WebApi.Configuration
{
    public class JwtSettings
    {
        public string Secret { get; set; } = null!;
        public int ExpiresInMinutes { get; set; }
    }

}
