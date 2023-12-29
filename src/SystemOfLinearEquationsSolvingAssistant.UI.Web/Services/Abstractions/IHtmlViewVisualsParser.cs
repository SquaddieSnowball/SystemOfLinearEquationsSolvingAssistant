using SystemOfLinearEquationsSolvingAssistant.UI.Web.Services.Entities.HtmlViewVisualsParser;

namespace SystemOfLinearEquationsSolvingAssistant.UI.Web.Services.Abstractions;

internal interface IHtmlViewVisualsParser
{
    ParsingResults Parse(string viewVisualsPath);
}