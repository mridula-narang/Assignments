using AppointmentManagementApp.Interfaces;
using AppointmentManagementApp.Models;
using AppointmentManagementApp.Repositories;
using AppointmentManagementApp.Services;

namespace AppointmentManagementApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddSession();
            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<ILoginService, PatientLoginService>();
            builder.Services.AddScoped<IAppointmentService, AppointmentService>();


            builder.Services.AddScoped<IRepository<string, Patient>, PatientRepository>();
            builder.Services.AddScoped<IRepository<string, Doctor>, DoctorRepository>();
            builder.Services.AddScoped<IRepository<int, Appointment>,AppointmentRepository>();

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
                pattern: "{controller=Login}/{action=PatientLogin}/{id?}");

            app.Run();
        }
    }
}
