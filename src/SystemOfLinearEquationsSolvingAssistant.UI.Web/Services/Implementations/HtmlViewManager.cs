using SystemOfLinearEquationsSolvingAssistant.ViewModels.Services.Abstractions;
using SystemOfLinearEquationsSolvingAssistant.UI.Web.Services.Entities.EventBus.IntegrationEvents.Entities;
using SystemOfLinearEquationsSolvingAssistant.UI.Web.Services.Entities.EventBus.IntegrationEvents;

namespace SystemOfLinearEquationsSolvingAssistant.UI.Web.Services.Implementations;

internal sealed class HtmlViewManager : IViewManager
{
    private readonly IEventBus _eventBus;

    public HtmlViewManager(IEventBus eventBus)
    {
        if (eventBus is null)
            throw new ArgumentNullException(nameof(eventBus), "Event bus must not be null.");

        _eventBus = eventBus;
    }

    public void ShowView(string viewName, string? ownerViewName = null, bool isDialogMode = false)
    {
        ViewActionRequestedIntegrationEvent showViewIntegrationEvent = new(ViewActionType.Show);

        _ = showViewIntegrationEvent
            .AddData(nameof(viewName), viewName)
            .AddData(nameof(ownerViewName), ownerViewName)
            .AddData(nameof(isDialogMode), isDialogMode);

        _eventBus.Publish(showViewIntegrationEvent);
    }

    public void CloseView(string viewName)
    {
        ViewActionRequestedIntegrationEvent closeViewIntegrationEvent = new(ViewActionType.Close);

        _ = closeViewIntegrationEvent
            .AddData(nameof(viewName), viewName);

        _eventBus.Publish(closeViewIntegrationEvent);
    }
}