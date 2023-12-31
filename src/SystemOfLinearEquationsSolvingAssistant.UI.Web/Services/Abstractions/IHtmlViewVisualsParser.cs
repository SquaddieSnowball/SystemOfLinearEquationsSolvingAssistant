using SystemOfLinearEquationsSolvingAssistant.UI.Web.Services.Entities.HtmlViewVisualsParser;

namespace SystemOfLinearEquationsSolvingAssistant.UI.Web.Services.Abstractions;

/// <summary>
/// Provides a method used to parse the view visuals.
/// </summary>
internal interface IHtmlViewVisualsParser
{
    /// <summary>
    /// Parses the view located at the specified path.
    /// </summary>
    /// <param name="viewVisualsPath">Path to view visuals.</param>
    /// <returns>A new instance of <see cref="ParsingResults"/>.</returns>
    ParsingResults Parse(string viewVisualsPath);
}