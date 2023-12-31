using SystemOfLinearEquationsSolvingAssistant.UI.Web.Services.Exceptions.HtmlViewVisualsParser;

namespace SystemOfLinearEquationsSolvingAssistant.UI.Web.Helpers.HtmlViewVisualsParser;

/// <summary>
/// Provides methods used as helpers for throwing exceptions.
/// </summary>
internal static class ThrowHelper
{
    /// <summary>
    /// Throws an exception that occurs while parsing the visual elements of an HTML view.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="name">The name of the element that caused the exception.</param>
    /// <param name="index">The index in the element representation that caused the exception.</param>
    public static void HtmlViewVisualsParsingException(string message, string? name = default, int? index = default)
    {
        message ??= string.Empty;

        if (name is not null)
            message += $"{Environment.NewLine}Name: {name}";

        if (index is not null)
            message += $"{Environment.NewLine}Index: {index}";

        throw new HtmlViewVisualsParsingException(message);
    }
}