using SystemOfLinearEquationsSolvingAssistant.UI.Web.Controls.Base;

namespace SystemOfLinearEquationsSolvingAssistant.UI.Web.Controls.CompositeTags.Base;

internal abstract class CompositeTag : HtmlElement
{
    protected CompositeTag(string name) : base(name) { }
}