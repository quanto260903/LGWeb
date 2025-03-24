using DataAccess.EFCore;
using DataAccess.EFCore.Repositories;
using DataAccess.EFCore.UnitOfWorks;
using Domain.Interfaces;
using LGWebsite;
using LGWebsite.Services;
using LGWebsite.Services.Common;
using LGWebsite.Services.IService;
using LGWebsite.Services.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.Get<AppSettings>();
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<LgwebsiteContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IConfigurationRepository, ConfigurationRepository>();
builder.Services.AddScoped<ISlideRepository, SlideRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddScoped<IMenuRepository, MenuRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ISystemConfigRepository, SystemConFigRepository>();
builder.Services.AddScoped<IVideoRepository, VideoRepository>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWorks>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<EmailUtil>();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        //options.Cookie.Name = "username";
        options.LoginPath = "/Account/Login"; // Set the login path
        options.LogoutPath = "/Account/Logout"; // Set the logout path
        options.AccessDeniedPath = "/Account/AccessDenied"; // Set the access denied path
        options.SlidingExpiration = true; // Enable sliding expiration
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Set cookie expiration time
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // gui cookie qua HTTPS
        options.Cookie.SameSite = SameSiteMode.Strict;
    });

builder.Services.AddScoped<ISlideService, SlideService>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IConfigurationService, ConfigurationService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IBlogsCategoryService, BlogsCategoryService>();
builder.Services.AddScoped<ISystemConfigService, SystemConfigService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMenuService, MenuService>();
builder.Services.AddScoped<IVideoService, VideoService>();
builder.Services.AddScoped<IAccountService, AccountService>();


builder.Services.AddAuthorization();
builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000")
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .AllowCredentials();
        });
});


var app = builder.Build();

app.UseCors("AllowSpecificOrigin");
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
