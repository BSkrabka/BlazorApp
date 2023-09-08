using Microsoft.AspNetCore.Components;
using Organizer.Lib.Common.Requests;
using Organizer.Lib.Core.Interfaces;

namespace Organizer.WebApp.Shared.Modals;

public partial class LoginModal
{
    [Inject]
    public IAuthService AuthService { get; set; }
    [Inject]
    public NavigationManager NavigationManager { get; set; }
    public string ModalDisplay = "none;";
    public string ModalClass = string.Empty;
    public bool ShowBackdrop = false;
    public LoginRequest Request = new();
    public bool ShowAuthError { get; set; }
    public string Error { get; set; }

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
        Request = new();
        StateHasChanged();
    }

    public async Task Login()
    {
        var result = await AuthService.Login(Request);

        if (!result.IsAuthSuccessful)
        {
            Error = result.ErrorMessage;
            ShowAuthError = true;
        }
        else
        {
            NavigationManager.NavigateTo("/");
            Close();
        }
    }
}