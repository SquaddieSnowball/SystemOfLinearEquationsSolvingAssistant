namespace SystemOfLinearEquationsSolvingAssistant.UI.Web.Services.Interfaces;

internal interface IHttpServerManagerService
{
    void SetDefaultView<TView>();

    void StartServer();

    void StopServer();
}