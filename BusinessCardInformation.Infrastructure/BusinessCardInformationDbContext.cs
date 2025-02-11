using BusinessCardInformation.Domain.Cards.Entities;
using Microsoft.EntityFrameworkCore;
using BusinessCardInformation.Domain.Shared.Entities;
using BusinessCardInformation.Domain.Shared.Interfaces;

namespace BusinessCardInformation.Infrastructure
{
    public class BusinessCardInformationDbContext : DbContext
    {
        private readonly IUserContextService _userContextService;

        public BusinessCardInformationDbContext(DbContextOptions<BusinessCardInformationDbContext> options, IUserContextService userContextService)
            : base(options)
        {
            _userContextService = userContextService;
        }

        // Define DbSet properties for entities

        public DbSet<Card> Cards { get; set; }


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var currentUserId = _userContextService.GetCurrentUserId();

            // Handle auditing for entities that implement AuditableEntity
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.SetAuditOnCreate(currentUserId);
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.SetAuditOnUpdate(currentUserId);
                }
                else if (entry.State == EntityState.Deleted)
                {
                    // Soft delete: mark as deleted and avoid actual deletion from DB
                    entry.State = EntityState.Modified;
                    entry.Entity.SoftDelete(currentUserId);

                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Apply global filters and configurations for soft delete and other entities
            modelBuilder.Entity<Card>()
                .HasQueryFilter(e => !e.IsDeleted)
                .ToTable("Cards");

            // Other entities can be configured here as needed
            // Example: modelBuilder.Entity<OtherEntity>().HasQueryFilter(e => !e.IsDeleted);

            base.OnModelCreating(modelBuilder);
        }
    }
}
