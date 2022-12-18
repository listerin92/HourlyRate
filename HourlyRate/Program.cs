using HourlyRate.Extensions;
using HourlyRate.Infrastructure.Data;
using HourlyRate.Infrastructure.Data.Models.Account;
using HouseRentingSystem.ModelBinders;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<UserIdentityExt>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequiredLength = 6;

        options.User.RequireUniqueEmail = true;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews()
    .AddMvcOptions(options =>
    {
        options.ModelBinderProviders.Insert(0, new DecimalModelBinderProvider());
    });
// Add Sidebar menu json file
builder.Configuration.AddJsonFile("sidebar.json", optional: true, reloadOnChange: true);

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(5);
    options.Cookie.HttpOnly = true;
});
builder.Services.AddResponseCaching();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("My Policy", policy =>
    {
        policy.RequireRole("Administrator");
        policy.RequireClaim("CompanyId");
    });
});

builder.Services.AddApplicationServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseResponseCaching();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseStatusCodePagesWithReExecute("/Home/HandleError/{0}");

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.UseEndpoints(endpoints =>
{
    endpoints.MapAreaControllerRoute(
            name: "Identity",
            areaName: "Identity",
            pattern: "Identity/{controller=Home}/{action=Index}");
    endpoints.MapControllers();
});
app.MapRazorPages();
    
app.Run();
