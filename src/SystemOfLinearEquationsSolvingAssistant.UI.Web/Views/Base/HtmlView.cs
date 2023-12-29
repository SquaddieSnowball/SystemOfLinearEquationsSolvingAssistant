using SimpleHttpServer.Entities;
using SimpleHttpServer.Services.Abstractions;
using SystemOfLinearEquationsSolvingAssistant.ViewModels.ViewModelsCollection.Base;

namespace SystemOfLinearEquationsSolvingAssistant.UI.Web.Views.Base;

internal abstract class HtmlView
{
    public abstract string PagePath { get; }

    public string? Page { get; set; }

    public ViewModel? ViewModel { get; set; }

    public IHttpServerResponseGenerator? HttpServerResponseGenerator { get; set; }

    public Func<HttpRequest, HttpResponse>? GetHandler { get; set; }

    public Func<HttpRequest, HttpResponse>? HeadHandler { get; set; }

    public Func<HttpRequest, HttpResponse>? PostHandler { get; set; }

    public Func<HttpRequest, HttpResponse>? PutHandler { get; set; }

    public Func<HttpRequest, HttpResponse>? PatchHandler { get; set; }

    public Func<HttpRequest, HttpResponse>? DeleteHandler { get; set; }

    public Func<HttpRequest, HttpResponse>? ConnectHandler { get; set; }

    public Func<HttpRequest, HttpResponse>? OptionsHandler { get; set; }

    public event EventHandler? RelinkRequest;

    protected virtual void OnRelinkRequest(EventArgs e) => RelinkRequest?.Invoke(this, e);
}