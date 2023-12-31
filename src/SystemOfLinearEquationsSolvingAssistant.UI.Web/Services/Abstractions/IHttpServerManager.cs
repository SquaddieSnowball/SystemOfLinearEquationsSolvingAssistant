namespace SystemOfLinearEquationsSolvingAssistant.UI.Web.Services.Abstractions;

/// <summary>
/// Provides methods used to manage the HTTP server.
/// </summary>
internal interface IHttpServerManager
{
    /// <summary>
    /// Sets the default view for the server.
    /// </summary>
    /// <typeparam name="TView">View type.</typeparam>
    void SetDefaultView<TView>();

    /// <summary>
    /// Starts the server.
    /// </summary>
    void StartServer();

    /// <summary>
    /// Stops the server.
    /// </summary>
    void StopServer();
}