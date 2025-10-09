using Demo.BusinessLogic.Mappings;
using Demo.BusinessLogic.Services.AttachmentService.Classes;
using Demo.BusinessLogic.Services.AttachmentService.Interfaces;
using Demo.BusinessLogic.Services.Classes;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.DataAccess.Data.Contexts;
using Demo.DataAccess.Data.Repositories.Classes;
using Demo.DataAccess.Data.Repositories.Interfaces;
using Demo.DataAccess.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
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
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"));
                options.UseLazyLoadingProxies();

            });

            builder.Services.AddScoped<IDepartmentRepository,DepartmentRepository>();
            //ask u to create instance of DepartmentRepository class whenever u need IDepartmentRepository interface

            builder.Services.AddScoped<IDepartmentService, DepartmentService>();

            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            //builder.Services.AddAutoMapper(cfg => { },typeof(MappingProfile).Assembly);
            builder.Services.AddAutoMapper(Mapping=>Mapping.AddProfile(new MappingProfile()));

            builder.Services.AddScoped<IEmployeeService, EmployeeService>();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddScoped<IAttachmentService, AttachmentService>();


            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>() 
    .AddDefaultTokenProviders();
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

            app.UseAuthentication(); //Identity
            app.UseAuthorization();



            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Register}/{id?}");

            app.Run();
        }
    }
}


