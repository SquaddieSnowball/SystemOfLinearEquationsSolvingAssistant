using Microsoft.Extensions.Logging;
using SimpleHttpServer;

namespace SystemOfLinearEquationsSolvingAssistant.UI.Web.Providers;

internal sealed class HttpServerLoggerProvider : ILogger<HttpServer>
{
    private readonly ILogger logger = LoggerFactory.Create(b => b.AddConsole().SetMinimumLevel(LogLevel.Debug)).CreateLogger<HttpServer>();

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull => logger.BeginScope(state);

    public bool IsEnabled(LogLevel logLevel) => logger.IsEnabled(logLevel);

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state,
        Exception? exception, Func<TState, Exception?, string> formatter) =>
            logger.Log(logLevel, eventId, state, exception, formatter);
}