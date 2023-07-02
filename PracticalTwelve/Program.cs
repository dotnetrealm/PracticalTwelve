using PracticalTwelve.Data.Interfaces;
using PracticalTwelve.Data.Repositories;

namespace PracticalTwelve
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            builder.Services.AddResponseCaching();

            builder.Services.AddRouting();

            builder.Services.AddSingleton<ITestOneRepository, TestOneRepository>();
            builder.Services.AddSingleton<ITestTwoRepository, TestTwoRepository>();
            builder.Services.AddSingleton<ITestThreeRepository, TestThreeRepository>();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error/500");
            }

            app.UseResponseCaching();

            app.UseStatusCodePagesWithReExecute("/Error/{0}");

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.MapControllerRoute(
                name: "Default",
                pattern: "{controller=TestOne}/{action=Index}/{id?}"
            );

            app.Run();
        }
    }
}