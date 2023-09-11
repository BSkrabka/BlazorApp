namespace Organizer.Lib.Common.Responses;

public class LoanResponse
{
    public LoanResponse(Guid id, string name, string accountNumber, double baseValue, double currentValue, DateTime nextRepayment, DateTime finalRepaymentAt)
    {
        Id = id;
        Name = name;
        AccountNumber = accountNumber;
        BaseValue = baseValue;
        CurrentValue = currentValue;
        NextRepayment = nextRepayment;
        FinalRepaymentAt = finalRepaymentAt;
    }

    public LoanResponse()
    {
        
    }

    public Guid Id { get; set; }
    public string Name{ get; set; }
    public string AccountNumber { get; set; }

    public double BaseValue { get; set; }
    public double CurrentValue { get; set; }
    public DateTime NextRepayment { get; set; }
    public DateTime FinalRepaymentAt { get; set; }
}