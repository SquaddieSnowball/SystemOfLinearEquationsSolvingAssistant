using SystemOfLinearEquationsSolvingAssistant.BL.SystemOfLinearEquationsSolving.Entities;
using SystemOfLinearEquationsSolvingAssistant.ViewModels.Models.EventBusService.IntegrationEvents.Base;

namespace SystemOfLinearEquationsSolvingAssistant.ViewModels.Models.EventBusService.IntegrationEvents;

public sealed class SoleLoadedIntegrationEvent : IntegrationEvent
{
    public Sole Sole { get; }

    public SoleLoadedIntegrationEvent(Sole sole)
    {
        if (sole is null)
            throw new ArgumentNullException(nameof(sole), "System of linear equations must not be null.");

        Sole = sole;
    }
}