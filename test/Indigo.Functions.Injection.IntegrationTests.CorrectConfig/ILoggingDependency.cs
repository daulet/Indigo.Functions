using Microsoft.Extensions.Logging;

namespace Indigo.Functions.Injection.IntegrationTests.CorrectConfig
{
    public interface ILoggingDependency
    {
        void Log(string message);
    }

    public class LoggingDependencyImpl : ILoggingDependency
    {
        private readonly ILogger _logger;

        public LoggingDependencyImpl(ILogger logger)
        {
            _logger = logger;
        }

        public void Log(string message)
        {
            _logger.LogInformation(message);
        }
    }
}
