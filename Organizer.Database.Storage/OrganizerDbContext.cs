using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using Organizer.Database.Storage.Tables;

namespace Organizer.Database.Storage;

public class OrganizerDbContext : DbContext
{

    public OrganizerDbContext(DbContextOptions<OrganizerDbContext> options) : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Apartment> Apartments { get; set; }
    public virtual DbSet<BankAccount> BankAccounts { get; set; }
    public virtual DbSet<Expense> Expenses { get; set; }
    public virtual DbSet<Loan> Loans { get; set; }
    public virtual DbSet<UserApartment> UserApartments { get; set; }
    public virtual DbSet<UserBankAccount> UserBankAccounts { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        ApplyQueryFilters(builder);
    }

    protected static void ApplyQueryFilters(ModelBuilder modelBuilder)
    {
        Expression<Func<BaseEntity, bool>> softDeleteFilterExpr = entity => !entity.DeletedAt.HasValue;

        foreach (var mutableType in modelBuilder.Model.GetEntityTypes())
        {
            if (mutableType.ClrType.IsAssignableTo(typeof(BaseEntity)))
            {
                var parameter = Expression.Parameter(mutableType.ClrType);
                var body = ReplacingExpressionVisitor.Replace(softDeleteFilterExpr.Parameters.First(), parameter,
                    softDeleteFilterExpr.Body);
                var lambda = Expression.Lambda(body, parameter);

                mutableType.SetQueryFilter(lambda);
            }
        }
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateSoftDeleteStatus();
        return base.SaveChangesAsync(cancellationToken);
    }

    public override int SaveChanges()
    {
        UpdateSoftDeleteStatus();
        return base.SaveChanges();
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        UpdateSoftDeleteStatus();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = default)
    {
        UpdateSoftDeleteStatus();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void UpdateSoftDeleteStatus()
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.Entity is BaseEntity baseEntity)
            {
                if (entry.State == EntityState.Added)
                {
                    baseEntity.CreatedAt = DateTime.Now;
                }
                else if (entry.State == EntityState.Deleted)
                {
                    entry.State = EntityState.Modified;
                    baseEntity.DeletedAt = DateTime.Now;
                }
            }
        }
    }
}