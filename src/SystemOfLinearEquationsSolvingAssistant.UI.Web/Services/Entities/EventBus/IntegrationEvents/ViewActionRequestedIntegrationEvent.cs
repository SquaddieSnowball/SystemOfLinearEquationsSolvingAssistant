using SystemOfLinearEquationsSolvingAssistant.UI.Web.Services.Entities.EventBus.IntegrationEvents.Entities;
using SystemOfLinearEquationsSolvingAssistant.ViewModels.Services.Entities.EventBus.IntegrationEvents.Base;

namespace SystemOfLinearEquationsSolvingAssistant.UI.Web.Services.Entities.EventBus.IntegrationEvents;

internal sealed class ViewActionRequestedIntegrationEvent : CommonDataIntegrationEvent
{
    public ViewActionType ViewActionType { get; }

    public ViewActionRequestedIntegrationEvent(ViewActionType viewActionType)
    {
        if (Enum.IsDefined(typeof(ViewActionType), viewActionType) is false)
            throw new ArgumentException("The specified view action type is not defined.", nameof(viewActionType));

        ViewActionType = viewActionType;
    }
}