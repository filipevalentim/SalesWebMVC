namespace SalesWebMVC
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var builder = WebApplication.CreateBuilder(args).UseStartup<Startup>();
    }
  }
}