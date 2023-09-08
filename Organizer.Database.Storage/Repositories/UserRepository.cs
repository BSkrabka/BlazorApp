using Organizer.Database.Storage.Repositories.Interfaces;
using Organizer.Database.Storage.Tables;

namespace Organizer.Database.Storage.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(OrganizerDbContext context) : base(context)
    {
    }
}