namespace TennisTournament.Options
{
    public class JwtOptions
    {
        public const string Jwt = "JwtSettings";

        public string Secret { get; set; } = string.Empty;
    }
}
