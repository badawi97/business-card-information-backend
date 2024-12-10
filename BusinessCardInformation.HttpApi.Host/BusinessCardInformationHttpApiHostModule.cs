using BusinessCardInformation.Application;
using BusinessCardInformation.Application.Contracts;
using BusinessCardInformation.Infrastructure;

namespace BusinessCardInformation.HttpApi.Host
{
    public class BusinessCardInformationHttpApiHostModule
    {
        public static void PreConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            BusinessCardInformationInfrastructureModule.PreConfigureServices(services, configuration);
            BusinessCardInformationApplicationContractsModule.PreConfigureServices(services, configuration);
            BusinessCardInformationApplicationModule.PreConfigureServices(services, configuration);


            services.AddControllers();

            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen();

        }

        public static void ConfigureServices(WebApplication app, IConfiguration configuration)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();
        }
    }
}
