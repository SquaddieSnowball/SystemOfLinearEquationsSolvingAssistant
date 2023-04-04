namespace SystemOfLinearEquationsSolvingAssistant.Dependencies;

public static class DependenciesContainer
{
    private static readonly Dictionary<Type, Type> _dependencies = new();

    public static IEnumerable<KeyValuePair<Type, Type>> GetAll() => _dependencies;

    public static void Register<TAbstract, TConcrete>() where TConcrete : TAbstract
    {
        try
        {
            _dependencies.Add(typeof(TAbstract), typeof(TConcrete));
        }
        catch (ArgumentException)
        {
            throw new ArgumentException("This dependency has already been registered.");
        }
    }

    public static T? Resolve<T>(params object?[]? ctorArgs)
    {
        Type type = typeof(T);

        if (_dependencies.ContainsKey(type) is true)
        {
            try
            {
                return (T?)Activator.CreateInstance(_dependencies[type], ctorArgs);
            }
            catch
            {
                throw;
            }
        }
        else
            throw new ArgumentException("This dependency has not yet been registered.");
    }
}