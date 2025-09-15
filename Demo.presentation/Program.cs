using Demo.DataAccess.Data.Contexts;
using Demo.DataAccess.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Demo.presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            #region DI Container
            builder.Services.AddControllersWithViews(); 

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            { 
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")!);
            });

            builder.Services.AddScoped<IDepartmentRepository,DepartmentRepository>();
            //ask u to create instance of DepartmentRepository class whenever u need IDepartmentRepository interface
            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts(); //middleware that make sure all requests to be https
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

           

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}


