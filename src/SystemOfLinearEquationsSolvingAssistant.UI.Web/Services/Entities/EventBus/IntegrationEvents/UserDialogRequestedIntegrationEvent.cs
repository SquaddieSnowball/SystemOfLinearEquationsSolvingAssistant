using SystemOfLinearEquationsSolvingAssistant.UI.Web.Services.Entities.EventBus.IntegrationEvents.Entities;
using SystemOfLinearEquationsSolvingAssistant.ViewModels.Services.Entities.EventBus.IntegrationEvents.Base;

namespace SystemOfLinearEquationsSolvingAssistant.UI.Web.Services.Entities.EventBus.IntegrationEvents;

internal sealed class UserDialogRequestedIntegrationEvent : CommonDataIntegrationEvent
{
    public UserDialogType UserDialogType { get; }

    public UserDialogRequestedIntegrationEvent(UserDialogType userDialogType)
    {
        if (Enum.IsDefined(typeof(UserDialogType), userDialogType) is false)
            throw new ArgumentException("The specified user dialog type is not defined.", nameof(userDialogType));

        UserDialogType = userDialogType;
    }
}