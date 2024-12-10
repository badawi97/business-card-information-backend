using BusinessCardInformation.Application.Contracts.Cards.Validators;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessCardInformation.Application.Contracts
{
    public class BusinessCardInformationApplicationContractsModule
    {
        public static void PreConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers().AddFluentValidation(fv =>
            {
                fv.RegisterValidatorsFromAssemblyContaining<CreateCardDtoValidator>();
            });
            services.AddControllers().AddFluentValidation(fv =>
            {
                fv.RegisterValidatorsFromAssemblyContaining<UpdateCardDtoValidator>();
            });
        }


    }
}
