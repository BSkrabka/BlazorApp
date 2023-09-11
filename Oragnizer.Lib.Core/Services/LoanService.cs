using Organizer.Database.Storage.Repositories.Interfaces;
using Organizer.Database.Storage.Tables;
using Organizer.Lib.Common.Requests;
using Organizer.Lib.Common.Responses;
using Organizer.Lib.Core.Interfaces;
using Organizer.Lib.Extension.Responses;

namespace Organizer.Lib.Core.Services;

public class LoanService : ILoanService
{
    private readonly ILoanRepository _loanRepository;

    public LoanService(ILoanRepository loanRepository)
    {
        _loanRepository = loanRepository;
    }

    public async Task<OperationResult> Create(LoanCreateRequest request)
    {
        var date = DateTime.UtcNow.Date.AddMonths(1);
        var nextDate = new DateTime(date.Year, date.Month, request.RepaymentDay);

        var loan = new Loan
        {
            AccountNumber = request.AccountNumber,
            Name = request.Name,
            FinalRepaymentAt = request.FinalRepaymentAt,
            BaseValue = request.BaseValue,
            CurrentValue = request.BaseValue,
            NextRepayment = nextDate
        };

        await _loanRepository.CreateAsync(loan);

        return OperationResult.Ok();
    }

    public async Task<OperationResult> Remove(Guid loanId)
    {
        var loan = await _loanRepository.GetAsync(l => l.Id == loanId);

        if (loan == null) 
            return OperationResult.Failure("Nie można znalezieźć wybranego obeiktu");

        await _loanRepository.DeleteAsync(loan);

        return OperationResult.Ok();
    }

    public async Task<List<LoanResponse>> GetAll()
    {
        var loans = await _loanRepository.GetAllAsync();

        return loans.Select(l => l.ToLoanResponse()).ToList();
    }

    public async Task<LoanResponse> GetById(Guid loanId)
    {
        var loan = await _loanRepository.GetAsync(l => l.Id == loanId);

        if (loan == null) return new LoanResponse();

        return loan.ToLoanResponse();
    }
}