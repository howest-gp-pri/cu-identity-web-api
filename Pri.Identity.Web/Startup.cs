using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pri.Identity.Web.Areas.Identity.Services;
using Pri.Identity.Web.Data;
using Pri.Identity.Web.Entities;
using System;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace Pri.Identity.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            // Identity configuration
            services.AddDefaultIdentity<ApplicationUser>(options =>
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

            //services.AddAuthentication()
            //    .AddGoogle(options =>
            //    {
            //        options.ClientId = Configuration["Authentication:Google:ClientId"];
            //        options.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
            //        options.UserInformationEndpoint = "https://openidconnect.googleapis.com/v1/userinfo";
            //        options.ClaimActions.Clear();
            //        options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "sub");
            //        options.ClaimActions.MapJsonKey(ClaimTypes.Email, "email");
            //    });

            //Configuratie e-mail ondersteuning        
            services.AddSingleton<IEmailSender, EmailSender>();
            services.Configure<AuthMessageSenderOptions>(Configuration);


            services.AddControllersWithViews();
            services.AddRazorPages();
            //services.AddRazorPages(options =>
            //{
            //    options.Conventions.AuthorizePage("/Contact");
            //    options.Conventions.AuthorizeFolder("/Private");
            //    options.Conventions.AuthorizeAreaPage("Identity", "/Manage/Accounts");
            //    options.Conventions.AuthorizeAreaFolder("Identity", "/Manage");
            //    options.Conventions.AllowAnonymousToPage("/Private/PublicPage");
            //    options.Conventions.AllowAnonymousToFolder("/Private/PublicPages");

            //    options.Conventions.AuthorizeFolder("/Private").AllowAnonymousToPage("/Private/Public");
            //});

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
