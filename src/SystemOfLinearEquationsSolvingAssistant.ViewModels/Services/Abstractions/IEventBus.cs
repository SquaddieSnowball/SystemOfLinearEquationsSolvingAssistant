using SystemOfLinearEquationsSolvingAssistant.ViewModels.Services.Entities.EventBus.IntegrationEvents.Base;

namespace SystemOfLinearEquationsSolvingAssistant.ViewModels.Services.Abstractions;

/// <summary>
/// Represents a bus used for event-based communication.
/// </summary>
public interface IEventBus
{
    /// <summary>
    /// Subscribes to the event.
    /// </summary>
    /// <typeparam name="T">Event type.</typeparam>
    /// <param name="integrationEventHandler">Event handler.</param>
    void Subscribe<T>(Action<T> integrationEventHandler) where T : IntegrationEvent;

    /// <summary>
    /// Unsubscribes from the event.
    /// </summary>
    /// <typeparam name="T">Event type.</typeparam>
    /// <param name="integrationEventHandler">Event handler.</param>
    void Unsubscribe<T>(Action<T> integrationEventHandler) where T : IntegrationEvent;

    /// <summary>
    /// Publishes the event.
    /// </summary>
    /// <param name="integrationEvent">Event to publish.</param>
    void Publish(IntegrationEvent integrationEvent);
}