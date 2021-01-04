using AutoMapper;
using Limak.az.Contexts;
using Limak.az.Data;
using Limak.az.Interfaces;
using Limak.az.Models;
using Limak.az.Repos;
using Limak.az.ViewModels;
using Limak.ViewModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Limak.az
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


            services.AddDbContext<LimakDbContext>(
                option => option.UseSqlServer("Server=DESKTOP-QL20RPD\\SQLEXPRESS;Database=LimakProjectDb;Trusted_Connection=True;"));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IDeclarationRepository, DeclarationRepository>();
            services.AddScoped<IUserBalanceRepository, UserBalanceRepository>();

            services.AddIdentity<CustomAppUser, IdentityRole>(options =>
                {
                    options.SignIn.RequireConfirmedEmail = true;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                })
            .AddEntityFrameworkStores<LimakDbContext>()
            .AddDefaultTokenProviders();


            services.AddControllersWithViews();
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new RegisterViewModelProfile());
                mc.AddProfile(new OrderViewModelProfile());
                mc.AddProfile(new DeclarationViewModelProfile());
                mc.AddProfile(new SettingsViewModelProfile());
            });
            #region Email configuration

            #endregion
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            //services.AddMvc(option => option.EnableEndpointRouting = false);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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

            app.SeedRole();

            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //      name: "areas",
            //      template: "{area:exists}/{controller=Account}/{action=GetAllUsers}/{id?}");
            //});

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Account}/{action=Login}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
