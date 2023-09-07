using Microsoft.Extensions.DependencyInjection;

namespace Organizer.Database.Storage;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddStorageLibraryServices(this IServiceCollection services)
    {
        services.AddDbContext<OrganizerDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")));

        return services;
    }
}