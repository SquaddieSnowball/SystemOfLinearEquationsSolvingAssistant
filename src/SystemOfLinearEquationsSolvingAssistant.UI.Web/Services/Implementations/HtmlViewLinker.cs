using System.Reflection;
using System.Text;
using SystemOfLinearEquationsSolvingAssistant.UI.Web.Controls.Base;
using SystemOfLinearEquationsSolvingAssistant.UI.Web.Extensions.Controls.Base.Entities;
using SystemOfLinearEquationsSolvingAssistant.UI.Web.Services.Abstractions;
using SystemOfLinearEquationsSolvingAssistant.UI.Web.Services.Entities.HtmlViewVisualsParser;
using SystemOfLinearEquationsSolvingAssistant.UI.Web.Services.Exceptions.HtmlViewLinker;
using SystemOfLinearEquationsSolvingAssistant.UI.Web.Views.Base;

namespace SystemOfLinearEquationsSolvingAssistant.UI.Web.Services.Implementations;

internal sealed class HtmlViewLinker : IHtmlViewLinker
{
    private readonly IHtmlViewVisualsParser _htmlViewVisualsParser;

    public HtmlViewLinker(IHtmlViewVisualsParser htmlViewVisualsParser)
    {
        if (htmlViewVisualsParser is null)
            throw new ArgumentNullException(nameof(htmlViewVisualsParser), "HTML view visuals parser must not be null.");

        _htmlViewVisualsParser = htmlViewVisualsParser;
    }

    public void Link(HtmlView viewLogicInstance, string viewVisualsPath)
    {
        if (viewLogicInstance is null)
            throw new ArgumentNullException(nameof(viewLogicInstance), "View logic instance must not be null.");

        if (string.IsNullOrEmpty(viewVisualsPath) is true)
            throw new ArgumentException("View visuals path must not be null or empty.", nameof(viewVisualsPath));

        ParsingResults viewVisualsParsingResults = _htmlViewVisualsParser.Parse(viewVisualsPath);
        StringBuilder htmlPageStringBuilder = new();

        foreach ((string, CodeElement?) viewVisualsParsingResult in viewVisualsParsingResults)
        {
            _ = htmlPageStringBuilder.AppendLine(viewVisualsParsingResult.Item1);

            if (viewVisualsParsingResult.Item2 is null)
                continue;

            if (viewVisualsParsingResult.Item2.HtmlElements.Any(e => e.Attributes
                .Any(a => a.PropertyBinding is not null) is true is true) && (viewLogicInstance.ViewModel is null))
            {
                throw new HtmlViewLinkingException("The HTML view cannot be linked because the view's visuals " +
                    "contain properties that require binding, but no view model has been set.");
            }

            foreach (HtmlElement htmlElement in viewVisualsParsingResult.Item2.HtmlElements)
            {
                foreach (HtmlAttribute htmlAttribute in htmlElement.Attributes)
                {
                    if (htmlAttribute.PropertyBinding is not null)
                    {
                        PropertyInfo? bindingPropertyInfo =
                            viewLogicInstance.ViewModel!.GetType().GetProperty(htmlAttribute.PropertyBinding.Name) ??
                            throw new HtmlViewLinkingException("The HTML view cannot be linked because the view model " +
                            $"does not contain a \"{htmlAttribute.PropertyBinding.Name}\" property that needs to be bound.");

                        htmlAttribute.PropertyBinding.Set(bindingPropertyInfo.GetValue(viewLogicInstance.ViewModel));
                    }
                }

                string htmlElementMarkup = htmlElement.GenerateHtml(viewVisualsParsingResult.Item2.NestingLevel);
                _ = htmlPageStringBuilder.AppendLine(htmlElementMarkup);
            }
        }

        viewLogicInstance.Page = htmlPageStringBuilder.ToString();
    }
}