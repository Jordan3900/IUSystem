using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using IUSystem.Data;
using IUSystem.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using System;
using IUSystem.Constans;
using IUSystem.Middlewares.MiddlewareExtensions;

namespace IUSystem
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
                    Configuration.GetConnectionString("DefaultConnection")).UseLazyLoadingProxies());

            services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddIdentityServer()
                .AddApiAuthorization<IdentityUser, ApplicationDbContext>();

            services.AddAuthentication()
                .AddIdentityServerJwt();


            services.AddControllersWithViews();
            services.AddRazorPages();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider services)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseSeedDataMiddleware();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
            CreateRoles(services).Wait();
        }
        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            string[] roleNames = {Constants.ADMIN_ROLE, Constants.FACILITATOR_ROLE, Constants.LEAD_USER_ROLE, Constants.USER_ROLE };
            IdentityResult roleResult;

            //create roles
            foreach (var roleName in roleNames)
            {
                var roleExists = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExists)
                {
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            IdentityUser admin = await UserManager.FindByEmailAsync(Constants.ADMIN_EMAIL);

            if (admin == null)
            {
                admin = new IdentityUser()
                {
                    UserName = Constants.ADMIN_EMAIL,
                    Email = Constants.ADMIN_EMAIL
                };
                await UserManager.CreateAsync(admin, Constants.SEED_USERS_PASSWORD);
            }
            await UserManager.AddToRoleAsync(admin, Constants.ADMIN_ROLE);
        }
        private async Task SeedData(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            string[] roleNames = { Constants.ADMIN_ROLE, Constants.FACILITATOR_ROLE, Constants.LEAD_USER_ROLE, Constants.USER_ROLE };
            IdentityResult roleResult;

            //create roles
            foreach (var roleName in roleNames)
            {
                var roleExists = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExists)
                {
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            IdentityUser admin = await UserManager.FindByEmailAsync(Constants.ADMIN_EMAIL);

            if (admin == null)
            {
                admin = new IdentityUser()
                {
                    UserName = Constants.ADMIN_EMAIL,
                    Email = Constants.ADMIN_EMAIL
                };
                await UserManager.CreateAsync(admin, Constants.SEED_USERS_PASSWORD);
            }
            await UserManager.AddToRoleAsync(admin, Constants.ADMIN_ROLE);
        }
    }
}
