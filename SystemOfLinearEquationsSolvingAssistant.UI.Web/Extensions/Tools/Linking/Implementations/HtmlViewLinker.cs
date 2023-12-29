using System.Reflection;
using System.Text;
using SystemOfLinearEquationsSolvingAssistant.UI.Web.Extensions.Controls.Base;
using SystemOfLinearEquationsSolvingAssistant.UI.Web.Extensions.Controls.Base.Entities;
using SystemOfLinearEquationsSolvingAssistant.UI.Web.Extensions.Tools.Linking.Exceptions;
using SystemOfLinearEquationsSolvingAssistant.UI.Web.Extensions.Tools.Parsing;
using SystemOfLinearEquationsSolvingAssistant.UI.Web.Extensions.Tools.Parsing.Entities;
using SystemOfLinearEquationsSolvingAssistant.UI.Web.Views.Base;

namespace SystemOfLinearEquationsSolvingAssistant.UI.Web.Extensions.Tools.Linking.Implementations;

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

        ParsingResults viewVisualsParsingResults;

        try
        {
            viewVisualsParsingResults = _htmlViewVisualsParser.Parse(viewVisualsPath);
        }
        catch
        {
            throw;
        }

        StringBuilder htmlPageStringBuilder = new();

        foreach ((string, CodeElement?) viewVisualsParsingResult in viewVisualsParsingResults)
        {
            _ = htmlPageStringBuilder.AppendLine(viewVisualsParsingResult.Item1);

            if (viewVisualsParsingResult.Item2 is null)
                continue;

            if ((viewVisualsParsingResult.Item2.HtmlElements.Any(e => e.Attributes
            .Any(a => a.PropertyBinding is not null) is true) is true) && (viewLogicInstance.ViewModel is null))
                throw new HtmlViewLinkingException("The HTML view cannot be linked because the view's visuals " +
                    "contain properties that require binding, but no view model has been set.");

            foreach (HtmlElement htmlElement in viewVisualsParsingResult.Item2.HtmlElements)
            {
                foreach (HtmlAttribute htmlAttribute in htmlElement.Attributes)
                    if (htmlAttribute.PropertyBinding is not null)
                    {
                        PropertyInfo? bindingPropertyInfo =
                            viewLogicInstance.ViewModel!.GetType().GetProperty(htmlAttribute.PropertyBinding.Name) ??
                            throw new HtmlViewLinkingException("The HTML view cannot be linked because the view model " +
                            $"does not contain a \"{htmlAttribute.PropertyBinding.Name}\" property that needs to be bound.");

                        htmlAttribute.PropertyBinding.Set(bindingPropertyInfo.GetValue(viewLogicInstance.ViewModel));
                    }

                string htmlElementMarkup;

                try
                {
                    htmlElementMarkup = htmlElement.GenerateHtml(viewVisualsParsingResult.Item2.NestingLevel);
                }
                catch
                {
                    throw;
                }

                _ = htmlPageStringBuilder.AppendLine(htmlElementMarkup);
            }
        }

        viewLogicInstance.Page = htmlPageStringBuilder.ToString();
    }
}