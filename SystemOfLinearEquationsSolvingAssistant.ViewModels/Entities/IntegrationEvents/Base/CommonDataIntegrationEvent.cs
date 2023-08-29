namespace SystemOfLinearEquationsSolvingAssistant.ViewModels.Entities.IntegrationEvents.Base;

public abstract class CommonDataIntegrationEvent : IntegrationEvent
{
    private readonly Dictionary<string, object?> _data = new();

    public IEnumerable<KeyValuePair<string, object?>> Data => _data;

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
        catch
        {
            throw;
        }

        return this;
    }
}