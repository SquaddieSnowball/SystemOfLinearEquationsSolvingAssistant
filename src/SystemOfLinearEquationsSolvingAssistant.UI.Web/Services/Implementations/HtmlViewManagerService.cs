using SystemOfLinearEquationsSolvingAssistant.UI.Web.Entities.IntegrationEvents.Entities;
using SystemOfLinearEquationsSolvingAssistant.UI.Web.Entities.IntegrationEvents;
using SystemOfLinearEquationsSolvingAssistant.ViewModels.Services.Interfaces;

namespace SystemOfLinearEquationsSolvingAssistant.UI.Web.Services.Implementations;

internal sealed class HtmlViewManagerService : IViewManagerService
{
    private readonly IEventBusService _eventBusService;

    public HtmlViewManagerService(IEventBusService eventBusService)
    {
        if (eventBusService is null)
            throw new ArgumentNullException(nameof(eventBusService), "Event bus service must not be null.");

        _eventBusService = eventBusService;
    }

    public void ShowView(string viewName, string? ownerViewName = null, bool isDialogMode = false)
    {
        ViewActionRequestedIntegrationEvent showViewIntegrationEvent = new(ViewActionType.Show);

        _ = showViewIntegrationEvent
            .AddData(nameof(viewName), viewName)
            .AddData(nameof(ownerViewName), ownerViewName)
            .AddData(nameof(isDialogMode), isDialogMode);

        _eventBusService.Publish(showViewIntegrationEvent);
    }

    public void CloseView(string viewName)
    {
        ViewActionRequestedIntegrationEvent closeViewIntegrationEvent = new(ViewActionType.Close);

        _ = closeViewIntegrationEvent
            .AddData(nameof(viewName), viewName);

        _eventBusService.Publish(closeViewIntegrationEvent);
    }
}