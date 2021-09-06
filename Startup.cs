using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WattEsportsCore.Data;
using WattEsportsCore.Models;
using WattEsportsCore.Services;

namespace WattEsportsCore
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

            // FOR LIVE DATABASE
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySQL(
                    Configuration.GetConnectionString("SteveDatabaseConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            // FOR LOCAL DATABASE
            //services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseSqlServer(
            //  Configuration.GetConnectionString("DefaultConnection")));
            //services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddIdentity<IdentityUser, ApplicationRole>(
                        options =>
                        {
                            options.Stores.MaxLengthForKeys = 128;
                            options.SignIn.RequireConfirmedEmail = true;
                        })
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultUI()
                    .AddDefaultTokenProviders();
            services.AddControllersWithViews();


            services.AddTransient<IEmailSender, EmailSender>();
            services.Configure<SendGridOptions>(Configuration.GetSection("SendGrid"));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            ApplicationDbContext _context,
            RoleManager<ApplicationRole> _roleManager,
            UserManager<IdentityUser> _userManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
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

            app.UseCookiePolicy();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                    name: "TeamAreaValorant",
                    areaName: "Team",
                    pattern: "Team/{controller=Valorant}/{action=Index}/{id?}");

                endpoints.MapAreaControllerRoute(
                    name: "TeamAreaCSGO",
                    areaName: "Team",
                    pattern: "Team/{controller=CSGO}/{action=Index}/{id?}");

                endpoints.MapAreaControllerRoute(
                    name: "TeamAreaLeagueOfLegends",
                    areaName: "Team",
                    pattern: "Team/{controller=LeagueOfLegends}/{action=Index}/{id?}");


                endpoints.MapAreaControllerRoute(
                    name: "TeamAreaRainbowSix",
                    areaName: "Team",
                    pattern: "Team/{controller=RainbowSix}/{action=Index}/{id?}");


                endpoints.MapAreaControllerRoute(
                    name: "TeamAreaRocketLeague",
                    areaName: "Team",
                    pattern: "Team/{controller=RocketLeague}/{action=Index}/{id?}");

                endpoints.MapAreaControllerRoute(
                    name: "AdminCommittee",
                    areaName: "Admin",
                    pattern: "Admin/{controller=Committees}/{action=Index}/{id?}");

                endpoints.MapAreaControllerRoute(
                  name: "GameLeadsValorant",
                  areaName: "GameLead",
                  pattern: "GameLead/{controller=Valorants}/{action=Index}/{id?}");

                endpoints.MapAreaControllerRoute(
                  name: "GameLeadsCSGO",
                  areaName: "GameLead",
                  pattern: "GameLead/{controller=Counterstrikes}/{action=Index}/{id?}");


                endpoints.MapAreaControllerRoute(
                  name: "GameLeadsLeagueOfLegends",
                  areaName: "GameLead",
                  pattern: "GameLead/{controller=LeagueOfLegends}/{action=Index}/{id?}");

                endpoints.MapAreaControllerRoute(
                  name: "GameLeadsRainbowSix",
                  areaName: "GameLead",
                  pattern: "GameLead/{controller=RainbowSixes}/{action=Index}/{id?}");

                endpoints.MapAreaControllerRoute(
                    name: "GameLeadsRocketLeague",
                    areaName: "GameLead",
                    pattern: "GameLead/{controller=RocketLeagues}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();

                app.UseForwardedHeaders(new ForwardedHeadersOptions
                {
                    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
                });

                //DummyData.Initialize(_context, _userManager, _roleManager).Wait();

            });
        }
    }
}
