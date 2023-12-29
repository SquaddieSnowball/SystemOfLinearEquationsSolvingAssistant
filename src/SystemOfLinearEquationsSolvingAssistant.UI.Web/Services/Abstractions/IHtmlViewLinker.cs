using SystemOfLinearEquationsSolvingAssistant.UI.Web.Views.Base;

namespace SystemOfLinearEquationsSolvingAssistant.UI.Web.Services.Abstractions;

internal interface IHtmlViewLinker
{
    void Link(HtmlView viewLogicInstance, string viewVisualsPath);
}