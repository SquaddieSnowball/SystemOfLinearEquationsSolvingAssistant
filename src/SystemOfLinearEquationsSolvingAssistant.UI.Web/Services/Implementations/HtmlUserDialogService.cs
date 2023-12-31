using SystemOfLinearEquationsSolvingAssistant.UI.Web.Services.Entities.EventBus.IntegrationEvents;
using SystemOfLinearEquationsSolvingAssistant.UI.Web.Services.Entities.EventBus.IntegrationEvents.Entities;
using SystemOfLinearEquationsSolvingAssistant.ViewModels.Services.Abstractions;

namespace SystemOfLinearEquationsSolvingAssistant.UI.Web.Services.Implementations;

/// <summary>
/// Provides methods used to interact with the user.
/// </summary>
internal sealed class HtmlUserDialogService : IUserDialogService
{
    private readonly IEventBus _eventBus;

    /// <summary>
    /// Initializes a new instance of <see cref="HtmlUserDialogService"/> with the specified event bus.
    /// </summary>
    /// <param name="eventBus"><see cref="IEventBus"/> instance.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public HtmlUserDialogService(IEventBus eventBus)
    {
        if (eventBus is null)
            throw new ArgumentNullException(nameof(eventBus), "Event bus must not be null.");

        _eventBus = eventBus;
    }

    /// <summary>
    /// Shows an information message.
    /// </summary>
    /// <param name="message">Message to show.</param>
    /// <param name="title">Title of the message.</param>
    public void ShowInformationMessage(string message, string title = "Information")
    {
        UserDialogRequestedIntegrationEvent showInformationMessageIntegrationEvent = new(UserDialogType.InformationMessage);

        _ = showInformationMessageIntegrationEvent
            .AddData(nameof(message), message)
            .AddData(nameof(title), title);

        _eventBus.Publish(showInformationMessageIntegrationEvent);
    }

    /// <summary>
    /// Shows a warning message.
    /// </summary>
    /// <param name="message">Message to show.</param>
    /// <param name="title">Title of the message.</param>
    public void ShowWarningMessage(string message, string title = "Warning")
    {
        UserDialogRequestedIntegrationEvent showWarningMessageIntegrationEvent = new(UserDialogType.WarningMessage);

        _ = showWarningMessageIntegrationEvent
            .AddData(nameof(message), message)
            .AddData(nameof(title), title);

        _eventBus.Publish(showWarningMessageIntegrationEvent);
    }

    /// <summary>
    /// Shows an error message.
    /// </summary>
    /// <param name="message">Message to show.</param>
    /// <param name="title">Title of the message.</param>
    public void ShowErrorMessage(string message, string title = "Error")
    {
        UserDialogRequestedIntegrationEvent showErrorMessageIntegrationEvent = new(UserDialogType.ErrorMessage);

        _ = showErrorMessageIntegrationEvent
            .AddData(nameof(message), message)
            .AddData(nameof(title), title);

        _eventBus.Publish(showErrorMessageIntegrationEvent);
    }

    /// <summary>
    /// Shows a common dialog that allows the user to specify the name of the file to open.
    /// </summary>
    /// <param name="filter">A filter string that determines what types of files are displayed.</param>
    /// <param name="title">Title of the dialog.</param>
    /// <returns>The full path of the file selected in a dialog.</returns>
    public string ShowOpenFileDialog(string filter = "All files (*.*)|*.*", string title = "Open file")
    {
        UserDialogRequestedIntegrationEvent showOpenFileDialogIntegrationEvent = new(UserDialogType.OpenFile);

        _ = showOpenFileDialogIntegrationEvent
            .AddData(nameof(filter), filter)
            .AddData(nameof(title), title);

        _eventBus.Publish(showOpenFileDialogIntegrationEvent);

        return string.Empty;
    }
}