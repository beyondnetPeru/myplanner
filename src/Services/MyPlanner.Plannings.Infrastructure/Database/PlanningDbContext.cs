using MyPlanner.Plannings.Infrastructure.Database.Configurations;
using MyPlanner.Plannings.Infrastructure.Database.Tables;
using MyPlanner.Products.Infrastructure.Database.Configurations;
using MyPlanner.Shared.Extensions;
using MyPlanner.IntegrationEventLogEF;

namespace MyPlanner.Plannings.Infrastructure.Database
{
    public class PlanningDbContext : DbContext, IUnitOfWork
    {
        private readonly IMediator _mediator;
        private IDbContextTransaction currentTransaction;

        public DbSet<ErrorTable> Errors { get; set; }
        public DbSet<PlanTable> Plans { get; set; }
        public DbSet<PlanCategoryTable> PlanCategories { get; set; }
        public DbSet<PlanItemTable> PlanItems { get; set; }
        public DbSet<SizeModelTypeTable> SizeModelTypes { get; set; }
        public DbSet<SizeModelTypeItemTable> SizeModelTypeItems { get; set; }
        public DbSet<SizeModelTable> SizeModels { get; set; }
        public DbSet<SizeModelItemTable> SizeModelItems { get; set; }
        public bool HasActiveTransaction => currentTransaction != null;

        public PlanningDbContext(DbContextOptions<PlanningDbContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            
            System.Diagnostics.Debug.WriteLine("OrderingContext::ctor ->" + this.GetHashCode());
        }

        public IDbContextTransaction GetCurrentTransaction() => currentTransaction;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("myplanner-plannings");

            modelBuilder.ApplyConfiguration(new ErrorEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ClientRequestEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SizeModelTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SizeModelTypeItemsConfiguration());
            modelBuilder.ApplyConfiguration(new SizeModelConfiguration());
            modelBuilder.ApplyConfiguration(new SizeModelItemConfiguration());
            modelBuilder.ApplyConfiguration(new SizeModelTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PlanConfiguration());
            modelBuilder.ApplyConfiguration(new PlanItemConfiguration());
            modelBuilder.ApplyConfiguration(new PlanCategoryConfiguration());

            modelBuilder.UseIntegrationEventLogs();
        }

        public async Task<bool> SaveEntitiesAsync(object entity, CancellationToken cancellationToken = default)
        {
            await _mediator.DispatchDomainEventsAsync(entity);

            _ = await base.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (currentTransaction != null)
            {
                return null;
            }

            currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            return currentTransaction;
        }

        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException(nameof(transaction));
            }

            if (transaction != currentTransaction)
            {
                throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");
            }

            try
            {
                await SaveChangesAsync();
                transaction.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (currentTransaction != null)
                {
                    currentTransaction.Dispose();
                    currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                currentTransaction?.Rollback();
            }
            finally
            {
                if (currentTransaction != null)
                {
                    currentTransaction.Dispose();
                    currentTransaction = null;
                }
            }
        }


    }
}
