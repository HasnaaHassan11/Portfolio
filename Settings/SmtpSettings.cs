namespace Portfolio.Settings
{
    public class SmtpSettings
    {
        public string FromEmail { get; set; }
        public string FromPassword { get; set; }
        public string ToEmail { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public bool EnableSsl { get; set; }
    }
}
