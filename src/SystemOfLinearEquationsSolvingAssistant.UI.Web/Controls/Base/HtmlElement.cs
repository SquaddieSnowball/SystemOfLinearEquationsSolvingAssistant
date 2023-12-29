using SystemOfLinearEquationsSolvingAssistant.UI.Web.Controls.Exceptions;
using SystemOfLinearEquationsSolvingAssistant.UI.Web.Extensions.Controls.Base.Entities;

namespace SystemOfLinearEquationsSolvingAssistant.UI.Web.Controls.Base;

internal abstract class HtmlElement
{
    private readonly List<HtmlAttribute> _attributes = new();

    public string Name { get; }

    public IEnumerable<HtmlAttribute> Attributes => _attributes;

    public HtmlElement(string name)
    {
        if (string.IsNullOrEmpty(name) is true)
            throw new ArgumentException("Name must not be null or empty.", nameof(name));

        Name = name;
    }

    public void AddAttribute(HtmlAttribute attribute)
    {
        if (attribute is null)
            throw new ArgumentNullException(nameof(attribute), "Attribute must not be null.");

        _attributes.Add(attribute);
    }

    public bool RemoveAttribute(HtmlAttribute attribute) => _attributes.Remove(attribute);

    protected virtual void Validate()
    {
        foreach (HtmlAttribute attribute in Attributes)
            if (attribute.PropertyBinding?.IsSet is false)
                throw new HtmlElementValidationException($"\"{attribute.Parameter}\" attribute property binding not set.");
    }

    public abstract string GenerateHtml(int nestingLevel);
}