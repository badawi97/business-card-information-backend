using BusinessCardInformation.Domain.Cards.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessCardInformation.Infrastructure
{
    public class BusinessCardInformationDbContext : DbContext
    {
        public BusinessCardInformationDbContext(DbContextOptions<BusinessCardInformationDbContext> options) : base(options)
        {
        }
        public DbSet<Card> Cards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Card>().ToTable("Cards");
            base.OnModelCreating(modelBuilder);
            // Fluent API configuration goes here if necessary
        }
    }
}
