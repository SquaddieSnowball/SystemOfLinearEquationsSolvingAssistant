using SystemOfLinearEquationsSolvingAssistant.UI.Web.Services.Entities.EventBus.IntegrationEvents;
using SystemOfLinearEquationsSolvingAssistant.UI.Web.Services.Entities.EventBus.IntegrationEvents.Entities;
using SystemOfLinearEquationsSolvingAssistant.ViewModels.Services.Abstractions;

namespace SystemOfLinearEquationsSolvingAssistant.UI.Web.Services.Implementations;

internal sealed class HtmlUserDialogService : IUserDialogService
{
    private readonly IEventBus _eventBusService;

    public HtmlUserDialogService(IEventBus eventBusService)
    {
        if (eventBusService is null)
            throw new ArgumentNullException(nameof(eventBusService), "Event bus service must not be null.");

        _eventBusService = eventBusService;
    }

    public void ShowInformationMessage(string message, string title = "Information")
    {
        UserDialogRequestedIntegrationEvent showInformationMessageIntegrationEvent = new(UserDialogType.InformationMessage);

        _ = showInformationMessageIntegrationEvent
            .AddData(nameof(message), message)
            .AddData(nameof(title), title);

        _eventBusService.Publish(showInformationMessageIntegrationEvent);
    }

    public void ShowWarningMessage(string message, string title = "Warning")
    {
        UserDialogRequestedIntegrationEvent showWarningMessageIntegrationEvent = new(UserDialogType.WarningMessage);

        _ = showWarningMessageIntegrationEvent
            .AddData(nameof(message), message)
            .AddData(nameof(title), title);

        _eventBusService.Publish(showWarningMessageIntegrationEvent);
    }

    public void ShowErrorMessage(string message, string title = "Error")
    {
        UserDialogRequestedIntegrationEvent showErrorMessageIntegrationEvent = new(UserDialogType.ErrorMessage);

        _ = showErrorMessageIntegrationEvent
            .AddData(nameof(message), message)
            .AddData(nameof(title), title);

        _eventBusService.Publish(showErrorMessageIntegrationEvent);
    }

    public string ShowOpenFileDialog(string filter = "All files (*.*)|*.*", string title = "Open file")
    {
        UserDialogRequestedIntegrationEvent showOpenFileDialogIntegrationEvent = new(UserDialogType.OpenFile);

        _ = showOpenFileDialogIntegrationEvent
            .AddData(nameof(filter), filter)
            .AddData(nameof(title), title);

        _eventBusService.Publish(showOpenFileDialogIntegrationEvent);

        return string.Empty;
    }
}