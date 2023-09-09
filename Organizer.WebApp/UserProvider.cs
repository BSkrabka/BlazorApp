using System.Security.Claims;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Organizer.Database.Storage.Providers;

namespace Organizer.WebApp;

public class UserProvider : IUserProvider
{
    private readonly AuthenticationStateProvider _authenticationStateProvider;

    public UserProvider(AuthenticationStateProvider authenticationStateProvider)
    {
        _authenticationStateProvider = authenticationStateProvider;
    }


    public async Task<Guid?> GetUserId()
    {
        var authenticationState = await _authenticationStateProvider.GetAuthenticationStateAsync();
        var result = Guid.TryParse(authenticationState.User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId);
        if (!result) { return null; }

        return userId;
    }
}