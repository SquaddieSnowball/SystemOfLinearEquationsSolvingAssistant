namespace SystemOfLinearEquationsSolvingAssistant.UI.Web.Controls.Exceptions;

/// <summary>
/// Represents the exception that is thrown when validation of an HTML element fails.
/// </summary>
internal sealed class HtmlElementValidationException : Exception
{
    /// <summary>
    /// Initializes a new instance of <see cref="HtmlElementValidationException"/>.
    /// </summary>
    public HtmlElementValidationException() { }

    /// <summary>
    /// Initializes a new instance of <see cref="HtmlElementValidationException"/> with the specified error message.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    public HtmlElementValidationException(string? message) : base(message) { }

    /// <summary>
    /// Initializes a new instance of <see cref="HtmlElementValidationException"/> with the specified error message 
    /// and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public HtmlElementValidationException(string? message, Exception? innerException) : base(message, innerException) { }
}