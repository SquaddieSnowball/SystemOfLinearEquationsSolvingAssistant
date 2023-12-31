using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using SimpleTcpServer;

namespace SystemOfLinearEquationsSolvingAssistant.UI.Web.Services.Implementations;

/// <summary>
/// Provides methods used to log <see cref="TcpServer"/> messages.
/// </summary>
internal sealed class TcpServerLoggerProvider : ILogger<TcpServer>
{
    private readonly ILogger logger = NullLogger<TcpServer>.Instance;

    /// <summary>
    /// Begins a logical operation scope.
    /// </summary>
    /// <typeparam name="TState">The type of the state to begin scope for.</typeparam>
    /// <param name="state">The identifier for the scope.</param>
    /// <returns>An <see cref="IDisposable"/> that ends the logical operation scope on dispose.</returns>
    public IDisposable? BeginScope<TState>(TState state) where TState : notnull => logger.BeginScope(state);

    /// <summary>
    /// Checks if the given <paramref name="logLevel"/> is enabled.
    /// </summary>
    /// <param name="logLevel">Level to be checked.</param>
    /// <returns><see langword="true"/> if enabled; otherwise, <see langword="false"/></returns>
    public bool IsEnabled(LogLevel logLevel) => logger.IsEnabled(logLevel);

    /// <summary>
    /// Writes a log entry.
    /// </summary>
    /// <typeparam name="TState">The type of the object to be written.</typeparam>
    /// <param name="logLevel">Entry will be written on this level.</param>
    /// <param name="eventId">Id of the event.</param>
    /// <param name="state">The entry to be written.</param>
    /// <param name="exception">The exception related to this entry.</param>
    /// <param name="formatter">Function to create a <see cref="string"/> message 
    /// of the <paramref name="state"/> and <paramref name="exception"/>.</param>
    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state,
        Exception? exception, Func<TState, Exception?, string> formatter) =>
            logger.Log(logLevel, eventId, state, exception, formatter);
}