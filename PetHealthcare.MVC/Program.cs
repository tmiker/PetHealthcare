using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using PetHealthcare.MVC.Abstractions;
using PetHealthcare.MVC.HttpProviders;
using PetHealthcare.MVC.Services;
using PetHealthcare.MVC.Utility;
using System.Net.Http.Headers;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(90);
    options.SlidingExpiration = true;
    options.LoginPath = "/PetHealth/Auth/Login";
    options.AccessDeniedPath = "/PetHealth/Auth/AccessDenied";
});

builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("IsAdmin", builder => builder.RequireClaim(ClaimTypes.Role, "Admin"));
    options.AddPolicy("IsAdminOrManager", builder => builder.RequireClaim(ClaimTypes.Role, "Admin", "Manager"));
    options.AddPolicy("MarlowAndWendyOnly", builder => builder.RequireClaim(ClaimTypes.Name, "marlow", "wendy"));
});

builder.Services.AddHttpClient(name: StaticDetails.PetHealthcareApi_ClientName, config =>
{
    config.BaseAddress = new Uri(StaticDetails.PetHealthcareApi_BaseUrl);
    config.DefaultRequestHeaders.Clear();
    config.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json", 1.0));
});
builder.Services.AddSingleton<IAuthenticationHttpProvider, AuthenticationHttpProvider>();
builder.Services.AddSingleton<IAdminUsersHttpProvider, AdminUsersHttpProvider>();
builder.Services.AddSingleton<ICarouselImagesHttpProvider, CarouselImagesHttpProvider>();

builder.Services.AddScoped<IClaimsDecoder, ClaimsDecoder>();
builder.Services.AddScoped<ITokenStatusDecoder, TokenStatusDecoder>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Landing}/{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
