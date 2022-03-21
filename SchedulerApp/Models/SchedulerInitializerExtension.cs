namespace SchedulerApp.Models
{
    public static class SchedulerInitializerExtension
    {
        public static IHost InitializeDatabase(this IHost webHost)
        {
            var serviceScopeFactory =
            (IServiceScopeFactory?)webHost.Services.GetService(typeof(IServiceScopeFactory));

            using (var scope = serviceScopeFactory!.CreateScope())
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