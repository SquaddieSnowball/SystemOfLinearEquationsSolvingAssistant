using System.Reflection;
using SystemOfLinearEquationsSolvingAssistant.Dependencies.Entities;

namespace SystemOfLinearEquationsSolvingAssistant.Dependencies;

public static class DependenciesContainer
{
    private static readonly List<DependencyDescriptor> _dependencyDescriptors = new();

    public static void Register<T>(DependencyObjectLifetime lifetime) where T : class => Register<T, T>(lifetime);

    public static void Register<TAbstract, TConcrete>(DependencyObjectLifetime lifetime) where TConcrete : class, TAbstract
    {
        if (Enum.IsDefined(lifetime) is false)
            throw new ArgumentException("The specified lifetime value does not exist.", nameof(lifetime));

        Type abstractType = typeof(TAbstract);
        Type concreteType = typeof(TConcrete);

        if (_dependencyDescriptors.FirstOrDefault(d => d.AbstractType.Equals(abstractType) is true) is not null)
            throw new ArgumentException("This dependency has already been registered.", nameof(TAbstract));

        _dependencyDescriptors.Add(new DependencyDescriptor(abstractType, concreteType, lifetime));
    }

    public static T? Resolve<T>() => (T?)Resolve(typeof(T));

    public static object? Resolve(Type abstractType)
    {
        DependencyDescriptor? dependencyDescriptor = _dependencyDescriptors
            .FirstOrDefault(d => d.AbstractType.Equals(abstractType) is true) ??
            throw new ArgumentException("This dependency has not yet been registered.", nameof(abstractType));

        if (dependencyDescriptor is { Lifetime: DependencyObjectLifetime.Singleton, DependencyObject: not null })
            return dependencyDescriptor.DependencyObject;

        IEnumerable<ParameterInfo[]> constructorParametersEnumerable = dependencyDescriptor
            .RealType
            .GetConstructors()
            .Select(c => c.GetParameters())
            .OrderByDescending(p => p.Length);

        if (constructorParametersEnumerable.Any() is false)
        {
            throw new ArgumentException("The dependency object cannot be instantiated because " +
                $"\"{dependencyDescriptor.RealType.FullName}\" does not provide public constructors.");
        }

        foreach (ParameterInfo[] constructorParameters in constructorParametersEnumerable)
        {
            if (constructorParameters.Any(p => _dependencyDescriptors.FirstOrDefault(d =>
                d.AbstractType.Equals(p.ParameterType) is true) is null) is true)
                continue;

            object?[] constructorArguments = constructorParameters.Select(p => Resolve(p.ParameterType)).ToArray();
            object? objectInstance = Activator.CreateInstance(dependencyDescriptor.RealType, constructorArguments);

            if (dependencyDescriptor.Lifetime is DependencyObjectLifetime.Singleton)
                dependencyDescriptor.DependencyObject = objectInstance;

            return objectInstance;
        }

        throw new ArgumentException("The dependency object cannot be instantiated because " +
            $"\"{dependencyDescriptor.RealType.FullName}\" does not provide suitable constructors.");
    }
}