using System.Text.Encodings.Web;

namespace SchedulerApp.Models
{
    public class WebAPIRecurring
    {
        public int id { get; set; }
        public string? text { get; set; }
        public string? start_date { get; set; }
        public string? end_date { get; set; }

        public int? event_pid { get; set; }
        public string? rec_type { get; set; }
        public long? event_length { get; set; }

        public static explicit operator WebAPIRecurring(SchedulerRecurringEvent ev)
        {
            return new WebAPIRecurring
            {
                id = ev.Id,
                text = HtmlEncoder.Default.Encode(ev.Name != null ? ev.Name : ""),
                start_date = ev.StartDate.ToString("yyyy-MM-dd HH:mm"),
                end_date = ev.EndDate.ToString("yyyy-MM-dd HH:mm"),

                event_pid = ev.EventPID,
                rec_type = ev.RecType,
                event_length = ev.EventLength
            };
        }

        public static explicit operator SchedulerRecurringEvent(WebAPIRecurring ev)
        {
            return new SchedulerRecurringEvent
            {
                Id = ev.id,
                Name = ev.text,
                StartDate = ev.start_date != null ? DateTime.Parse(ev.start_date,
                    System.Globalization.CultureInfo.InvariantCulture) : new DateTime(),
                EndDate = ev.end_date != null ? DateTime.Parse(ev.end_date,
                    System.Globalization.CultureInfo.InvariantCulture) : new DateTime(),

                EventPID = ev.event_pid != null ? ev.event_pid.Value : 0,
                EventLength = ev.event_length != null ? ev.event_length.Value : 0,
                RecType = ev.rec_type
            };
        }
    }
}
