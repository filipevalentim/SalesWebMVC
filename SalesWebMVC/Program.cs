using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SalesWebMVC.Data;
using SalesWebMVC.Services;

namespace SalesWebMVC
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var builder = WebApplication.CreateBuilder(args);      
      var connectionString = builder.Configuration.GetConnectionString("SalesWebMVCContext");
      var serverVersion = ServerVersion.AutoDetect(connectionString);
      builder.Services.AddDbContext<SalesWebMVCContext>
        (options => options.UseMySql(connectionString, serverVersion, builder => builder.MigrationsAssembly("SalesWebMVC")));

      builder.Services.AddScoped<SeedingService>();
      builder.Services.AddScoped<SellerService>();
      // Add services to the container.
      builder.Services.AddControllersWithViews();

      var app = builder.Build();

      // Configure the HTTP request pipeline.
      if (!app.Environment.IsDevelopment())
      {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();

      }

      app.Services.CreateScope().ServiceProvider.GetService<SeedingService>().Seed();

      app.UseHttpsRedirection();
      app.UseStaticFiles();

      app.UseRouting();

      app.UseAuthorization();

      app.MapControllerRoute(
          name: "default",
          pattern: "{controller=Home}/{action=Index}/{id?}");

      app.Run();
    }
  }
}