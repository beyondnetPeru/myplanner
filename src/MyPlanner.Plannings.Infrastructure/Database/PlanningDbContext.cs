using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using BeyondNet.Ddd.Interfaces;
using MyPlanner.Plannings.Infrastructure.Database.Configurations;
using MyPlanner.Plannings.Infrastructure.Database.Tables;
using MyPlanner.Products.Infrastructure.Database.Configurations;
using MyPlanner.Plannings.Shared.Extensions;
using MyPlanner.Plannings.IntegrationEventLogEF;

namespace MyPlanner.Plannings.Infrastructure.Database
{
    public class PlanningDbContext : DbContext, IUnitOfWork
    {

        public DbSet<ErrorTable> Errors { get; set; }
        public DbSet<PlanTable> Plans { get; set; }
        public DbSet<PlanItemTable> PlanItems { get; set; }
        public DbSet<SizeModelTypeTable> SizeModelTypes { get; set; }
        public DbSet<SizeModelTypeItemTable> SizeModelTypeItems { get; set; }
        public DbSet<SizeModelTable> SizeModels { get; set; }
        public DbSet<SizeModelItemTable> SizeModelItems { get; set; }

        private readonly IMediator mediator;

        private IDbContextTransaction? currentTransaction;
        public IDbContextTransaction GetCurrentTransaction() => currentTransaction!;
        public bool HasActiveTransaction => currentTransaction != null;

        public PlanningDbContext(DbContextOptions<PlanningDbContext> options, IMediator mediator) : base(options)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

            System.Diagnostics.Debug.WriteLine("PlanningDbContext::ctor ->" + this.GetHashCode());
        }

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

            modelBuilder.UseIntegrationEventLogs();
        }

        public async Task<bool> SaveEntitiesAsync(object entity, CancellationToken cancellationToken = default)
        {
            await mediator.DispatchDomainEventsAsync(entity);

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
