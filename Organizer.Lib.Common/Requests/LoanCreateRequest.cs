namespace Organizer.Lib.Common.Requests;

public class LoanCreateRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string AccountNumber { get; set; }

    public double BaseValue { get; set; }

    public int RepaymentDay { get; set; }
    public DateTime FinalRepaymentAt { get; set; }
}