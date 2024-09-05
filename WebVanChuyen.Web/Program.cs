using WebVanChuyen.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebVanChuyen.Models;
using WebVanChuyen.Web.Services;
using WebVanChuyen.App.Models;
using Microsoft.Extensions.Options;
using WebVanChuyen.Web.Services.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Using SQL Server
var conn = builder.Configuration.GetConnectionString("DBConnection");
builder.Services.AddDbContext<DataContextUser>(options => options.UseSqlServer(conn));

// Add Identity
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 5;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.SignIn.RequireConfirmedAccount = false;
}).AddEntityFrameworkStores<DataContextUser>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = new PathString("/account/login");
    options.LogoutPath = new PathString("/account/logout");
    options.AccessDeniedPath = new PathString("/account/accessdenied");

    options.SlidingExpiration = true;
    options.ExpireTimeSpan = TimeSpan.FromHours(1);

    /*
    options.Cookie = new()
    {
        Name = "UserCookie",
    };
    */

    // Set cookie expire if not accessed in set of time - default dotnet is haft off timespan
    //options.SlidingExpiration = true;
    //options.ExpireTimeSpan = TimeSpan.FromDays(7);
});

builder.Services.AddAuthorization(options =>
{
    //options.AddPolicy("", policy => policy.RequireClaim("", ""));

    // RequireRole
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("ManagerOnly", policy => policy.RequireRole("Manager"));
    options.AddPolicy("StaffOnly", policy => policy.RequireRole("Staff"));
    options.AddPolicy("DriverRole", policy => policy.RequireRole("Driver"));
    options.AddPolicy("ShipperRole", policy => policy.RequireRole("Shipper"));
    options.AddPolicy("Manager&Staff", policy => policy.RequireRole("Manager", "Staff"));

    /*
    options.AddPolicy("HRManageOnly", policy => policy
        .RequireClaim("Department", "HR")
        .RequireClaim("Manager")
        .Requirements.Add(new HRManagerProbationRequirement(3))
        );
    */
});

// Add API Services
builder.Services.AddHttpClient<IEmployeeService, EmployeeService>("Test", client => client.BaseAddress = new Uri("http://localhost:5255/"));
builder.Services.AddHttpClient<IPackageService, PackageService>("Test", client => client.BaseAddress = new Uri("http://localhost:5255/"));
builder.Services.AddHttpClient<IProductService, ProductService>("Test", client => client.BaseAddress = new Uri("http://localhost:5255/"));
builder.Services.AddHttpClient<IShipmentTripService, ShipmentTripService>("Test", client => client.BaseAddress = new Uri("http://localhost:5255/"));
builder.Services.AddHttpClient<IWarehouseService, WarehouseService>("Test", client => client.BaseAddress = new Uri("http://localhost:5255/"));
builder.Services.AddHttpClient<IRoutePathService, RoutePathService>("Test", client => client.BaseAddress = new Uri("http://localhost:5255/"));

// Auto map models
builder.Services.AddAutoMapper(typeof(ModelsMap));

// Add transfer data between component
builder.Services.AddSingleton<PackageStateContainer>();
builder.Services.AddSingleton<ShipmentTripStateContainer>();

// 



var app = builder.Build();

// Seeding User & Role
var services = app.Services.CreateScope().ServiceProvider;
await DataContextSeedRole.Initialize(services);
await DataContextSeedUser.InitializeAsync(services);


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}


app.UseStaticFiles();

app.UseRouting();

// Add Middleware
app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
