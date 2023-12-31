namespace SystemOfLinearEquationsSolvingAssistant.ViewModels.Services.Entities.EventBus.IntegrationEvents.Base;

/// <summary>
/// Represents an integration event.
/// </summary>
public abstract class IntegrationEvent
{
    /// <summary>
    /// Event ID.
    /// </summary>
    public int? Id { get; init; }
}