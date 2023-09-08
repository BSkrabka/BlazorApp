using Organizer.Lib.Helper.Enums;

namespace Organizer.Lib.Common.Responses;

public class BankAccountResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Value { get; set; }
    public long AccountNumber { get; set; }
    public EntityPermissionEnum BankAccountPermission { get; set; }
}