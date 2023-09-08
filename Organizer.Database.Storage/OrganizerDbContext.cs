using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using Organizer.Database.Storage.Tables;
using Organizer.Database.Storage.Providers;

namespace Organizer.Database.Storage;

public class OrganizerDbContext : DbContext
{
    private readonly IUserProvider _userProvider;

    public OrganizerDbContext(DbContextOptions<OrganizerDbContext> options, IUserProvider userProvider) : base(options)
    {
        _userProvider = userProvider;
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

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await UpdateSoftDeleteStatus();
        return await base.SaveChangesAsync(cancellationToken);
    }

    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = default)
    {
        await UpdateSoftDeleteStatus();
        return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private async Task UpdateSoftDeleteStatus()
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.Entity is BaseEntity baseEntity)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        baseEntity.CreatedAt = DateTime.Now;
                        baseEntity.CreatedById = await _userProvider.GetUserId();
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        baseEntity.DeletedById = await _userProvider.GetUserId();
                        baseEntity.DeletedAt = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        baseEntity.LastModifiedById = await _userProvider.GetUserId();
                        baseEntity.LastModifiedAt = DateTime.Now;
                        break;
                }
            }
        }
    }
}