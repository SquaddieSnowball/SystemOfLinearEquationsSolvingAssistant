using SystemOfLinearEquationsSolvingAssistant.ViewModels.Services.Abstractions;
using SystemOfLinearEquationsSolvingAssistant.ViewModels.Services.Entities.EventBus.IntegrationEvents.Base;

namespace SystemOfLinearEquationsSolvingAssistant.ViewModels.Services.Implementations;

/// <summary>
/// Represents a bus used for event-based communication.
/// </summary>
public sealed class EventBus : IEventBus
{
    private readonly Dictionary<Type, Delegate?> _subscribtions = new();
    private readonly object _lock = new();

    /// <summary>
    /// Subscribes to the event.
    /// </summary>
    /// <typeparam name="T">Event type.</typeparam>
    /// <param name="integrationEventHandler">Event handler.</param>
    public void Subscribe<T>(Action<T> integrationEventHandler) where T : IntegrationEvent
    {
        Type integrationEventType = typeof(T);

        lock (_lock)
        {
            if (_subscribtions.TryGetValue(integrationEventType, out Delegate? currentIntegrationEventHandlers) is false)
                _subscribtions.Add(integrationEventType, integrationEventHandler);
            else
                _subscribtions[integrationEventType] = Delegate.Combine(currentIntegrationEventHandlers, integrationEventHandler);
        }
    }

    /// <summary>
    /// Unsubscribes from the event.
    /// </summary>
    /// <typeparam name="T">Event type.</typeparam>
    /// <param name="integrationEventHandler">Event handler.</param>
    public void Unsubscribe<T>(Action<T> integrationEventHandler) where T : IntegrationEvent
    {
        Type integrationEventType = typeof(T);

        lock (_lock)
        {
            if (_subscribtions.TryGetValue(integrationEventType, out Delegate? currentIntegrationEventHandlers) is false)
                return;
            else
                _subscribtions[integrationEventType] = Delegate.Remove(currentIntegrationEventHandlers, integrationEventHandler);
        }
    }

    /// <summary>
    /// Publishes the event.
    /// </summary>
    /// <param name="integrationEvent">Event to publish.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public void Publish(IntegrationEvent integrationEvent)
    {
        if (integrationEvent is null)
            throw new ArgumentNullException(nameof(integrationEvent), "Integration event must not be null.");

        lock (_lock)
        {
            if (_subscribtions.TryGetValue(integrationEvent.GetType(), out Delegate? currentIntegrationEventHandlers) is false)
                return;

            _ = currentIntegrationEventHandlers?.DynamicInvoke(integrationEvent);
        }
    }
}