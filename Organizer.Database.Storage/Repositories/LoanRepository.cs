using Organizer.Database.Storage.Repositories.Interfaces;
using Organizer.Database.Storage.Tables;

namespace Organizer.Database.Storage.Repositories;

public class LoanRepository : BaseRepository<Loan>, ILoanRepository
{
    public LoanRepository(OrganizerDbContext context) : base(context)
    {
    }
}