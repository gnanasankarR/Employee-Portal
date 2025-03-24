using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using MyWebApp.Data;

var builder = WebApplication.CreateBuilder(args);

// ✅ Add Razor Pages
builder.Services.AddRazorPages();

// ✅ Register Entity Framework Core with SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
           .EnableSensitiveDataLogging()  // Logs detailed SQL queries
           .LogTo(Console.WriteLine, LogLevel.Information)); // Outputs to console


// ✅ Add Authentication & Authorization services
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();

builder.Services.AddAuthorization();

var app = builder.Build();

// ✅ Ensure correct middleware order
app.UseRouting();

app.UseAuthentication(); // Must be before UseAuthorization
app.UseAuthorization();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
app.UseStaticFiles(); // ✅ Enables serving static files

// Disable HTTPS redirection for local testing
app.UseHttpsRedirection();

app.MapRazorPages(); // ✅ Ensure Razor Pages routing is enabled

app.Run();
