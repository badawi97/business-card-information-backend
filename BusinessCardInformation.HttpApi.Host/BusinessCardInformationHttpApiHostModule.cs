using BusinessCardInformation.Application;
using BusinessCardInformation.Application.Contracts;
using BusinessCardInformation.Infrastructure;
using Microsoft.OpenApi.Models;

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

            ConfigureSwagger(services);
        }

        public static void ConfigureServices(WebApplication app, IConfiguration configuration)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
        }

        private static void ConfigureSwagger(IServiceCollection services)
        {
            // Configure Swagger with JWT Authentication
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' followed by space and your token. Example: \"Bearer your_token_here\""
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                        },
                        Array.Empty<string>()
                    }
                });
            });
        }
    }
}
