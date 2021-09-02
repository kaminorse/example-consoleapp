using Microsoft.Extensions.Configuration;
using System.IO;

namespace Example.ConsoleApp
{
    public class AppSetting
    {
        private static readonly IConfigurationRoot configuration = 
            new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            //.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: true)
            .Build();

        /// <summary>
        /// MailSmtpHost
        /// </summary>
        public static readonly string MailSmtpHost = configuration["Mail:SmtpHost"];

        /// <summary>
        /// MailSmtpPort
        /// </summary>
        public static readonly int MailSmtpPort = int.Parse(configuration["Mail:SmtpPort"]);

        /// <summary>
        /// MailSmtpUseSsl
        /// </summary>
        public static readonly bool? MailSmtpUseSsl =
            !string.IsNullOrWhiteSpace(configuration["Mail:SmtpUseSsl"]) ?
            bool.Parse(configuration["Mail:SmtpUseSsl"]) :
            (bool?)null;

        /// <summary>
        /// MailSmtpUserName
        /// </summary>
        public static readonly string MailSmtpUserName = configuration["Mail:SmtpUserName"];

        /// <summary>
        /// MailSmtpPassword
        /// </summary>
        public static readonly string MailSmtpPassword = configuration["Mail:SmtpPassword"];

    }
}
