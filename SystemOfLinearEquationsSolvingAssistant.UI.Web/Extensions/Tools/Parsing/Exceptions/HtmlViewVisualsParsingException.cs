namespace SystemOfLinearEquationsSolvingAssistant.UI.Web.Extensions.Tools.Parsing.Exceptions;

internal sealed class HtmlViewVisualsParsingException : Exception
{
    public HtmlViewVisualsParsingException() { }

    public HtmlViewVisualsParsingException(string? message) : base(message) { }

    public HtmlViewVisualsParsingException(string? message, Exception? innerException) : base(message, innerException) { }
}