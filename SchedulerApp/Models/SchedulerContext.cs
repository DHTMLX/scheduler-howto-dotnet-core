using Microsoft.EntityFrameworkCore;

namespace SchedulerApp.Models
{
    public class SchedulerContext : DbContext
    {
        public SchedulerContext(DbContextOptions<SchedulerContext> options)
           : base(options)
        {
        }
        public DbSet<SchedulerEvent> Events { get; set; }
        public DbSet<SchedulerRecurringEvent> RecurringEvents { get; set; }
    }
}
