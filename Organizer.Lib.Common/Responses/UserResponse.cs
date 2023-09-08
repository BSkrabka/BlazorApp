namespace Organizer.Lib.Common.Responses;

public class UserResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string Login { get; set; } 
    public string? Email { get; set; }
}