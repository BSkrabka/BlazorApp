namespace Organizer.Lib.Helper;

public class AppSettings
{
    public Dictionary<string, string> ConnectionStrings { get; set; } = new();
    public JwtSettings JwtSettings { get; set; } = new();
    public AdminCredentials AdminCredentials { get; set; } = new();
}

public class JwtSettings
{
    public string SecurityKey { get; set; } = string.Empty;
    public string ValidIssuer { get; set; } = string.Empty;
    public string ValidAudience { get; set; } = string.Empty;
    public int ExpiryInMinutes { get; set; }
}

public class AdminCredentials
{
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}