﻿using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using SimpleTcpServer;

namespace SystemOfLinearEquationsSolvingAssistant.UI.Web.Providers;

internal sealed class TcpServerLoggerProvider : ILogger<TcpServer>
{
    private readonly ILogger logger = NullLogger<TcpServer>.Instance;

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull => logger.BeginScope(state);

    public bool IsEnabled(LogLevel logLevel) => logger.IsEnabled(logLevel);

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state,
        Exception? exception, Func<TState, Exception?, string> formatter) =>
            logger.Log(logLevel, eventId, state, exception, formatter);
}