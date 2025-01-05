using CloudHRMS.DAO;
using CloudHRMS.Services;
using CloudHRMS.UnitOfWorks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
// Add services to the container.
builder.Services.AddControllersWithViews();
var config = builder.Configuration;//declare the configuration to read connection string of appSetting.json
//add the dbContext that we defined the ApplicationDbContext to get connection string name
/*Database Connection with MS SQL
builder.Services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(config.GetConnectionString("CloudHRMSConnectingString")));
*/
//Database Connection with MySQL
/*
var connectionString = config.GetConnectionString("CloudHRMSConnectingStringMySQL");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString,
    new MySqlServerVersion(new Version(8, 0, 35)),
    mySqlOptions => mySqlOptions.EnableRetryOnFailure()));
*/
//adding the connection to the PostgreSQL database
builder.Services.AddDbContext<ApplicationDbContext>(options =>options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
//Register for Identity UIs
builder.Services.AddRazorPages();
//Register for Identity dbContext for related Identity User and Roles.
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
//Register the Service Of Position Service
builder.Services.AddScoped<IPositionService, PositionService>();
//Register for UnitOfWork : DatabaseConnection,UnitOfWork:The Service is created once per HTTP request ('scope')
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
//Register the user service to be use.
builder.Services.AddScoped<IUserService, UserService>();
//Register the Reporting service to be use.
builder.Services.AddScoped<IReportingService, ReportingService>();

/*
// Adding Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})

// Adding Jwt Bearer
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = configuration["JWT:ValidAudience"],
        ValidIssuer = configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
    };
});
*/
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
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
