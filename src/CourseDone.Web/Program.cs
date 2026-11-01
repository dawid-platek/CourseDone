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

app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();
// Manifest dla przyszłego launchera ekosystemu.
app.MapGet("/.well-known/eco-app", () => AppInfo.Manifest);

app.Run();

public partial class Program;
