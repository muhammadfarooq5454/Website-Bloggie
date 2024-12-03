using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Serilog;

namespace Bloggie.Web.Services
{
    public class LoggerService : ILoggerService
    {
        public async Task LogErrorAsync(string message, Exception ex = null)
        {
            Log.Error(ex, message); 
            await Task.CompletedTask;
        }

        public async Task LogInfoAsync(string message, Exception ex = null)
        {
            Log.Information(message);
            await Task.CompletedTask;
        }

        public async Task LogWarningAsync(string message, Exception ex = null)
        {
            Log.Warning(message);
            await Task.CompletedTask;
        }
    }
}
