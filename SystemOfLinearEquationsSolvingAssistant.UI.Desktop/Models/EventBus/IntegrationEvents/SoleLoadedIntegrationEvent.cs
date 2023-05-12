using System;
using SystemOfLinearEquationsSolvingAssistant.BL.SystemOfLinearEquationsSolving.Entities;

namespace SystemOfLinearEquationsSolvingAssistant.UI.Desktop.Models.EventBus.IntegrationEvents;

internal sealed class SoleLoadedIntegrationEvent : IntegrationEvent
{
    public Sole Sole { get; }

    public SoleLoadedIntegrationEvent(Sole sole)
    {
        if (sole is null)
            throw new ArgumentNullException(nameof(sole), "System of linear equations must not be null.");

        Sole = sole;
    }
}