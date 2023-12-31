using SystemOfLinearEquationsSolvingAssistant.UI.Web.Controls.Base;

namespace SystemOfLinearEquationsSolvingAssistant.UI.Web.Services.Entities.HtmlViewVisualsParser;

/// <summary>
/// Represents a code element.
/// </summary>
internal sealed class CodeElement
{
    /// <summary>
    /// Gets the HTML elements of the code element.
    /// </summary>
    public IEnumerable<HtmlElement> HtmlElements { get; }

    /// <summary>
    /// Gets the nesting level of the code element.
    /// </summary>
    public int NestingLevel { get; }

    /// <summary>
    /// Initializes a new instance of <see cref="CodeElement"/> with the specified HTML elements and nesting level.
    /// </summary>
    /// <param name="htmlElements">The HTML elements of the code element.</param>
    /// <param name="nestingLevel">The nesting level of the code element.</param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentException"></exception>
    public CodeElement(IEnumerable<HtmlElement> htmlElements, int nestingLevel)
    {
        if (htmlElements is null)
            throw new ArgumentNullException(nameof(htmlElements), "HTML elements must not be null.");

        if (nestingLevel < 0)
            throw new ArgumentException("Nesting level must not be negative.", nameof(nestingLevel));

        (HtmlElements, NestingLevel) = (htmlElements, nestingLevel);
    }
}