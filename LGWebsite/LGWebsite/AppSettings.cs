namespace LGWebsite
{
    public class AppSettings
    {
        public static AuthenticationConfig Authentication { get; set; }
        public static Jwt JwtConfig { get; set; }
        public class AuthenticationConfig
        {
            public string ClientId { get; set; }
            public string ClientSecret { get; set; }
        }
        public class Jwt
        {
            public string SecretKey { get; set; }
            public string Issuer { get; set; }
            public string Audience { get; set; }
        }
        public static string Endpoint { set; get; }
    }
}
