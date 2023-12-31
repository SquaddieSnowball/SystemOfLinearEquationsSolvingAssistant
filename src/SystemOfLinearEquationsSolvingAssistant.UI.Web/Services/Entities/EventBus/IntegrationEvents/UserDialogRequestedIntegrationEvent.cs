using SystemOfLinearEquationsSolvingAssistant.UI.Web.Services.Entities.EventBus.IntegrationEvents.Entities;
using SystemOfLinearEquationsSolvingAssistant.ViewModels.Services.Entities.EventBus.IntegrationEvents.Base;

namespace SystemOfLinearEquationsSolvingAssistant.UI.Web.Services.Entities.EventBus.IntegrationEvents;

/// <summary>
/// Represents an integration event used to publish data when a user dialog is requested.
/// </summary>
internal sealed class UserDialogRequestedIntegrationEvent : CommonDataIntegrationEvent
{
    /// <summary>
    /// Gets the user dialog type.
    /// </summary>
    public UserDialogType UserDialogType { get; }

    /// <summary>
    /// Initializes a new instance of <see cref="UserDialogRequestedIntegrationEvent"/> with the specified user dialog type.
    /// </summary>
    /// <param name="userDialogType">User dialog type.</param>
    /// <exception cref="ArgumentException"></exception>
    public UserDialogRequestedIntegrationEvent(UserDialogType userDialogType)
    {
        if (Enum.IsDefined(typeof(UserDialogType), userDialogType) is false)
            throw new ArgumentException("The specified user dialog type is not defined.", nameof(userDialogType));

        UserDialogType = userDialogType;
    }
}