namespace SchedulerApp.Models
{
    public class SchedulerRecurringEvent
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int EventPID { get; set; }
        public string? RecType { get; set; }
        public long EventLength { get; set; }

    }
}
