using SystemOfLinearEquationsSolvingAssistant.UI.Web.Controls.Base;

namespace SystemOfLinearEquationsSolvingAssistant.UI.Web.Services.Entities.HtmlViewVisualsParser;

internal sealed class CodeElement
{
    public IEnumerable<HtmlElement> HtmlElements { get; }

    public int NestingLevel { get; }

    public CodeElement(IEnumerable<HtmlElement> htmlElements, int nestingLevel)
    {
        if (htmlElements is null)
            throw new ArgumentNullException(nameof(htmlElements), "HTML elements must not be null.");

        if (nestingLevel < 0)
            throw new ArgumentException("Nesting level must not be negative.", nameof(nestingLevel));

        (HtmlElements, NestingLevel) = (htmlElements, nestingLevel);
    }
}