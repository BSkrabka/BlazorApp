using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components;
using Organizer.Lib.Core.Interfaces;

namespace Organizer.WebApp.Shared;

public partial class Logout
{
    [Inject]
    public IAuthService AuthService { get; set; }
    [Inject]
    public NavigationManager NavigationManager { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await AuthService.Logout();
        NavigationManager.NavigateTo("/");
    }
}