namespace FormWise.WebApi.Configuration
{
    public class DefaultUser
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }
    }
}
