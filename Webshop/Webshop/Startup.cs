using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Webshop.Auth;
using Webshop.Models;

namespace Webshop
{
    public class Startup
    {
        private readonly IConfigurationRoot _configurationRoot;

        public Startup(IHostingEnvironment hostingEnvironment)
        {
            _configurationRoot = new ConfigurationBuilder()
                .SetBasePath(hostingEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{hostingEnvironment.EnvironmentName}.json", true)
                .Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(_configurationRoot.GetConnectionString("DefaultConnection")));
            services.AddIdentity<ApplicationUser, IdentityRole>(options => {
                        //options.Password.RequiredLength = 8;
                        //options.Password.RequireNonAlphanumeric = true;
                        //options.Password.RequireUppercase = true;
                        options.User.RequireUniqueEmail = true;
                    })
                    .AddEntityFrameworkStores<AppDbContext>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped(ShoppingCart.GetCart);
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddMvc();
            // Claims based
            services.AddAuthorization(options => {
                options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
                options.AddPolicy("DeleteProduct", policy => policy.RequireClaim("Delete Product", "Delete Product"));
                options.AddPolicy("AddProduct", policy => policy.RequireClaim("Add Product", "Add Product"));
            });
            services.AddMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            ILoggerFactory loggerFactory) //DbInitializer seed
        {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }
            else {
                app.UseExceptionHandler("/AppException");
            }

            ServeFromDirectory(app, env, "node_modules");
            ServeFromDirectory(app, env, "src");


            //app.UseStaticFiles();
            app.UseSession();
            app.UseAuthentication();

            //app.UseMvcWithDefaultRoute();
            app.UseMvc(routes => {
                routes.MapRoute(
                    "categoryfilter",
                    "Product/{action}/{category?}",
                    new { Controller = "Product", action = "List" });
                routes.MapRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });

            //seed.Seed(app);
            //DbInitializer.Seed(app);
        }

        public void ServeFromDirectory(IApplicationBuilder app, IHostingEnvironment env, string path)
        {
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(env.ContentRootPath, path)
                ),
                RequestPath = "/" + path
            });
        }




        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
        {
            public AppDbContext CreateDbContext(string[] args)
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
                var builder = new DbContextOptionsBuilder<AppDbContext>();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                builder.UseSqlServer(connectionString);
                return new AppDbContext(builder.Options);
            }
        }
    }
}