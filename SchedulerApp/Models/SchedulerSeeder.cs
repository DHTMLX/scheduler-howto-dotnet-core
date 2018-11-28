using System;
using System.Collections.Generic;
using System.Linq;

namespace SchedulerApp.Models
{
    public static class SchedulerSeeder
    {
        public static void Seed(SchedulerContext context)
        {
            if (context.Events.Any())
            {
                return;   // DB has been seeded
            }

            var events = new List<SchedulerEvent>()
            {
                new SchedulerEvent
                {
                    Name = "Event 1",
                    StartDate = new DateTime(2019, 1, 15, 2, 0, 0),
                    EndDate = new DateTime(2019, 1, 15, 4, 0, 0)
                },
                new SchedulerEvent()
                {
                    Name = "Event 2",
                    StartDate = new DateTime(2019, 1, 17, 3, 0, 0),
                    EndDate = new DateTime(2019, 1, 17, 6, 0, 0)
                },
                new SchedulerEvent()
                {
                    Name = "Multiday event",
                    StartDate = new DateTime(2019, 1, 15, 0, 0, 0),
                    EndDate = new DateTime(2019, 1, 20, 0, 0, 0)
                }
            };

            events.ForEach(s => context.Events.Add(s));
            context.SaveChanges();
        }
    }
}
