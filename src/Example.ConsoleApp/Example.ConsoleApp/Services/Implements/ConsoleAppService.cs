using CommandLine;
using Example.ConsoleApp.Constants;
using Example.ConsoleApp.Models;
using Serilog;
using System;
using System.Threading.Tasks;

namespace Example.ConsoleApp.Services.Implements
{
    public class ConsoleAppService : IConsoleAppService
    {
        private readonly IMailService _mailService;

        /// <summary>
        /// Constructs a new instance of <see cref="ConsoleAppService"/>.
        /// </summary>
        public ConsoleAppService(IMailService mailService)
        {
            _mailService = mailService;
        }

        /// <inheritdoc/>
        public async Task<int> RunAndReturnExitCode(string[] args)
        {
            return await Parser.Default.ParseArguments<MailOptions>(args)
                .MapResult(
                    async (MailOptions opts) => await RunMailAndReturnExitCode(opts),
                    errs => Task.FromResult(1));
        }

        /// <summary>
        /// Run Mail And Return ExitCode
        /// </summary>
        /// <param name="opts"></param>
        /// <returns></returns>
        private async Task<int> RunMailAndReturnExitCode(MailOptions opts)
        {
            switch (opts.Method)
            {
                case MailMethod.SEND_MAIL:
                    try
                    {
                        var result = await _mailService.SendAsync(opts.MailFrom, opts.MailTo, opts.MailSubject, opts.MailBody);
                        if (!result)
                        {
                            System.Console.WriteLine("send_mail failed.");
                            return 1;
                        }
                    }
                    catch (Exception ex)
                    {
                        var msg = ex.Message;
                        System.Console.WriteLine(msg);
                        Log.Error(ex, "ERROR!");
                    }
                    System.Console.WriteLine("send_mail is complete.");
                    return 0;
            }
            return 0;
        }
    }
}
