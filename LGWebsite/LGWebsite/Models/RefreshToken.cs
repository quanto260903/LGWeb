namespace LGWebsite.Models
{
    public class LoginModel
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
    public class TokenModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
    public class RefreshToken
    {
        public string Token { get; set; }
        public string ClientId { get; set; }
        public string SecretKey { get; set; }
        public DateTime Expiration { get; set; } = DateTime.UtcNow.AddDays(7); // Set to 7 days, or however long you want the refresh token to last
    }
}
