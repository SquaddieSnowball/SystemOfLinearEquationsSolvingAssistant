namespace SystemOfLinearEquationsSolvingAssistant.ViewModels.Services.Entities.EventBus.IntegrationEvents.Base;

/// <summary>
/// Represents an integration event used to publish common data.
/// </summary>
public abstract class CommonDataIntegrationEvent : IntegrationEvent
{
    private readonly Dictionary<string, object?> _data = new();

    /// <summary>
    /// Gets event data.
    /// </summary>
    public IEnumerable<KeyValuePair<string, object?>> Data => _data;

    /// <summary>
    /// Adds data to the event.
    /// </summary>
    /// <param name="key">Data key.</param>
    /// <param name="value">Data value.</param>
    /// <returns>The current instance of <see cref="CommonDataIntegrationEvent"/>.</returns>
    /// <exception cref="ArgumentException"></exception>
    public CommonDataIntegrationEvent AddData(string key, object? value)
    {
        if (string.IsNullOrEmpty(key) is true)
            throw new ArgumentException("Key must not be null or empty.", nameof(key));

        try
        {
            _data.Add(key, value);
        }
        catch (ArgumentException)
        {
            throw new ArgumentException("A data record with the specified key already exists.", nameof(key));
        }

        return this;
    }
}