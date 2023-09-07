using Organizer.Lib.Helper.Enums;

namespace Organizer.Database.Storage.Tables;

public class User : BaseEntity
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string? Email { get; set; }
    public UserStatusEnum UserStatus { get; set; } = UserStatusEnum.Created;
}
