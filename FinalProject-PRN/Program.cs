using CloudinaryDotNet;
using FinalProject_PRN.Models;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.FileProviders.Physical;

var builder = WebApplication.CreateBuilder(args);

//var configuration = new ConfigurationBuilder()
//    .SetBasePath(builder.Environment.ContentRootPath)
//   .AddJsonFile("appsettings.json")
//.Build();

//builder.Services.Configure<CloudinarySettings>(configuration.GetSection("CloudinarySettings"));

builder.Services.AddControllersWithViews();
builder.Services.AddSession();
var app = builder.Build();

app.UseHttpsRedirection(); // Add this line to enforce HTTPS redirection

app.UseSession();
app.UseRouting();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "Styles")),
    RequestPath = "/Styles"
});

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{searchString?}/{genreString?}/{aid?}/{number?}"
    );
});

app.Run();
