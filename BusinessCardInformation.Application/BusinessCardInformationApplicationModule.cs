using BusinessCardInformation.Application.Cards;
using BusinessCardInformation.Application.Contracts.Cards.Interfaces;
using BusinessCardInformation.Application.Contracts.DI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessCardInformation.Application
{
    public class BusinessCardInformationApplicationModule
    {
        public static void PreConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            RegisterTransientServices(services);
            RegisterSingletonServices(services);
            RegisterScopedServices(services);
            services.AddTransient<ICardApplicationService, CardApplicationService>();
            services.AddAutoMapper(typeof(BusinessCardInformationApplicationAutoMapperProfile).Assembly);
        }


        private static void RegisterTransientServices(IServiceCollection services)
        {
            services.Scan(scan => scan
                .FromAssemblyOf<ITransient>()
                .AddClasses(classes => classes.AssignableTo<ITransient>())
                .AsImplementedInterfaces()
                .WithTransientLifetime());
        }

        private static void RegisterSingletonServices(IServiceCollection services)
        {
            services.Scan(scan => scan
                .FromAssemblyOf<ISingleton>()
                .AddClasses(classes => classes.AssignableTo<ISingleton>())
                .AsImplementedInterfaces()
                .WithSingletonLifetime());
        }

        private static void RegisterScopedServices(IServiceCollection services)
        {
            services.Scan(scan => scan
                .FromAssemblyOf<IScoped>()
                .AddClasses(classes => classes.AssignableTo<IScoped>())
                .AsImplementedInterfaces()
                .WithScopedLifetime());
        }
    }
}
