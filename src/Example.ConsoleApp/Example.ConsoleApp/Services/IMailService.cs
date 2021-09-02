using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Example.ConsoleApp.Services
{
    public interface IMailService
    {
        Task<bool> SendAsync(string from, string to, string subject, string body);
    }
}
