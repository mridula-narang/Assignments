
using EFWebApiApp.Contexts;
using EFWebApiApp.Interfaces;
using EFWebApiApp.Models;
using EFWebApiApp.Repositories;
using EFWebApiApp.Services;
using Microsoft.EntityFrameworkCore;

namespace EFWebApiApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            #region Contexts
            builder.Services.AddDbContext<ShoppingContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            #endregion

            #region Repositories
            builder.Services.AddScoped<IRepository<int, Customer>, CustomerRepository>();
            builder.Services.AddScoped<IRepository<int, Product>, ProductRepository>();
            #endregion

            #region OtherServices
            builder.Services.AddAutoMapper(typeof(Customer));
            builder.Services.AddAutoMapper(typeof(Product));
            #endregion

            #region Services
            builder.Services.AddScoped<ICustomerBasicService, CustomerBasicService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            #endregion

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
