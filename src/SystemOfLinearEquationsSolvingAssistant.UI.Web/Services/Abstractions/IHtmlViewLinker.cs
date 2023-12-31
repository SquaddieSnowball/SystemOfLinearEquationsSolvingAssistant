using SystemOfLinearEquationsSolvingAssistant.UI.Web.Views.Base;

namespace SystemOfLinearEquationsSolvingAssistant.UI.Web.Services.Abstractions;

/// <summary>
/// Provides a method used to link views.
/// </summary>
internal interface IHtmlViewLinker
{
    /// <summary>
    /// Links the logic of the view to its visuals.
    /// </summary>
    /// <param name="viewLogicInstance">The logic of the view.</param>
    /// <param name="viewVisualsPath">Path to view visuals.</param>
    void Link(HtmlView viewLogicInstance, string viewVisualsPath);
}