using SystemOfLinearEquationsSolvingAssistant.Dependencies.Entities;

namespace SystemOfLinearEquationsSolvingAssistant.Dependencies;

public static class DependenciesContainer
{
    private static readonly List<Dependency> _dependencies = new();

    public static void Register<T>(DependencyObjectLifetime dependencyObjectLifetime) =>
        Register<T, T>(dependencyObjectLifetime);

    public static void Register<TAbstract, TConcrete>(DependencyObjectLifetime dependencyObjectLifetime)
        where TConcrete : TAbstract
    {
        if (Enum.IsDefined(dependencyObjectLifetime) is false)
            throw new ArgumentException("The specified dependency object lifetime value does not exist.",
                nameof(dependencyObjectLifetime));

        Type abstractType = typeof(TAbstract);
        Type concreteType = typeof(TConcrete);

        if (_dependencies.FirstOrDefault(d => d.AbstractType.Equals(abstractType)) is not null)
            throw new ArgumentException("This dependency has already been registered.");

        _dependencies.Add(new Dependency(abstractType, concreteType, dependencyObjectLifetime));
    }

    public static T? Resolve<T>(params object?[]? ctorArgs)
    {
        Dependency? dependency = _dependencies.FirstOrDefault(d => d.AbstractType.Equals(typeof(T))) ??
            throw new ArgumentException("This dependency has not yet been registered.");

        try
        {
            return dependency.DependencyObjectLifetime switch
            {
                DependencyObjectLifetime.Transient =>
                    (T?)(dependency.DependencyObject = Activator.CreateInstance(dependency.RealType, ctorArgs)),
                DependencyObjectLifetime.Singleton =>
                    (T?)(dependency.DependencyObject ??= Activator.CreateInstance(dependency.RealType, ctorArgs)),
                _ =>
                    throw new NotImplementedException("The behavior for the specified " +
                        "dependency object lifetime value is not yet implemented."),
            };
        }
        catch
        {
            throw;
        }
    }
}