using Organizer.Lib.Helper.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Organizer.Database.Storage.Tables;

public class UserApartment
{
    [Key]
    public Guid Id { get; set; }

    public required EntityPermissionEnum ApartmentPermission { get; set; }

    [ForeignKey(nameof(User))]
    public Guid UserId { get; set; }
    public virtual required User User { get; set; }

    [ForeignKey(nameof(Apartment))]
    public Guid ApartmentId { get; set; }
    public virtual required Apartment Apartment { get; set; }
}