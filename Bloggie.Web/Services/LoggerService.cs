using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Serilog;

namespace Bloggie.Web.Services
{
    public class LoggerService
    {
        public async Task LogErrorAsync(string message, Exception ex = null)
        {
            Log.Error(ex, message); // Include exception if provided
            await Task.CompletedTask;
        }

        public async Task LogInfoAsync(string message)
        {
            Log.Information(message);
            await Task.CompletedTask;
        }

        public async Task LogWarningAsync(string message)
        {
            Log.Warning(message);
            await Task.CompletedTask;
        }
    }
}
