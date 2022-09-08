
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pri.Identity.Web.Data;
using Pri.Identity.Web.Entities;
using Pri.Identity.Web.Areas.Identity.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity configuration
builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    // Password configurations
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireDigit = true;
    options.Password.RequiredUniqueChars = 1;
    // User configurations
    options.User.RequireUniqueEmail = true;
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 -._@+";
    // SignIn options
    options.SignIn.RequireConfirmedAccount = true;
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;
    // Lockout options
    options.Lockout.AllowedForNewUsers = true;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
    options.Lockout.MaxFailedAccessAttempts = 5;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();



//Configuratie e-mail ondersteuning        
builder.Services.AddSingleton<IEmailSender, EmailSender>();
builder.Services.Configure<AuthMessageSenderOptions>(builder.Configuration);


builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();


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
app.MapRazorPages();
app.Run();