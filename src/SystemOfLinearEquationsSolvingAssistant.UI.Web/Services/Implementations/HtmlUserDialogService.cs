using SystemOfLinearEquationsSolvingAssistant.UI.Web.Services.Entities.EventBus.IntegrationEvents;
using SystemOfLinearEquationsSolvingAssistant.UI.Web.Services.Entities.EventBus.IntegrationEvents.Entities;
using SystemOfLinearEquationsSolvingAssistant.ViewModels.Services.Abstractions;

namespace SystemOfLinearEquationsSolvingAssistant.UI.Web.Services.Implementations;

internal sealed class HtmlUserDialogService : IUserDialogService
{
    private readonly IEventBus _eventBus;

    public HtmlUserDialogService(IEventBus eventBus)
    {
        if (eventBus is null)
            throw new ArgumentNullException(nameof(eventBus), "Event bus must not be null.");

        _eventBus = eventBus;
    }

    public void ShowInformationMessage(string message, string title = "Information")
    {
        UserDialogRequestedIntegrationEvent showInformationMessageIntegrationEvent = new(UserDialogType.InformationMessage);

        _ = showInformationMessageIntegrationEvent
            .AddData(nameof(message), message)
            .AddData(nameof(title), title);

        _eventBus.Publish(showInformationMessageIntegrationEvent);
    }

    public void ShowWarningMessage(string message, string title = "Warning")
    {
        UserDialogRequestedIntegrationEvent showWarningMessageIntegrationEvent = new(UserDialogType.WarningMessage);

        _ = showWarningMessageIntegrationEvent
            .AddData(nameof(message), message)
            .AddData(nameof(title), title);

        _eventBus.Publish(showWarningMessageIntegrationEvent);
    }

    public void ShowErrorMessage(string message, string title = "Error")
    {
        UserDialogRequestedIntegrationEvent showErrorMessageIntegrationEvent = new(UserDialogType.ErrorMessage);

        _ = showErrorMessageIntegrationEvent
            .AddData(nameof(message), message)
            .AddData(nameof(title), title);

        _eventBus.Publish(showErrorMessageIntegrationEvent);
    }

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