using Microsoft.EntityFrameworkCore;
using ProductStorageMvc.Data;
using ProductStorageMvc.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

string port = Environment.GetEnvironmentVariable("PORT") ?? "10000";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException("The database connection string was not found.");

    if (string.IsNullOrWhiteSpace(connectionString))
    {
        throw new InvalidOperationException("The database connection string is empty. Configure ConnectionStrings__DefaultConnection in Render or appsettings.json locally.");
    }

    options.UseNpgsql(connectionString);
});

builder.Services.AddScoped<ProductService>();

WebApplication app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Render terminates HTTPS at its load balancer and forwards traffic to the container over HTTP.
// For this reason, this project does not force HTTPS redirection inside the container.
// app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapGet("/health", () => Results.Ok("OK"));

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Products}/{action=Create}/{id?}");

app.Run();
