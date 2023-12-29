using SystemOfLinearEquationsSolvingAssistant.ViewModels.Services.Entities.EventBus.IntegrationEvents.Base;

namespace SystemOfLinearEquationsSolvingAssistant.ViewModels.Services.Abstractions;

public interface IEventBus
{
    void Subscribe<T>(Action<T> integrationEventHandler) where T : IntegrationEvent;

    void Unsubscribe<T>(Action<T> integrationEventHandler) where T : IntegrationEvent;

    void Publish(IntegrationEvent integrationEvent);
}