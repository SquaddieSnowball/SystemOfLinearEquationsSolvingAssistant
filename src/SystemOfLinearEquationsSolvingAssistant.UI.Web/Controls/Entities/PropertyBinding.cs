namespace SystemOfLinearEquationsSolvingAssistant.UI.Web.Extensions.Controls.Base.Entities;

/// <summary>
/// Represents an HTML attribute property binding.
/// </summary>
internal sealed class PropertyBinding
{
    /// <summary>
    /// Gets the name of the property binding.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets the value of the property binding.
    /// </summary>
    public object? Value { get; private set; }

    /// <summary>
    /// Gets a value indicating whether the property binding is set.
    /// </summary>
    public bool IsSet { get; private set; }

    /// <summary>
    /// Initializes a new instance of <see cref="PropertyBinding"/> with the specified name.
    /// </summary>
    /// <param name="name">The name of the property binding.</param>
    /// <exception cref="ArgumentException"></exception>
    public PropertyBinding(string name)
    {
        if (string.IsNullOrEmpty(name) is true)
            throw new ArgumentException("Name must not be null or empty.", nameof(name));

        Name = name;
    }

    /// <summary>
    /// Sets the property binding.
    /// </summary>
    /// <param name="value">The property binding value.</param>
    public void Set(object? value) => (Value, IsSet) = (value, true);
}