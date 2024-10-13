using CloudHRMS.DAO;
using CloudHRMS.Repositories;
using CloudHRMS.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var config = builder.Configuration;//declare the configuration to read connection string of appSetting.json

//add the dbContext that we defined the ApplicationDbContext to get connection string name
builder.Services.AddDbContext<ApplicationDbContext>(option =>
                            option.UseSqlServer(config.GetConnectionString("CloudHRMSConnectingString")));
//Register the Services
builder.Services.AddScoped<IPositionService, PositionService>();
//Register the Repository
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
