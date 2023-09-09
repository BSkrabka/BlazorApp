using Blazorise.Charts;
using Microsoft.AspNetCore.Components;
using Organizer.Database.Storage.Providers;
using Organizer.Lib.Common.Responses;
using Organizer.Lib.Core.Interfaces;
using Organizer.Lib.Helper.Enums;
using Radzen;
using System;

namespace Organizer.WebApp.Pages.User;

public partial class BankAccountDetails
{
    [Inject] private IBankAccountService BankAccountService { get; set; }
    [Inject] private IUserProvider UserProvider { get; set; }

    bool showDataLabels = false;

    void OnSeriesClick(SeriesClickEventArgs args)
    {
    }

    [Parameter] public Guid Id { get; set; }

    private BankAccountResponse bankAccount;

    protected override async Task OnInitializedAsync()
    {
        var userId = await UserProvider.GetUserId();
        if (userId == null) return;

        bankAccount = await BankAccountService.GetBankAccountAsync(userId.Value, Id);
    }

    private DataItem[] revenue = new DataItem[]
    {
        new DataItem
        {
            Quarter = "Q1",
            Revenue = 30000
        },
        new DataItem
        {
            Quarter = "Q2",
            Revenue = 40000
        },
        new DataItem
        {
            Quarter = "Q3",
            Revenue = 50000
        },
        new DataItem
        {
            Quarter = "Q4",
            Revenue = 80000
        },
    };
}

class DataItem
    {
        public string Quarter { get; set; }
        public double Revenue { get; set; }
    }