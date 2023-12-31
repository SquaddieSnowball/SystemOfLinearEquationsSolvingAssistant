using SystemOfLinearEquationsSolvingAssistant.UI.Web.Controls.Base;

namespace SystemOfLinearEquationsSolvingAssistant.UI.Web.Controls.CompositeTags.Base;

/// <summary>
/// Serves as the base class for all composite tags.
/// </summary>
internal abstract class CompositeTag : HtmlElement
{
    /// <summary>
    /// Initializes a new instance of <see cref="CompositeTag"/> with the specified name.
    /// </summary>
    /// <param name="name">Tag name.</param>
    protected CompositeTag(string name) : base(name) { }
}