using Microsoft.Extensions.Options;
using SimpleTcpServer.Extensions.Options;

namespace SystemOfLinearEquationsSolvingAssistant.UI.Web.Services.Implementations;

internal sealed class TcpServerOptionsProvider : IOptions<TcpServerOptions>
{
    public TcpServerOptions Value { get; } = new()
    {
        IpAddress = "127.0.0.1",
        Port = 8080
    };
}