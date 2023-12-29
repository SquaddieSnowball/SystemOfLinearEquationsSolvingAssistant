using SystemOfLinearEquationsSolvingAssistant.UI.Web.Extensions.Tools.Parsing.Exceptions;

namespace SystemOfLinearEquationsSolvingAssistant.UI.Web.Extensions.Tools.Parsing.Helpers;

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