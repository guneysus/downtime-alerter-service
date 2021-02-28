namespace DowntimeAlerterWeb.Services
{
    public class AuthMessageSenderOptions
    {
        public string SendGridUser { get; set; }
        public string SendGridKey { get; set; }
        public string NoreplyEmail { get; set; }
    }
}
