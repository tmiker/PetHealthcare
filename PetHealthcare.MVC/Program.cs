using PetHealthcare.MVC.Abstractions;
using PetHealthcare.MVC.HttpProviders;
using PetHealthcare.MVC.Utility;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddHttpClient(name: StaticDetails.PetHealthcareApi_ClientName, config =>
{
    config.BaseAddress = new Uri(StaticDetails.PetHealthcareApi_BaseUrl);
    config.DefaultRequestHeaders.Clear();
    config.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json", 1.0));
});
builder.Services.AddSingleton<IAuthenticationHttpProvider, AuthenticationHttpProvider>();
builder.Services.AddSingleton<IAdminUsersHttpProvider, AdminUsersHttpProvider>();
builder.Services.AddSingleton<ICarouselImagesHttpProvider, CarouselImagesHttpProvider>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
