using Microsoft.AspNetCore.Components;
using Organizer.Database.Storage.Providers;
using Organizer.Lib.Common.Responses;
using Organizer.Lib.Core.Interfaces;
using Organizer.WebApp.Shared.Modals;

namespace Organizer.WebApp.Pages.User;

public partial class Loan
{
    [Inject]
    private ILoanService LoanService { get; set; }

    [Inject]
    private IUserProvider UserProvider { get; set; }

    public List<LoanResponse> Loans { get; set; } = new();

    private AddApartmentModal AddModal { get; set; }
    private bool _showLoginModal;

    private BaseModal baseModal;
    private Guid _loanToRemoveId;

    private async Task ShowRemoveConfirmationModal(Guid id)
    {
        _loanToRemoveId = id;
        await baseModal.ShowModal("Czy na pewno chcesz usunąć ten kredyt?", DeleteLoan);
    }

    private void ShowAddModal()
    {
        AddModal.ShowModal();
    }

    protected override async Task OnInitializedAsync()
    {
        await GetAllApartments();
    }

    private async Task GetAllApartments()
    {
        Loans = await LoanService.GetAll();
    }

    private void ShowDetails(Guid apartmentId)
    {
    }

    private async Task DeleteLoan()
    {
        var result = await LoanService.Remove(_loanToRemoveId);

        if (result.IsSuccess)
        {
            await GetAllApartments();
            StateHasChanged();

        }
    }

    public async Task Create()
    {
        await GetAllApartments();
        StateHasChanged();
    }
}