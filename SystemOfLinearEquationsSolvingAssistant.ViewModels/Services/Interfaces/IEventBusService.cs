using SystemOfLinearEquationsSolvingAssistant.ViewModels.Entities.IntegrationEvents.Base;

namespace SystemOfLinearEquationsSolvingAssistant.ViewModels.Services.Interfaces;

public interface IEventBusService
{
    void Subscribe<T>(Action<T> integrationEventHandler) where T : IntegrationEvent;

    void Unsubscribe<T>(Action<T> integrationEventHandler) where T : IntegrationEvent;

    void Publish(IntegrationEvent integrationEvent);
}