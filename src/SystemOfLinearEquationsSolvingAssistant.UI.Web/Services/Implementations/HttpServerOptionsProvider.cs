using Microsoft.Extensions.Options;
using SimpleHttpServer.Extensions.Options;

namespace SystemOfLinearEquationsSolvingAssistant.UI.Web.Services.Implementations;

/// <summary>
/// Provides a configured instance of <see cref="HttpServerOptions"/>.
/// </summary>
internal sealed class HttpServerOptionsProvider : IOptions<HttpServerOptions>
{
    /// <summary>
    /// Gets the configured instance of <see cref="HttpServerOptions"/>.
    /// </summary>
    public HttpServerOptions Value { get; } = new();
}