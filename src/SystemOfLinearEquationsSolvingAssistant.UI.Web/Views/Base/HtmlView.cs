using SimpleHttpServer.Entities;
using SimpleHttpServer.Services.Abstractions;
using SystemOfLinearEquationsSolvingAssistant.ViewModels.ViewModels.Base;

namespace SystemOfLinearEquationsSolvingAssistant.UI.Web.Views.Base;

/// <summary>
/// Serves as the base class for all HTML views.
/// </summary>
internal abstract class HtmlView
{
    /// <summary>
    /// Gets the path to the view visuals.
    /// </summary>
    public abstract string PagePath { get; }

    /// <summary>
    /// Gets the HTML representation of the view.
    /// </summary>
    public string? Page { get; set; }

    /// <summary>
    /// Gets or sets the view model.
    /// </summary>
    public ViewModel? ViewModel { get; set; }

    /// <summary>
    /// Gets or sets the HTTP server response generator.
    /// </summary>
    public IHttpServerResponseGenerator? HttpServerResponseGenerator { get; set; }

    /// <summary>
    /// Gets or sets a handler for the HTTP "GET" method.
    /// </summary>
    public Func<HttpRequest, HttpResponse>? GetHandler { get; set; }

    /// <summary>
    /// Gets or sets a handler for the HTTP "HEAD" method.
    /// </summary>
    public Func<HttpRequest, HttpResponse>? HeadHandler { get; set; }

    /// <summary>
    /// Gets or sets a handler for the HTTP "POST" method.
    /// </summary>
    public Func<HttpRequest, HttpResponse>? PostHandler { get; set; }

    /// <summary>
    /// Gets or sets a handler for the HTTP "PUT" method.
    /// </summary>
    public Func<HttpRequest, HttpResponse>? PutHandler { get; set; }

    /// <summary>
    /// Gets or sets a handler for the HTTP "PATCH" method.
    /// </summary>
    public Func<HttpRequest, HttpResponse>? PatchHandler { get; set; }

    /// <summary>
    /// Gets or sets a handler for the HTTP "DELETE" method.
    /// </summary>
    public Func<HttpRequest, HttpResponse>? DeleteHandler { get; set; }

    /// <summary>
    /// Gets or sets a handler for the HTTP "CONNECT" method.
    /// </summary>
    public Func<HttpRequest, HttpResponse>? ConnectHandler { get; set; }

    /// <summary>
    /// Gets or sets a handler for the HTTP "OPTIONS" method.
    /// </summary>
    public Func<HttpRequest, HttpResponse>? OptionsHandler { get; set; }

    /// <summary>
    /// Occurs when view requests relinking.
    /// </summary>
    public event EventHandler? RelinkRequest;

    /// <summary>
    /// Provides access to the <see cref="RelinkRequest"/> event from derived classes.
    /// </summary>
    /// <param name="e">An object that contains no event data.</param>
    protected virtual void OnRelinkRequest(EventArgs e) => RelinkRequest?.Invoke(this, e);
}