using SystemOfLinearEquationsSolvingAssistant.BL.SystemOfLinearEquationsSolving.Entities;
using SystemOfLinearEquationsSolvingAssistant.ViewModels.Entities.IntegrationEvents.Base;

namespace SystemOfLinearEquationsSolvingAssistant.ViewModels.Entities.IntegrationEvents;

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