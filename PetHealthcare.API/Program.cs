using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PetHealthcare.API.CompositionRoot;
using PetHealthcare.DataAccess.Data;
using PetHealthcare.Domain.Models;
using Scalar.AspNetCore;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("PetHealthcareApiSqlServerConnection")));
        builder.Services.AddIdentityCore<ApplicationUser>().AddRoles<IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAnyPolicy", cfg =>
            {
                cfg.AllowAnyOrigin();
                cfg.AllowAnyMethod();
                cfg.AllowAnyHeader();
            });
        });

        // CONFIGURATION EXTENSION REGISTRATIONS
        builder.Services.RegisterAutomapperServices();
        builder.Services.RegisterJwtAuthenticationServices(builder.Configuration);




        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference();
        }

        app.UseHttpsRedirection();

        app.UseCors("AllowAnyPolicy");
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}