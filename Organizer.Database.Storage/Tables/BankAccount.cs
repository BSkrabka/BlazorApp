namespace Organizer.Database.Storage.Tables;

public class BankAccount : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public double Value { get; set; }
    public long AccountNumber { get; set; }
}