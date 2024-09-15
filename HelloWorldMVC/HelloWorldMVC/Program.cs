var builder = WebApplication.CreateBuilder(args);
//register Controllers & Views to the routing
builder.Services.AddControllersWithViews();
var app = builder.Build();
//define the default routing url  when project initialize in hosting
//eg : server://host
app.MapControllerRoute(name:"default", pattern: "{controller=Home}/{action=Index}/{id?}");
//map all Controllers and Action Methods
app.MapDefaultControllerRoute();
app.Run();
