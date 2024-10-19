using CloudHRMS.DAO;
using CloudHRMS.Repositories;
using CloudHRMS.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var config = builder.Configuration;//declare the configuration to read connection string of appSetting.json

//add the dbContext that we defined the ApplicationDbContext to get connection string name
builder.Services.AddDbContext<ApplicationDbContext>(option =>
                            option.UseSqlServer(config.GetConnectionString("CloudHRMSConnectingString")));
//Register for Identity UIs
builder.Services.AddRazorPages();
//Register for Identity dbContext for related Identity User and Roles.
builder.Services.AddIdentity<IdentityRole, IdentityUser>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();

//Register the Service Of Position Service
builder.Services.AddScoped<IPositionService, PositionService>();
//Register the Repository of Position Repository
builder.Services.AddScoped<IPositoryRepository, PositionRepository>();

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
//Enable Authentication & Authorization process
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
//Mapping the Razor Page Routes
app.MapRazorPages();
app.Run();
