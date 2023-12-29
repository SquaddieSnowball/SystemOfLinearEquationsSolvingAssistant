namespace SystemOfLinearEquationsSolvingAssistant.UI.Web.Extensions.Tools.Linking.Exceptions;

internal sealed class HtmlViewLinkingException : Exception
{
    public HtmlViewLinkingException() { }

    public HtmlViewLinkingException(string? message) : base(message) { }

    public HtmlViewLinkingException(string? message, Exception? innerException) : base(message, innerException) { }
}