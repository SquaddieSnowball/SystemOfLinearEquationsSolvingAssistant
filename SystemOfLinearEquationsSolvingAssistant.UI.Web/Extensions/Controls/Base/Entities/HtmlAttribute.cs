namespace SystemOfLinearEquationsSolvingAssistant.UI.Web.Extensions.Controls.Base.Entities;

internal sealed class HtmlAttribute
{
    public string Parameter { get; }

    public string Value { get; }

    public PropertyBinding? PropertyBinding { get; }

    public HtmlAttribute(string parameter, string value, PropertyBinding? propertyBinding = default)
    {
        if (string.IsNullOrEmpty(parameter) is true)
            throw new ArgumentException("Parameter must not be null or empty.", nameof(parameter));

        if (value is null)
            throw new ArgumentNullException(nameof(value), "Value must not be null.");

        (Parameter, Value, PropertyBinding) = (parameter, value, propertyBinding);
    }
}