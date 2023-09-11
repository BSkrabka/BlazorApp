using Organizer.Database.Storage.Tables;
using Organizer.Lib.Common.Responses;

namespace Organizer.Lib.Extension.Responses;

public static class LoanExtension
{
    public static LoanResponse ToLoanResponse(this Loan loan)
    {
        return new LoanResponse(
            id: loan.Id,
            name: loan.Name,
            accountNumber: loan.AccountNumber,
            baseValue: loan.BaseValue,
            currentValue: loan.CurrentValue,
            nextRepayment: loan.NextRepayment,
            finalRepaymentAt: loan.FinalRepaymentAt
        );
    }
}