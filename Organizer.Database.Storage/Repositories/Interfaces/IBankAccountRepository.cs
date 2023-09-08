using Organizer.Database.Storage.Tables;

namespace Organizer.Database.Storage.Repositories.Interfaces;

public interface IBankAccountRepository : IBaseRepository<BankAccount>
{
    Task<List<BankAccount>> GetBankAccountsWithOwnerAsync(Guid userId);
}