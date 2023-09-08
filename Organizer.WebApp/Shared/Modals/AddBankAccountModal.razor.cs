using Azure.Core;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Organizer.Lib.Common.Requests;
using Organizer.Lib.Core.Interfaces;
using System.Security.Claims;

namespace Organizer.WebApp.Shared.Modals;

public partial class AddBankAccountModal
{
    [Inject]
    public IBankAccountService BankAccountService { get; set; }
    [Inject]
    private AuthenticationStateProvider AuthenticationStateProvider { get; set; }
    [Parameter]
    public Func<Task> Create { get; set; }

    public string ModalDisplay = "none;";
    public string ModalClass = string.Empty;
    public bool ShowBackdrop = false;
    public bool ShowAddError { get; set; }
    public string Error { get; set; }
    public BankAccountCreateRequest Request = new();

    public void Open()
    {
        ModalDisplay = "block;";
        ModalClass = "Show";
        ShowBackdrop = true;
        StateHasChanged();
    }

    public void Close()
    {
        ModalDisplay = "none";
        ModalClass = "";
        ShowBackdrop = false;
        StateHasChanged();
    }

    public async Task CreateBank()
    {
        var authenticationState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        var result = Guid.TryParse(authenticationState.User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId);
        if (!result) { return; }
        await BankAccountService.CreateBankAccount(Request, userId);
        await Create();
        Request = new();
        Close();
    }
}