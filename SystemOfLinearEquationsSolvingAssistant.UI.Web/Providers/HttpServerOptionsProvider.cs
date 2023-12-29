using Microsoft.Extensions.Options;
using SimpleHttpServer.Extensions.Options;

namespace SystemOfLinearEquationsSolvingAssistant.UI.Web.Providers;

internal sealed class HttpServerOptionsProvider : IOptions<HttpServerOptions>
{
    public HttpServerOptions Value { get; } = new();
}