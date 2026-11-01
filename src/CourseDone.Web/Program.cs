using CourseDone.Web;
using CourseDone.Web.Data;
using Eco.Shared;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddDbContext<CourseDoneDb>(o =>
    o.UseSqlite(builder.Configuration.GetConnectionString("Default") ?? "Data Source=coursedone.db"));
builder.Services.AddSingleton(AppInfo.Manifest);
builder.Services.AddScoped<ICurrentUser, SingleUser>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
    scope.ServiceProvider.GetRequiredService<CourseDoneDb>().Database.EnsureCreated();

if (!app.Environment.IsDevelopment())
    app.UseExceptionHandler("/Error");

var types = new Microsoft.AspNetCore.StaticFiles.FileExtensionContentTypeProvider();
types.Mappings[".webmanifest"] = "application/manifest+json";
app.UseStaticFiles(new StaticFileOptions
{
    ContentTypeProvider = types,
    // SW musi być zawsze świeży, inaczej aktualizacje PWA utkną w cache przeglądarki.
    OnPrepareResponse = ctx =>
    {
        if (ctx.File.Name == "sw.js") ctx.Context.Response.Headers.CacheControl = "no-cache";
    }
});
app.UseRouting();
app.MapRazorPages();
// Manifest dla przyszłego launchera ekosystemu.
app.MapGet("/.well-known/eco-app", () => AppInfo.Manifest);

app.Run();

public partial class Program;
