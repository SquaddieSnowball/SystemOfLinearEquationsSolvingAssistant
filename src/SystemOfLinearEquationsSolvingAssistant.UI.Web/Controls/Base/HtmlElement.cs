using SystemOfLinearEquationsSolvingAssistant.UI.Web.Controls.Exceptions;
using SystemOfLinearEquationsSolvingAssistant.UI.Web.Extensions.Controls.Base.Entities;

namespace SystemOfLinearEquationsSolvingAssistant.UI.Web.Controls.Base;

/// <summary>
/// Serves as the base class for all HTML elements.
/// </summary>
internal abstract class HtmlElement
{
    private readonly List<HtmlAttribute> _attributes = new();

    /// <summary>
    /// Gets the name of the HTML element.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets the attributes of the HTML element.
    /// </summary>
    public IEnumerable<HtmlAttribute> Attributes => _attributes;

    /// <summary>
    /// Initializes a new instance of <see cref="HtmlElement"/> with the specified name.
    /// </summary>
    /// <param name="name">The name of the HTML element.</param>
    /// <exception cref="ArgumentException"></exception>
    public HtmlElement(string name)
    {
        if (string.IsNullOrEmpty(name) is true)
            throw new ArgumentException("Name must not be null or empty.", nameof(name));

        Name = name;
    }

    /// <summary>
    /// Adds a new HTML attribute.
    /// </summary>
    /// <param name="attribute"><see cref="HtmlAttribute"/> to add.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public void AddAttribute(HtmlAttribute attribute)
    {
        if (attribute is null)
            throw new ArgumentNullException(nameof(attribute), "Attribute must not be null.");

        _attributes.Add(attribute);
    }

    /// <summary>
    /// Removes an HTML attribute.
    /// </summary>
    /// <param name="attribute"><see cref="HtmlAttribute"/> to remove.</param>
    /// <returns><see langword="true"/> if the specified HTML attribute has been removed; 
    /// otherwise, <see langword="false"/>.</returns>
    public bool RemoveAttribute(HtmlAttribute attribute) => _attributes.Remove(attribute);

    /// <summary>
    /// Generates a string containing the HTML markup of an HTML element.
    /// </summary>
    /// <param name="nestingLevel">The nesting level at which the HTML element is located.</param>
    /// <returns>A string containing the HTML markup of an HTML element.</returns>
    public abstract string GenerateHtml(int nestingLevel);

    /// <summary>
    /// Validates an HTML element.
    /// </summary>
    /// <exception cref="HtmlElementValidationException"></exception>
    protected virtual void Validate()
    {
        foreach (HtmlAttribute attribute in Attributes)
        {
            if (attribute.PropertyBinding?.IsSet is false)
                throw new HtmlElementValidationException($"\"{attribute.Parameter}\" attribute property binding not set.");
        }
    }
}