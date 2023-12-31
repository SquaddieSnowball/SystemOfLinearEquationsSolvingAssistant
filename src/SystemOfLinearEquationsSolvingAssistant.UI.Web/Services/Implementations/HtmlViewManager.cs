using SystemOfLinearEquationsSolvingAssistant.ViewModels.Services.Abstractions;
using SystemOfLinearEquationsSolvingAssistant.UI.Web.Services.Entities.EventBus.IntegrationEvents.Entities;
using SystemOfLinearEquationsSolvingAssistant.UI.Web.Services.Entities.EventBus.IntegrationEvents;

namespace SystemOfLinearEquationsSolvingAssistant.UI.Web.Services.Implementations;

/// <summary>
/// Provides methods used to manage views.
/// </summary>
internal sealed class HtmlViewManager : IViewManager
{
    private readonly IEventBus _eventBus;

    /// <summary>
    /// Initializes a new instance of <see cref="HtmlViewManager"/> with the specified even bus.
    /// </summary>
    /// <param name="eventBus"><see cref="IEventBus"/> instance.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public HtmlViewManager(IEventBus eventBus)
    {
        if (eventBus is null)
            throw new ArgumentNullException(nameof(eventBus), "Event bus must not be null.");

        _eventBus = eventBus;
    }

    /// <summary>
    /// Shows the view.
    /// </summary>
    /// <param name="viewName">The name of the view to show.</param>
    /// <param name="ownerViewName">Owner of the view.</param>
    /// <param name="isDialogMode">Determines whether the view should be shown in dialog mode.</param>
    public void ShowView(string viewName, string? ownerViewName = null, bool isDialogMode = false)
    {
        ViewActionRequestedIntegrationEvent showViewIntegrationEvent = new(ViewActionType.Show);

        _ = showViewIntegrationEvent
            .AddData(nameof(viewName), viewName)
            .AddData(nameof(ownerViewName), ownerViewName)
            .AddData(nameof(isDialogMode), isDialogMode);

        _eventBus.Publish(showViewIntegrationEvent);
    }

    /// <summary>
    /// Closes the view.
    /// </summary>
    /// <param name="viewName">The name of the view to close.</param>
    public void CloseView(string viewName)
    {
        ViewActionRequestedIntegrationEvent closeViewIntegrationEvent = new(ViewActionType.Close);

        _ = closeViewIntegrationEvent
            .AddData(nameof(viewName), viewName);

        _eventBus.Publish(closeViewIntegrationEvent);
    }
}