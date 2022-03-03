using System.Text.Encodings.Web;

namespace SchedulerApp.Models
{
    public class WebAPIEvent
    {
        public int id { get; set; }
        public string? text { get; set; }
        public string? start_date { get; set; }
        public string? end_date { get; set; }

        public static explicit operator WebAPIEvent(SchedulerEvent ev)
        {
            return new WebAPIEvent
            {
                id = ev.Id,
                text = HtmlEncoder.Default.Encode(ev.Name != null ? ev.Name : ""),
                start_date = ev.StartDate.ToString("yyyy-MM-dd HH:mm"),
                end_date = ev.EndDate.ToString("yyyy-MM-dd HH:mm"),
            };
        }

        public static explicit operator SchedulerEvent(WebAPIEvent ev)
        {
            return new SchedulerEvent
            {
                Id = ev.id,
                Name = ev.text,
                StartDate = ev.start_date != null ? DateTime.Parse(ev.start_date,
                    System.Globalization.CultureInfo.InvariantCulture) : new DateTime(),
                EndDate = ev.end_date != null ? DateTime.Parse(ev.end_date,
                    System.Globalization.CultureInfo.InvariantCulture) : new DateTime(),
            };
        }
    }
}