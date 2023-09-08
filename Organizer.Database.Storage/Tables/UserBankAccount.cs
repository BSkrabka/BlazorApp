using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Organizer.Lib.Helper.Enums;

namespace Organizer.Database.Storage.Tables;

public class UserBankAccount
{
    [Key]
    public Guid Id { get; set; }

    public required EntityPermissionEnum BankAccountPermission { get; set; }

    [ForeignKey(nameof(User))]
    public Guid UserId { get; set; }
    public virtual User User { get; set; }

    [ForeignKey(nameof(BankAccount))]
    public Guid BankAccountId { get; set; }
    public virtual BankAccount BankAccount { get; set; }
}