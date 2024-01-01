namespace SalesWebMVC;

using Data;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Localization;
using Services;

public interface IStartup
  {
    IConfiguration Configuration { get; }
    public void ConfigureServices(IServiceCollection service);
    public void Configure(WebApplication app, IWebHostEnvironment environment);
  }
public class Startup : IStartup
  {
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection service)
    {
      var connectionString = Configuration.GetConnectionString("SalesWebMVCContext");
      var serverVersion = ServerVersion.AutoDetect(connectionString);
      service.AddDbContext<SalesWebMVCContext>(options => options.UseMySql(connectionString,
      serverVersion,
      builder => builder.MigrationsAssembly("SalesWebMVC")));
      service.AddScoped<SeedingService>();
      service.AddScoped<SellerService>();
      service.AddScoped<DepartmentService>();
      service.AddScoped<SalesRecordService>();
      service.AddControllersWithViews();
    }

    public void Configure(WebApplication app, IWebHostEnvironment environment)
    {
      var enUS = new CultureInfo("en-US");
      
      var localizationOptions = new RequestLocalizationOptions()
                                {
                                  DefaultRequestCulture = new RequestCulture(enUS),
                                  SupportedCultures = new List<CultureInfo>(){ enUS},
                                  SupportedUICultures = new List<CultureInfo>(){ enUS}
                                };
      
      app.UseRequestLocalization(localizationOptions);
      
      if (!app.Environment.IsDevelopment())
      {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
      }

      app.Services.CreateScope().ServiceProvider.GetRequiredService<SeedingService>().Seed();

      app.UseHttpsRedirection();
      app.UseStaticFiles();

      app.UseRouting();

      app.UseAuthorization();

      app.MapControllerRoute(
      name: "default",
      pattern: "{controller=Home}/{action=Index}/{id?}");
    }
  }
public static class StartupExtensions
  {
    public static WebApplicationBuilder UseStartup<TStartup>(this WebApplicationBuilder webAppBuilder)
        where TStartup : IStartup
    {
      var startup = Activator.CreateInstance(typeof(TStartup), webAppBuilder.Configuration) as IStartup;
      if (startup == null)
      {
        throw new ArgumentException("Classe Startup.cs inválida!");
      }

      startup.ConfigureServices(webAppBuilder.Services);
      var app = webAppBuilder.Build();

      startup.Configure(app, app.Environment);
      app.Run();

      return webAppBuilder;
    }
  }
