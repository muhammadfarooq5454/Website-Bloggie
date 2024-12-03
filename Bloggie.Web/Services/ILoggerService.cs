namespace Bloggie.Web.Services
{
    public interface ILoggerService
    {
        public Task LogInfoAsync(string message, Exception ex = null );
        public Task LogWarningAsync(string message, Exception ex = null);
        public Task LogErrorAsync(string message, Exception ex = null);
    }
}
