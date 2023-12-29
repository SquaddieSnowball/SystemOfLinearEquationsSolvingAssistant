namespace SystemOfLinearEquationsSolvingAssistant.UI.Web.Services.Exceptions.HtmlViewVisualsParser;

internal sealed class HtmlViewVisualsParsingException : Exception
{
    public HtmlViewVisualsParsingException() { }

    public HtmlViewVisualsParsingException(string? message) : base(message) { }

    public HtmlViewVisualsParsingException(string? message, Exception? innerException) : base(message, innerException) { }
}