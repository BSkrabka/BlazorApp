using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Organizer.Lib.Common.Responses;
using Organizer.Lib.Core.Interfaces;
using Organizer.WebApp.Shared.Modals;
using System.Security.Claims;

namespace Organizer.WebApp.Pages.User;

public partial class BankAccount
{
    [Inject]
    private IBankAccountService BankAccountService { get; set; }

    [Inject]
    private AuthenticationStateProvider AuthenticationStateProvider { get; set; }

    public List<BankAccountResponse> BankAccounts { get; set; } = new();

    private AddBankAccountModal Modal { get; set; }
    private bool showLoginModal;

    protected override async Task OnInitializedAsync()
    {
        await GetBankAccounts();
    }

    private async Task<Guid?> GetUserId()
    {
        var authenticationState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        var result = Guid.TryParse(authenticationState.User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId);
        if (!result) { return null; }

        return userId;
    }

    public async Task GetBankAccounts()
    {
        var userId = await GetUserId();
        if (userId == null) { return; }

        BankAccounts = await BankAccountService.GetBankAccountsAsync(userId.Value);
    }

    public async Task Create()
    {
        await GetBankAccounts();
        StateHasChanged();
    }

    private async Task Remove(Guid bankId)
    {
        var userId = await GetUserId();
        if (userId == null) { return; }

        await BankAccountService.RemoveBankAccount(bankId, userId.Value);

        await GetBankAccounts();
    }
}