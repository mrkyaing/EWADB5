using CloudHRMS.DAO;
using CloudHRMS.Repositories;
using CloudHRMS.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();
var config = builder.Configuration;//declare the configuration to read connection string of appSetting.json
var connectionString = config.GetConnectionString("CloudHRMSConnectingStringMySQL");
//add the dbContext that we defined the ApplicationDbContext to get connection string name
//Database Connection with MS SQL
builder.Services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(config.GetConnectionString("CloudHRMSConnectingString")));
//Database Connection with MySQL
/*
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString,
    new MySqlServerVersion(new Version(8, 0, 35)),
    mySqlOptions => mySqlOptions.EnableRetryOnFailure()));
*/
//Register for Identity UIs
builder.Services.AddRazorPages();
//Register for Identity dbContext for related Identity User and Roles.
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
//Register the Service Of Position Service
builder.Services.AddScoped<IPositionService, PositionService>();
//Register the Repository of Position Repository
builder.Services.AddScoped<IPositoryRepository, PositionRepository>();
//Register the user service to be use.
builder.Services.AddScoped<IUserService, UserService>();
//Register the Reporting service to be use.
builder.Services.AddScoped<IReportingService, ReportingService>();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
//Enable Authentication & Authorization process
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
//Mapping the Razor Page Routes
app.MapRazorPages();
app.Run();
