using SystemOfLinearEquationsSolvingAssistant.UI.Web.Services.Entities.EventBus.IntegrationEvents.Entities;
using SystemOfLinearEquationsSolvingAssistant.ViewModels.Services.Entities.EventBus.IntegrationEvents.Base;

namespace SystemOfLinearEquationsSolvingAssistant.UI.Web.Services.Entities.EventBus.IntegrationEvents;

/// <summary>
/// Represents an integration event used to publish data when a view action is requested.
/// </summary>
internal sealed class ViewActionRequestedIntegrationEvent : CommonDataIntegrationEvent
{
    /// <summary>
    /// Gets the view action type.
    /// </summary>
    public ViewActionType ViewActionType { get; }

    /// <summary>
    /// Initializes a new instance of <see cref="ViewActionRequestedIntegrationEvent"/> with the specified view action type.
    /// </summary>
    /// <param name="viewActionType">View action type.</param>
    /// <exception cref="ArgumentException"></exception>
    public ViewActionRequestedIntegrationEvent(ViewActionType viewActionType)
    {
        if (Enum.IsDefined(typeof(ViewActionType), viewActionType) is false)
            throw new ArgumentException("The specified view action type is not defined.", nameof(viewActionType));

        ViewActionType = viewActionType;
    }
}