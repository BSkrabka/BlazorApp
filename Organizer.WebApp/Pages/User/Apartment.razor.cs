using Microsoft.AspNetCore.Components;
using Organizer.Database.Storage.Providers;
using Organizer.Lib.Common.Responses;
using Organizer.Lib.Core.Interfaces;
using Organizer.WebApp.Shared.Modals;

namespace Organizer.WebApp.Pages.User;

public partial class Apartment
{
    [Inject]
    private IApartmentService ApartmentService { get; set; }

    [Inject]
    private IUserProvider UserProvider { get; set; }

    public List<ApartmentResponse> Apartments { get; set; } = new();

    private AddApartmentModal AddModal { get; set; }
    private bool showLoginModal;

    private BaseModal baseModal;
    private Guid apartmentToRemoveId;

    private async Task ShowRemoveConfirmationModal(Guid id)
    {
        apartmentToRemoveId = id;
        await baseModal.ShowModal("Czy na pewno chcesz usunąć to konto?", DeleteApartment);
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
        Apartments = await ApartmentService.GetAll();
    }

    private void ShowDetails(Guid apartmentId)
    {
    }

    private async Task DeleteApartment()
    {
        var result = await ApartmentService.Remove(apartmentToRemoveId);

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