using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProiectTi.Services;
using ProiectTi.Services.Abstractions;
using Rotativa.AspNetCore;
using ServiceStack.OrmLite;

namespace ProiectTi
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
            services.AddControllersWithViews();
            services.AddRouting(x => x.LowercaseUrls = true);

            var connectionString = Configuration.GetConnectionString("ProiectTi");
            var connectionFactory =
                new OrmLiteConnectionFactory(connectionString, MySqlDialect.Provider);
            OrmLiteConfig.DialectProvider.GetStringConverter().UseUnicode = true;

            services.AddScoped(s => connectionFactory.OpenDbConnection())
                .AddScoped<IEmployeeRepository, EmployeeRepository>()
                .AddScoped<IDtoMapper, DtoMapper>();


            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(config =>
                {
                    config.LoginPath = $"/account/signin";
                    config.LogoutPath = $"/account/logout";
                    config.AccessDeniedPath = $"/account/accessdenied";
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseExceptionHandler("/Home/Error");

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });

            RotativaConfiguration.Setup(env.WebRootPath, "C:\\Program Files\\wkhtmltopdf\\bin");
        }
    }
}