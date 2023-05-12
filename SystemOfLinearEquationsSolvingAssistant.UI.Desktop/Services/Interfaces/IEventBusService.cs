using System;
using SystemOfLinearEquationsSolvingAssistant.UI.Desktop.Models.EventBus;

namespace SystemOfLinearEquationsSolvingAssistant.UI.Desktop.Services.Interfaces;

internal interface IEventBusService
{
    void Subscribe<T>(Action<T> integrationEventHandler) where T : IntegrationEvent;

    void Unsubscribe<T>(Action<T> integrationEventHandler) where T : IntegrationEvent;

    void Publish(IntegrationEvent integrationEvent);
}