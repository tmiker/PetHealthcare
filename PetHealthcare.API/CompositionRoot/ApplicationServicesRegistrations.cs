using Microsoft.AspNetCore.Identity;
using PetHealthcare.API.Abstractions;
using PetHealthcare.API.Helpers;
using PetHealthcare.API.ServiceDecorators;
using PetHealthcare.API.Services;
using PetHealthcare.DataAccess.SqlServerRepositories;
using PetHealthcare.Domain.Abstractions.ISqlServerRepositories;
using PetHealthcare.Domain.Models;

namespace PetHealthcare.API.CompositionRoot
{
    public static class ApplicationServicesRegistrations
    {
        public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAzureBlobStorageService, AzureBlobStorageService>();

            services.AddScoped<RegisterUserService>();
            services.AddScoped(sp =>
            {
                var baseService = sp.GetRequiredService<RegisterUserService>();
                var userManager = sp.GetRequiredService<UserManager<ApplicationUser>>();
                IRegisterUserService validatedService = new RegisterUserValidator(baseService, userManager);
                return validatedService;
            });

            services.AddScoped<LoginUserService>();
            services.AddScoped(sp =>
            {
                var baseService = sp.GetRequiredService<LoginUserService>();
                var userManager = sp.GetRequiredService<UserManager<ApplicationUser>>();
                var passwordHasher = sp.GetRequiredService<IPasswordHasher<ApplicationUser>>();
                ILoginUserService validatedService = new LoginUserValidator(baseService, userManager, passwordHasher);
                return validatedService;
            });

            services.AddScoped<UpdatePasswordService>();
            services.AddScoped(sp =>
            {
                var baseService = sp.GetRequiredService<UpdatePasswordService>();
                var userManager = sp.GetRequiredService<UserManager<ApplicationUser>>();
                var passwordHasher = sp.GetRequiredService<IPasswordHasher<ApplicationUser>>();
                IUpdatePasswordService decoratedService = new UpdatePasswordValidator(userManager, passwordHasher, baseService);
                return decoratedService;
            });

            services.AddScoped<ICustomerNumberGenerator, CustomerNumberGenerator>();

            services.AddScoped<IDeleteAccountService, DeleteAccountService>();

            services.AddScoped<IAdminUserService, AdminUserService>();

            services.AddScoped<ICarouselImageService, CarouselImageService>();
            services.AddScoped<IPetService, PetService>();
            services.AddScoped<IVetService, VetService>();
            services.AddScoped<IVisitService, VisitService>();

            return services;
        }
    }
}
