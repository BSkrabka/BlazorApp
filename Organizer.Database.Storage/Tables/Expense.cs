using Organizer.Lib.Helper.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Organizer.Database.Storage.Tables;

public class Expense : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public required double Value { get; set; }
    public ExpenseEnum ExpenseType { get; set; }

    [ForeignKey(nameof(Owner))]
    public Guid? OwnerId { get; set; }
    public virtual User? Owner { get; set; }
}