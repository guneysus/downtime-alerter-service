namespace DowntimeAlerterWeb.Models
{
    public class MonitoringModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Url { get; set; }

        public string Interval { get; set; }

        public bool IsValid => !(string.IsNullOrEmpty(Interval) || string.IsNullOrEmpty(Url));
    }
}
