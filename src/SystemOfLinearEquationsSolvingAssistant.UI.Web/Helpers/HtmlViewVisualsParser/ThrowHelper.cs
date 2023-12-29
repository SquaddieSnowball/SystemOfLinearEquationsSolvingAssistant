using SystemOfLinearEquationsSolvingAssistant.UI.Web.Services.Exceptions.HtmlViewVisualsParser;

namespace SystemOfLinearEquationsSolvingAssistant.UI.Web.Helpers.HtmlViewVisualsParser;

internal static class ThrowHelper
{
    public static void HtmlViewVisualsParsingException(string message, string? name = default, int? index = default)
    {
        message ??= string.Empty;

        if (name is not null)
            message += Environment.NewLine + "Name: " + name;

        if (index is not null)
            message += Environment.NewLine + "Index: " + index;

        throw new HtmlViewVisualsParsingException(message);
    }
}