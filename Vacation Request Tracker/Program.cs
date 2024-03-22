using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Vacation_Request_Tracker.Data;
using Vacation_Request_Tracker.Repositories.Vacation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<VacationDbContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("VacationRequestDbConnectionString")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<VacationDbContext>();


builder.Services.AddScoped<IVacationRepository, VacationRepository>();


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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
