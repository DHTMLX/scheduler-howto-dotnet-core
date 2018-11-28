using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using SchedulerApp.Models;

namespace SchedulerApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args)
                .Build()
                .InitializeDatabase()
                .Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
