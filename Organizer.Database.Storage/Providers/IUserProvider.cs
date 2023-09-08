namespace Organizer.Database.Storage.Providers;

public interface IUserProvider
{
    Task<Guid?> GetUserId();
}