using System.ComponentModel.DataAnnotations.Schema;
using Organizer.Database.Storage.Tables;

namespace Organizer.Database.Storage;

public abstract class BaseEntity
{
    public Guid Id { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? LastModifiedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    [ForeignKey(nameof(CreatedBy))]
    public Guid? CreatedById { get; set; }
    public virtual User? CreatedBy { get; set; }

    [ForeignKey(nameof(LastModifiedBy))]
    public Guid? LastModifiedById { get; set; }
    public virtual User? LastModifiedBy { get; set; }

    [ForeignKey(nameof(DeletedBy))]
    public Guid? DeletedById { get; set; }
    public virtual User? DeletedBy { get; set; }
}