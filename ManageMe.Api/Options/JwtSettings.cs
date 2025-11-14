namespace ManageMe.Api.Options;

public class JwtSettings
{
    public static string Option = nameof(JwtSettings);

    public string Issuer { get; set; }

    public string Audience { get; set; }

    public string Secret { get; set; }
}
