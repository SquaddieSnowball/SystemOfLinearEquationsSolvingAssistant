using Microsoft.Extensions.Options;
using SimpleTcpServer.Extensions.Options;

namespace SystemOfLinearEquationsSolvingAssistant.UI.Web.Services.Implementations;

/// <summary>
/// Provides a configured instance of <see cref="TcpServerOptions"/>.
/// </summary>
internal sealed class TcpServerOptionsProvider : IOptions<TcpServerOptions>
{
    /// <summary>
    /// Gets the configured instance of <see cref="TcpServerOptions"/>.
    /// </summary>
    public TcpServerOptions Value { get; } = new()
    {
        IpAddress = "127.0.0.1",
        Port = 8080
    };
}