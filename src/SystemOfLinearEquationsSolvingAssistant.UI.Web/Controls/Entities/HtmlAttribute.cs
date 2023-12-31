namespace SystemOfLinearEquationsSolvingAssistant.UI.Web.Extensions.Controls.Base.Entities;

/// <summary>
/// Represents an HTML attribute.
/// </summary>
internal sealed class HtmlAttribute
{
    /// <summary>
    /// Gets the attribute parameter.
    /// </summary>
    public string Parameter { get; }

    /// <summary>
    /// Gets the value of the attribute.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Gets the attribute property binding.
    /// </summary>
    public PropertyBinding? PropertyBinding { get; }

    /// <summary>
    /// Initializes a new instance of <see cref="HtmlAttribute"/> with the specified parameter, value and property binding.
    /// </summary>
    /// <param name="parameter">Attribute parameter.</param>
    /// <param name="value">Attribute value.</param>
    /// <param name="propertyBinding">Attribute property binding.</param>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    public HtmlAttribute(string parameter, string value, PropertyBinding? propertyBinding = default)
    {
        if (string.IsNullOrEmpty(parameter) is true)
            throw new ArgumentException("Parameter must not be null or empty.", nameof(parameter));

        if (value is null)
            throw new ArgumentNullException(nameof(value), "Value must not be null.");

        (Parameter, Value, PropertyBinding) = (parameter, value, propertyBinding);
    }
}