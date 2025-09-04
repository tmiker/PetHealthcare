using PetHealthcare.API.Abstractions;
using PetHealthcare.API.Services;

namespace PetHealthcare.API.CompositionRoot
{
    public static class ApplicationServicesRegistrations
    {
        public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IAzureBlobStorageService, AzureBlobStorageService>();


            return services;
        }
    }
}
