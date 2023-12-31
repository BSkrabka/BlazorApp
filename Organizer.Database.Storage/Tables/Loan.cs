﻿namespace Organizer.Database.Storage.Tables;

public class Loan : BaseEntity
{
    public string Name { get; set; }
    public string AccountNumber { get; set; }

    public double BaseValue { get; set; }
    public double CurrentValue { get; set; }
    public DateTime FinalRepaymentAt { get; set; }
    public DateTime NextRepayment { get; set; }
}