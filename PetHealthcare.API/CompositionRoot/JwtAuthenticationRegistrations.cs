using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using PetHealthcare.API.Abstractions;
using PetHealthcare.API.Helpers;
using PetHealthcare.API.JwtAuth;
using PetHealthcare.API.ServiceDecorators;
using PetHealthcare.API.Services;
using PetHealthcare.Domain.Models;
using System.Security.Claims;
using System.Text;

namespace PetHealthcare.API.CompositionRoot
{
    public static class JwtAuthenticationRegistrations
    {
        public static IServiceCollection RegisterJwtAuthenticationServices(this IServiceCollection services, IConfiguration config)
        {
            var jwtOptions = new JwtOptions();
            config.GetSection("JwtSettings").Bind(jwtOptions);
            services.AddSingleton(jwtOptions);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = true;
                cfg.SaveToken = false;
                cfg.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = jwtOptions.JwtIssuer,
                    ValidAudience = jwtOptions.JwtIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.JwtSecret!)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(5)
                };
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("IsAdmin", builder => builder.RequireClaim(ClaimTypes.Role, "Admin"));
                options.AddPolicy("IsManager", builder => builder.RequireClaim(ClaimTypes.Role, "Manager"));
                options.AddPolicy("IsAdminOrManager", builder => builder.RequireClaim(ClaimTypes.Role, "Admin", "Manager"));
                options.AddPolicy("MarlowAndWendyOnly", builder => builder.RequireClaim(ClaimTypes.Name, "Marlow", "wendy"));
            });

            services.AddScoped<IPasswordHasher<ApplicationUser>, PasswordHasher<ApplicationUser>>();

            services.AddScoped<ITokenProvider, TokenProvider>();

            services.AddScoped<ICustomerNumberGenerator, CustomerNumberGenerator>();

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



            return services;
        }
    }
}
