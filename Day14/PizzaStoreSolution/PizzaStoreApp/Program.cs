using PizzaStoreApp.Interfaces;
using PizzaStoreApp.Models;
using PizzaStoreApp.Repositories;
using PizzaStoreApp.Services;

namespace PizzaStoreApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddSession(); 

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IPizzaService,PizzaService>();
            builder.Services.AddScoped<ILoginService, LoginService>();


            builder.Services.AddScoped<IRepository<int, PizzaImages>,PizzaImageRepository>();
            builder.Services.AddScoped<IRepository<int, Pizza>, PizzaRepository>();
            builder.Services.AddScoped<IRepository<string, User>, UserRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Login}/{action=UserLogin}/{id?}");

            app.Run();
        }
    }
}
