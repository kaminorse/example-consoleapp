using Example.ConsoleApp.Models;
using MailKit.Net.Smtp;
using MimeKit;
using Serilog;
using System;
using System.Net.Security;
using System.Threading.Tasks;

namespace Example.ConsoleApp.Services.Implements
{
    public class MailService : IMailService
    {
        private readonly ServiceContext _serviceContext;
        public MailService(ServiceContext serviceContext)
        {
            _serviceContext = serviceContext;
        }

        /// <inheritdoc/>
        public async Task<bool> SendAsync(string from, string to, string subject, string body)
        {
            try
            {
                var message = new MimeMessage();

                message.From.Add(new MailboxAddress(from, from));
                message.To.Add(new MailboxAddress(to, to));
                message.Subject = subject;

                message.Body = new TextPart("plain")
                {
                    Text = body,
                };

                using (var client = new SmtpClient())
                {
                    if (_serviceContext.MailSmtpUseSsl.HasValue)
                    {
                        if (_serviceContext.MailSmtpUseSsl.Value)
                        {
                            client.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) =>
                            {
                                var host = (string)sender;
                                Log.Information($"hostname: {host}");
                                Log.Information($"X509Certificate.Subject: {certificate.Subject}");
                                Log.Information($"X509Certificate.Issuer: {certificate.Issuer}");
                                Log.Information($"SslPolicyErrors: {sslPolicyErrors}");

                                switch (sslPolicyErrors)
                                {
                                    case SslPolicyErrors.None:
                                        return true;
                                    case SslPolicyErrors.RemoteCertificateNotAvailable:
                                        return false;
                                    case SslPolicyErrors.RemoteCertificateNameMismatch:
                                        return true;
                                    case SslPolicyErrors.RemoteCertificateChainErrors:
                                        return false;
                                }

                                // どんなSSLでも通す
                                return true;
                            });

                            client.CheckCertificateRevocation = false;
                        }
                        client.Connect(_serviceContext.MailSmtpHost, _serviceContext.MailSmtpPort, _serviceContext.MailSmtpUseSsl.Value);
                    }
                    else
                    {
                        client.Connect(_serviceContext.MailSmtpHost, _serviceContext.MailSmtpPort);
                    }

                    // Note: only needed if the SMTP server requires authentication
                    if (!string.IsNullOrWhiteSpace(_serviceContext.MailSmtpUserName))
                    {
                        client.Authenticate(_serviceContext.MailSmtpUserName, _serviceContext.MailSmtpPassword);
                    }

                    await client.SendAsync(message);
                    client.Disconnect(true);
                }

                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error@MailService.SendAsync: ");
                return false;
            }
        }
    }
}
