using Organizer.Lib.Common.Requests;
using Organizer.Lib.Common.Responses;

namespace Organizer.Lib.Core.Interfaces;

public interface IBankAccountService
{
    Task<List<BankAccountResponse>> GetBankAccountsAsync(Guid userId);
    Task<BankAccountResponse> GetBankAccountAsync(Guid userId, Guid bankId);
    Task<OperationResult> CreateBankAccount(BankAccountCreateRequest request, Guid userId);
    Task<OperationResult> RemoveBankAccount(Guid bankAccountId, Guid userId);
}