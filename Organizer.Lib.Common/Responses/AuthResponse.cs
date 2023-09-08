namespace Organizer.Lib.Common.Responses;

public class AuthResponse
{
    public bool IsAuthSuccessful { get; set; }
    public string Token { get; set; } = string.Empty;
    public string ErrorMessage { get; set; } = string.Empty;
}