namespace SystemOfLinearEquationsSolvingAssistant.UI.Web.Services.Exceptions.HtmlViewVisualsParser;

/// <summary>
/// Represents the exception that is thrown when parsing the visual elements of an HTML view fails.
/// </summary>
internal sealed class HtmlViewVisualsParsingException : Exception
{
    /// <summary>
    /// Initializes a new instance of <see cref="HtmlViewVisualsParsingException"/>.
    /// </summary>
    public HtmlViewVisualsParsingException() { }

    /// <summary>
    /// Initializes a new instance of <see cref="HtmlViewVisualsParsingException"/> with the specified error message.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    public HtmlViewVisualsParsingException(string? message) : base(message) { }

    /// <summary>
    /// Initializes a new instance of <see cref="HtmlViewVisualsParsingException"/> with the specified error message 
    /// and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public HtmlViewVisualsParsingException(string? message, Exception? innerException) : base(message, innerException) { }
}