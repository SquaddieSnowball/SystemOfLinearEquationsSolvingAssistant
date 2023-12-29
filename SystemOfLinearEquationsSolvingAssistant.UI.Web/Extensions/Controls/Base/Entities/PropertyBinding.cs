namespace SystemOfLinearEquationsSolvingAssistant.UI.Web.Extensions.Controls.Base.Entities;

internal sealed class PropertyBinding
{
    public string Name { get; }

    public object? Value { get; private set; }

    public bool IsSet { get; private set; }

    public PropertyBinding(string name)
    {
        if (string.IsNullOrEmpty(name) is true)
            throw new ArgumentException("Name must not be null or empty.", nameof(name));

        Name = name;
    }

    public void Set(object? value) => (Value, IsSet) = (value, true);
}