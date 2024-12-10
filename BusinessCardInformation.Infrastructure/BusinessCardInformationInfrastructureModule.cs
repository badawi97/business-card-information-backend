using BusinessCardInformation.Domain.Cards.Interfaces;
using BusinessCardInformation.Infrastructure.Repositories.Cards;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessCardInformation.Infrastructure
{
    public class BusinessCardInformationInfrastructureModule
    {
        public static void PreConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            RegisterDbContext(services, configuration);
            services.AddTransient<ICardRepository, CardRepository>();

        }

        private static void RegisterDbContext(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BusinessCardInformationDbContext>(options =>
                options.UseNpgsql(GetConnectionString(configuration)));
        }

        private static string GetConnectionString(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException("Connection string is missing");
            }

            return connectionString;
        }
    }
}
