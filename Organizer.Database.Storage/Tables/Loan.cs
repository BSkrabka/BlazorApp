namespace Organizer.Database.Storage.Tables;

public class Loan : BaseEntity
{
    public double BaseValue { get; set; }
    public double CurrentValue { get; set; }
    public DateTime RepaymentAt { get; set; }
    public DateTime FinalRepaymentAt { get; set; }
}