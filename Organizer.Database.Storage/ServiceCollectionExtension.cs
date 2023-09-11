using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Organizer.Database.Storage.Repositories;
using Organizer.Database.Storage.Repositories.Interfaces;

namespace Organizer.Database.Storage;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddStorageLibraryServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<OrganizerDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("OrganizerDb")));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IBankAccountRepository, BankAccountRepository>();
        services.AddScoped<ILoanRepository, LoanRepository>();
        services.AddScoped<IApartmentRepository, ApartmentRepository>();

        return services;
    }
}