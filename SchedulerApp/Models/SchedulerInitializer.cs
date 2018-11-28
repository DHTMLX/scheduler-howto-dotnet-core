using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;

namespace SchedulerApp.Models
{
    public static class SchedulerInitializerExtension
    {
        public static IWebHost InitializeDatabase(this IWebHost webHost)
        {
            var serviceScopeFactory =
             (IServiceScopeFactory)webHost.Services.GetService(typeof(IServiceScopeFactory));

            using (var scope = serviceScopeFactory.CreateScope())
            {
                var services = scope.ServiceProvider;
                var dbContext = services.GetRequiredService<SchedulerContext>();
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();
                SchedulerSeeder.Seed(dbContext);
            }

            return webHost;
        }
    }
}
