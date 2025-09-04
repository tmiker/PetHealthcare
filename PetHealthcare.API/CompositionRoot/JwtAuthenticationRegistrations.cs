using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using PetHealthcare.API.Abstractions;
using PetHealthcare.API.Helpers;
using PetHealthcare.API.JwtAuth;
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

            

            return services;
        }
    }
}
