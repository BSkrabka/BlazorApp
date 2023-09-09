using Microsoft.EntityFrameworkCore;
using Organizer.Database.Storage.Repositories.Interfaces;
using Organizer.Database.Storage.Tables;

namespace Organizer.Database.Storage.Repositories;

public class BankAccountRepository : BaseRepository<BankAccount>, IBankAccountRepository
{
    public BankAccountRepository(OrganizerDbContext context) : base(context)
    {
    }

    public async Task<List<BankAccount>> GetBankAccountsWithOwnerAsync(Guid userId)
    {
        return await Context.BankAccounts
            .Include(ba => ba.UserBankAccounts)
            .Where(ba => ba.UserBankAccounts.Any(u => u.UserId == userId))
            .ToListAsync();
    }

    public async Task<BankAccount> GetBankAccountWithOwnerAsync(Guid userId, Guid bankId)
    {
        return await Context.BankAccounts
            .Include(ba => ba.UserBankAccounts)
            .FirstOrDefaultAsync(ba => ba.Id == bankId && ba.UserBankAccounts.Any(u => u.UserId == userId));
    }
}