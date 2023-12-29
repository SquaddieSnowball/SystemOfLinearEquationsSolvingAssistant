using SystemOfLinearEquationsSolvingAssistant.UI.Web.Extensions.Tools.Parsing.Entities;

namespace SystemOfLinearEquationsSolvingAssistant.UI.Web.Extensions.Tools.Parsing;

internal interface IHtmlViewVisualsParser
{
    ParsingResults Parse(string viewVisualsPath);
}