using System;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using WebStore.DAL.Context;
using WebStore.Data;
using WebStore.Domain.Entities.Identity;
using WebStore.Infrastructure.Conventions;
using WebStore.Infrastructure.MiddleWare;
using WebStore.Services.InCookies;
using WebStore.Services.InMemory;
using WebStore.Services.InSQL;
using WebStore.Interfaces.Services;
using WebStore.Interfaces.TestAPI;
using WebStore.WebAPI.Values;
using WebStore.WebAPI.Clients.Employees;
using WebStore.WebAPI.Clients.Products;
using WebStore.WebAPI.Clients.Orders;
using WebStore.WebAPI.Clients.Identity;
using Microsoft.Extensions.Logging;
using WebStore.Logger;
using WebStore.Services.Services;

namespace WebStore
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration Configuration) => this.Configuration = Configuration;

        public void ConfigureServices(IServiceCollection services)
        {



            services.AddIdentity<User, Role>()
               .AddIdentityWebStoreWebAPIClients()
               .AddDefaultTokenProviders();

           // services.AddIdentityWebStoreWebAPIClients();

            services.Configure<IdentityOptions>(opt =>
            {
#if DEBUG
                opt.Password.RequireDigit = false;
                opt.Password.RequiredLength = 3;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredUniqueChars = 3;
#endif

                opt.User.RequireUniqueEmail = false;
                opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

                opt.Lockout.AllowedForNewUsers = false;
                opt.Lockout.MaxFailedAccessAttempts = 10;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
            });

            services.ConfigureApplicationCookie(opt =>
            {
                opt.Cookie.Name = "WebStore.GB";
                opt.Cookie.HttpOnly = true;
                opt.ExpireTimeSpan = TimeSpan.FromDays(10);

                opt.LoginPath = "/Account/Login";
                opt.LogoutPath = "/Account/Logout";
                opt.AccessDeniedPath = "/Account/AccessDenied";

                opt.SlidingExpiration = true;
            });

            //services.AddSingleton<IEmployeesData, InMemoryEmployesData>();  // Объект InMemoryEmployesData создаётся один раз на всё время работы приложения
            //  services.AddScoped<IEmployeesData, SqlEmployeesData>();
            services.AddScoped<ICartStore, InCookiesCartStore>();
            services.AddScoped<ICartService, CartService>();
            //if (Configuration["ProductsDataSource"] == "db")
            //services.AddScoped<IProductData, SqlProductData>();
            //else
            //    services.AddSingleton<IProductData, InMemoryProductData>();

            //services.AddScoped<IOrderService, SqlOrderService>();
            services.AddHttpClient("WebStoreApi", client => client.BaseAddress = new Uri(Configuration["WebAPI"]))
               .AddTypedClient<IValuesService, ValuesClient>()
               .AddTypedClient<IEmployeesData, EmployeesClient>()
               .AddTypedClient<IProductData, ProductsClient>()
               .AddTypedClient<IOrderService, OrdersClient>()
  
               ;
            //services.AddScoped<IValuesService, ValuesClient>();
            //services.AddHttpClient<IValuesService, ValuesClient>(client => client.BaseAddress = new Uri(Configuration["WebAPI"]));
            //services.AddHttpClient<IEmployeesData, EmployeesClient>(client => client.BaseAddress = new Uri(Configuration["WebAPI"]));

            services.AddControllersWithViews(/*opt => opt.Conventions.Add(new TestControllersConvention())*/)
               .AddRazorRuntimeCompilation();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory Log)
        {
            Log.AddLog4Net();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

           // app.UseMiddleware<TestMiddleWare>();

            app.UseWelcomePage("/WelcomePage");

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapGet("/greetings", async context =>
                //{
                //    await context.Response.WriteAsync(Configuration["Greetings"]);
                //});

                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );

                endpoints.MapControllerRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
