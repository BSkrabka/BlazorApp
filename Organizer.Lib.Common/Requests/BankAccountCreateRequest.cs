namespace Organizer.Lib.Common.Requests;

public class BankAccountCreateRequest
{
    public string Name { get; set; } = string.Empty;
    public double Value { get; set; }
    public long AccountNumber { get; set; }
}