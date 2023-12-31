namespace SystemOfLinearEquationsSolvingAssistant.UI.Web.Services.Entities.EventBus.IntegrationEvents.Entities;

/// <summary>
/// Represents a user dialog type.
/// </summary>
internal enum UserDialogType
{
    /// <summary>
    /// Represents the "Information Message" user dialog type.
    /// </summary>
    InformationMessage,

    /// <summary>
    /// Represents the "Warning Message" user dialog type.
    /// </summary>
    WarningMessage,

    /// <summary>
    /// Represents the "Error Message" user dialog type.
    /// </summary>
    ErrorMessage,

    /// <summary>
    /// Represents the "Open File" user dialog type.
    /// </summary>
    OpenFile
}