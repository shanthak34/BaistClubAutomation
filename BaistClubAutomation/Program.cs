using BaistClubAutomation.Pages.BLL;
using BaistClubAutomation.Pages.Data;
using BaistClubAutomation.Pages.Manager;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<MembershipManager>();
builder.Services.AddScoped<MembershipService>();
builder.Services.AddScoped<TeeTimeManager>();
builder.Services.AddScoped<TeeTimeService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
