namespace SystemOfLinearEquationsSolvingAssistant.UI.Web.Services.Exceptions.HtmlViewLinker;

/// <summary>
/// Represents the exception that is thrown when HTML view linking fails.
/// </summary>
internal sealed class HtmlViewLinkingException : Exception
{
    /// <summary>
    /// Initializes a new instance of <see cref="HtmlViewLinkingException"/>.
    /// </summary>
    public HtmlViewLinkingException() { }

    /// <summary>
    /// Initializes a new instance of <see cref="HtmlViewLinkingException"/> with the specified error message.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    public HtmlViewLinkingException(string? message) : base(message) { }

    /// <summary>
    /// Initializes a new instance of <see cref="HtmlViewLinkingException"/> with the specified error message 
    /// and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public HtmlViewLinkingException(string? message, Exception? innerException) : base(message, innerException) { }
}