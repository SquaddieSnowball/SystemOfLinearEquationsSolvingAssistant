namespace SystemOfLinearEquationsSolvingAssistant.UI.Web.Services.Abstractions;

internal interface IHttpServerManager
{
    void SetDefaultView<TView>();

    void StartServer();

    void StopServer();
}