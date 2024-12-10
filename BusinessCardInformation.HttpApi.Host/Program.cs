
namespace BusinessCardInformation.HttpApi.Host
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;

            BusinessCardInformationHttpApiHostModule.PreConfigureServices(builder.Services, configuration);

            var app = builder.Build();

            BusinessCardInformationHttpApiHostModule.ConfigureServices(app, configuration);

            await app.RunAsync();
        }
    }
}
