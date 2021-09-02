using System.Threading.Tasks;

namespace Example.ConsoleApp.Services
{
    public interface IConsoleAppService
    {
        /// <summary>
        /// Run And Return ExitCode
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        Task<int> RunAndReturnExitCode(string[] args);
    }
}
