using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Organizer.Lib.Core.Interfaces;
using Organizer.Lib.Core.Providers;
using Organizer.Lib.Core.Services;

namespace Organizer.Lib.Core;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddCoreLibraryServices(this IServiceCollection services)
    {
        services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IBankAccountService, BankAccountService>();

        return services;
    }
}