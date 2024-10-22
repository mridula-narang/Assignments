
using EventBookingApp.Contexts;
using EventBookingApp.Interfaces;
using EventBookingApp.Models;
using EventBookingApp.Repositories;
using EventBookingApp.Services;
using Microsoft.EntityFrameworkCore;

namespace EventBookingApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            #region Contexts
            builder.Services.AddDbContext<EventContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            #endregion

            #region Repositories
            builder.Services.AddScoped<IRepository<int, Event>, EventRepository>();
            builder.Services.AddScoped<IRepository<int, Employee>, EmployeeRepository>();
            builder.Services.AddScoped<IRepository<int, Booking>, BookingRepository>();
            #endregion

            #region OtherServices
            builder.Services.AddAutoMapper(typeof(Employee));
            builder.Services.AddAutoMapper(typeof(Event));
            builder.Services.AddAutoMapper(typeof(Booking));
            #endregion

            #region Services
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
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

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
