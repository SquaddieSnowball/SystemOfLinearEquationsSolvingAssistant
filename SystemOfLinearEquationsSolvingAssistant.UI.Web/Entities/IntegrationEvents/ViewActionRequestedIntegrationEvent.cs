using SystemOfLinearEquationsSolvingAssistant.UI.Web.Entities.IntegrationEvents.Entities;
using SystemOfLinearEquationsSolvingAssistant.ViewModels.Entities.IntegrationEvents.Base;

namespace SystemOfLinearEquationsSolvingAssistant.UI.Web.Entities.IntegrationEvents;

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