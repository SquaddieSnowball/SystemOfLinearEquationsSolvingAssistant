namespace SystemOfLinearEquationsSolvingAssistant.UI.Web.Extensions.Controls.Exceptions;

internal sealed class HtmlElementValidationException : Exception
{
    public HtmlElementValidationException() { }

    public HtmlElementValidationException(string? message) : base(message) { }

    public HtmlElementValidationException(string? message, Exception? innerException) : base(message, innerException) { }
}