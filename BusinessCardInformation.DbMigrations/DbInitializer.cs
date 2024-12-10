using BusinessCardInformation.Domain.Cards.Entities;
using BusinessCardInformation.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BusinessCardInformation.DbMigrations
{
    public class DbInitializer
    {
        private readonly BusinessCardInformationDbContext _dbContext;

        public DbInitializer(BusinessCardInformationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {
            try
            {
                if (!_dbContext.Cards.Any())
                {
                    _dbContext.Cards.Add(new Card
                    {
                        Id = Guid.NewGuid(),
                        Name = "Khalid",
                        Gender = 1,
                        DateOfBirth = DateTime.SpecifyKind(new DateTime(1997, 3, 10), DateTimeKind.Utc)
                    });

                    _dbContext.SaveChanges();
                }
            }
            catch (DbUpdateException dbEx)
            {
                Console.WriteLine(dbEx.InnerException?.Message);
                throw;
            }
        }

    }
}
