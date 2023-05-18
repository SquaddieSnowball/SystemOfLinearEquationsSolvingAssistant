using SystemOfLinearEquationsSolvingAssistant.ViewModels.Entities.IntegrationEvents.Base;
using SystemOfLinearEquationsSolvingAssistant.ViewModels.Services.Interfaces;

namespace SystemOfLinearEquationsSolvingAssistant.ViewModels.Services.Implementations;

public sealed class EventBusService : IEventBusService
{
    private readonly Dictionary<Type, Delegate?> _subscribtions = new();
    private readonly object _lock = new();

    public void Subscribe<T>(Action<T> integrationEventHandler) where T : IntegrationEvent
    {
        Type integrationEventType = typeof(T);

        lock (_lock)
        {
            if (_subscribtions.TryGetValue(integrationEventType, out Delegate? currentIntegrationEventHandlers) is false)
                _subscribtions.Add(integrationEventType, integrationEventHandler);
            else
                _subscribtions[integrationEventType] =
                    Delegate.Combine(currentIntegrationEventHandlers, integrationEventHandler);
        }
    }

    public void Unsubscribe<T>(Action<T> integrationEventHandler) where T : IntegrationEvent
    {
        Type integrationEventType = typeof(T);

        lock (_lock)
        {
            if (_subscribtions.TryGetValue(integrationEventType, out Delegate? currentIntegrationEventHandlers) is false)
                return;
            else
                _subscribtions[integrationEventType] =
                    Delegate.Remove(currentIntegrationEventHandlers, integrationEventHandler);
        }
    }

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