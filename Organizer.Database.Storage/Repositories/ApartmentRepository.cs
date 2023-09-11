using Organizer.Database.Storage.Repositories.Interfaces;
using Organizer.Database.Storage.Tables;

namespace Organizer.Database.Storage.Repositories;

public class ApartmentRepository : BaseRepository<Apartment>, IApartmentRepository
{
    public ApartmentRepository(OrganizerDbContext context) : base(context)
    {
    }

}