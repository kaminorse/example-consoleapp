using CommandLine;

namespace Example.ConsoleApp.Models
{
    [Verb("mail", HelpText = "Mail")]
    public class MailOptions
    {
        [Option('m', "method", Required = true, HelpText = "Method")]
        public string Method { get; set; }

        [Option('t', "to", Required = false, HelpText = "mail to address")]
        public string MailTo { get; set; }

        [Option('f', "from", Required = false, HelpText = "mail from address")]
        public string MailFrom { get; set; }

        [Option('s', "subject", Required = false, HelpText = "mail subject")]
        public string MailSubject { get; set; }

        [Option('b', "body", Required = false, HelpText = "mail body")]
        public string MailBody { get; set; }
    }
}
