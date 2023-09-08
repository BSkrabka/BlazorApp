using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Components;
using Organizer.Database.Storage.Tables;
using Organizer.Lib.Common.Responses;
using Organizer.Lib.Core.Interfaces;

namespace Organizer.WebApp.Pages.Admin;

public partial class UserManagement
{
    public List<UserResponse> Users { get; set; } = new();
    [Inject]
    private IUserService _userService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await GetUsers();
    }

    protected async Task GetUsers()
    {
        Users = await _userService.GetUsersAsync();
    }

    private async Task DeleteUser(Guid userId)
    {
        var result = await _userService.RemoveUserAsync(userId);

        if (result.IsSuccess)
        {
            await GetUsers();
        }
    }

    private void ShowDetails(Guid userId)
    {
        // Implementacja wyświetlania szczegółów użytkownika
    }
}