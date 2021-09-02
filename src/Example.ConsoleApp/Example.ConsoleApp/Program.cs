using Example.ConsoleApp.Models;
using Example.ConsoleApp.Services;
using Example.ConsoleApp.Services.Implements;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace Example.ConsoleApp
{
    class Program
    {
        [STAThread]
        static async Task<int> Main(string[] args)
        {
            var basePath = Directory.GetParent(Assembly.GetExecutingAssembly().Location).FullName;

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .Build();
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            Log.Information("Start Main");

            var services = GetServiceDescriptors();

            var provider = services.BuildServiceProvider();

            var consoleAppService = provider.GetService<IConsoleAppService>();

            return await consoleAppService.RunAndReturnExitCode(args);
        }

        private static IServiceCollection GetServiceDescriptors()
        {
            var services = new ServiceCollection();

            services.AddTransient<IConsoleAppService, ConsoleAppService>();
            services.AddTransient<IMailService, MailService>();

            services.AddTransient<ServiceContext>(context => new ServiceContext()
            {
                MailSmtpHost = AppSetting.MailSmtpHost,
                MailSmtpPort = AppSetting.MailSmtpPort,
                MailSmtpUseSsl = AppSetting.MailSmtpUseSsl,
                MailSmtpUserName = AppSetting.MailSmtpUserName,
                MailSmtpPassword = AppSetting.MailSmtpPassword,
            });

            return services;
        }
    }
}
