using SystemOfLinearEquationsSolvingAssistant.UI.Web.Views.Base;

namespace SystemOfLinearEquationsSolvingAssistant.UI.Web.Extensions.Tools.Linking;

internal interface IHtmlViewLinker
{
    void Link(HtmlView viewLogicInstance, string viewVisualsPath);
}