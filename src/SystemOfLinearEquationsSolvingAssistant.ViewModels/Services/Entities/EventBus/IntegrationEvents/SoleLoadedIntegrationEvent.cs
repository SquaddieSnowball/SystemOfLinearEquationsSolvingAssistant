using SystemOfLinearEquationsSolvingAssistant.BL.Entities;
using SystemOfLinearEquationsSolvingAssistant.ViewModels.Services.Entities.EventBus.IntegrationEvents.Base;

namespace SystemOfLinearEquationsSolvingAssistant.ViewModels.Services.Entities.EventBus.IntegrationEvents;

/// <summary>
/// Represents an integration event used to publish data when a system of linear equations loads.
/// </summary>
public sealed class SoleLoadedIntegrationEvent : IntegrationEvent
{
    /// <summary>
    /// Gets the loaded system of linear equations.
    /// </summary>
    public Sole Sole { get; }

    /// <summary>
    /// Initializes a new instance of <see cref="SoleLoadedIntegrationEvent"/> with the specified system of linear equations.
    /// </summary>
    /// <param name="sole">Loaded system of linear equations.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public SoleLoadedIntegrationEvent(Sole sole)
    {
        if (sole is null)
            throw new ArgumentNullException(nameof(sole), "System of linear equations must not be null.");

        Sole = sole;
    }
}