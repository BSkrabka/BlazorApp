using Microsoft.AspNetCore.Components;
using Organizer.Lib.Common.Requests;
using Organizer.Lib.Core.Interfaces;

namespace Organizer.WebApp.Shared.Modals;

public partial class AddApartmentModal
{
    [Inject]
    public IApartmentService ApartmentService { get; set; }

    [Parameter]
    public Func<Task> Create { get; set; }

    public bool ShowBackdrop = false;
    public bool ShowAddError { get; set; }
    public string Error { get; set; }
    public ApartmentCreateRequest Request = new();

    private bool showModal;

    public void ShowModal()
    {
        showModal = true;
        ShowBackdrop = true;
        StateHasChanged();
    }

    private void CloseModal()
    {
        showModal = false;
        ShowBackdrop = false;
    }

    private async Task CreateApartment()
    {
        await ApartmentService.Create(Request);
        await Create();
        Request = new();
        CloseModal();
    }
}