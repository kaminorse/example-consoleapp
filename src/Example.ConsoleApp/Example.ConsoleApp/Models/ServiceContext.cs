namespace Example.ConsoleApp.Models
{
    public class ServiceContext
    {
        /// <summary>
        /// MailSmtpHost
        /// </summary>
        public string MailSmtpHost { get; set; }

        /// <summary>
        /// MailSmtpPort
        /// </summary>
        public int MailSmtpPort { get; set; }

        /// <summary>
        /// MailSmtpUseSsl
        /// </summary>
        public bool? MailSmtpUseSsl { get; set; }

        /// <summary>
        /// MailSmtpUserName
        /// </summary>
        public string MailSmtpUserName { get; set; }

        /// <summary>
        /// MailSmtpPassword
        /// </summary>
        public string MailSmtpPassword { get; set; }
    }
}
