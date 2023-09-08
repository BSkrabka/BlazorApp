using Organizer.Database.Storage.Repositories.Interfaces;
using Organizer.Database.Storage.Tables;
using Organizer.Lib.Common.Requests;
using Organizer.Lib.Common.Responses;
using Organizer.Lib.Core.Interfaces;
using Organizer.Lib.Helper.Enums;
using Organizer.Lib.Helper.Resources;

namespace Organizer.Lib.Core.Services;

public class BankAccountService : IBankAccountService
{
    private readonly IBankAccountRepository _bankAccountRepository;

    public BankAccountService(IBankAccountRepository bankAccountRepository)
    {
        _bankAccountRepository = bankAccountRepository;
    }

    public async Task<List<BankAccountResponse>> GetBankAccountsAsync(Guid userId)
    {
        var bankAccounts = await _bankAccountRepository.GetBankAccountsWithOwnerAsync(userId);

        return bankAccounts.Select(ba => new BankAccountResponse
        {
            Id = ba.Id,
            AccountNumber = ba.AccountNumber,
            Name = ba.Name,
            Value = ba.Value,
            BankAccountPermission = ba.UserBankAccounts.First(u => u.UserId == userId).BankAccountPermission
        }).ToList();
    }

    public async Task<OperationResult> CreateBankAccount(BankAccountCreateRequest request, Guid userId)
    {
        var bankAccount = new BankAccount()
        {
            AccountNumber = request.AccountNumber,
            Name = request.Name,
            Value = request.Value,
            UserBankAccounts = new List<UserBankAccount>()
        };

        var userBankAccount = new UserBankAccount()
        {
            UserId = userId,
            BankAccountPermission = EntityPermissionEnum.Owner
        };

        bankAccount.UserBankAccounts.Add(userBankAccount);

        await _bankAccountRepository.CreateAsync(bankAccount);

        return OperationResult.Ok();
    }

    public async Task<OperationResult> RemoveBankAccount(Guid bankAccountId, Guid userId)
    {
        var bankAccount = await _bankAccountRepository
            .GetAsync(ba => ba.Id == bankAccountId && ba.UserBankAccounts.Any(u =>
                u.UserId == userId && u.BankAccountPermission == EntityPermissionEnum.Owner));

        if(bankAccount == null)
            return OperationResult.Failure(Messages.UserNotFound);

        await _bankAccountRepository.DeleteAsync(bankAccount);

        return OperationResult.Ok();
    }
}