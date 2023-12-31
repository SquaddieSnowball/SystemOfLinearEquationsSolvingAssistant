using System.Reflection;
using SystemOfLinearEquationsSolvingAssistant.Dependencies.Entities;

namespace SystemOfLinearEquationsSolvingAssistant.Dependencies;

/// <summary>
/// Provides methods used to manage application dependencies.
/// </summary>
public static class DependenciesContainer
{
    private static readonly List<DependencyDescriptor> _dependencyDescriptors = new();

    /// <summary>
    /// Registers a new dependency in the dependency container.
    /// </summary>
    /// <typeparam name="TReal">Real type of the dependency.</typeparam>
    /// <param name="lifetime">The lifetime of the dependency object.</param>
    public static void Register<TReal>(DependencyObjectLifetime lifetime) where TReal : class =>
        Register<TReal, TReal>(lifetime);

    /// <summary>
    /// Registers a new dependency in the dependency container.
    /// </summary>
    /// <typeparam name="TAbstract">Abstract type of the dependency.</typeparam>
    /// <typeparam name="TReal">Real type of the dependency.</typeparam>
    /// <param name="lifetime">The lifetime of the dependency object.</param>
    /// <exception cref="ArgumentException"></exception>
    public static void Register<TAbstract, TReal>(DependencyObjectLifetime lifetime) where TReal : class, TAbstract
    {
        if (Enum.IsDefined(lifetime) is false)
            throw new ArgumentException("The specified lifetime value does not exist.", nameof(lifetime));

        Type abstractType = typeof(TAbstract);
        Type realType = typeof(TReal);

        if (_dependencyDescriptors.FirstOrDefault(d => d.AbstractType.Equals(abstractType) is true) is not null)
            throw new ArgumentException("This dependency has already been registered.", nameof(TAbstract));

        _dependencyDescriptors.Add(new DependencyDescriptor(abstractType, realType, lifetime));
    }

    /// <summary>
    /// Resolves an existing dependency in the dependency container.
    /// </summary>
    /// <typeparam name="TAbstract">Abstract type of the dependency.</typeparam>
    /// <returns>An instance of the <typeparamref name="TAbstract"/>'s real type.</returns>
    public static TAbstract? Resolve<TAbstract>() => (TAbstract?)Resolve(typeof(TAbstract));

    /// <summary>
    /// Resolves an existing dependency in the dependency container.
    /// </summary>
    /// <param name="abstractType">Abstract type of the dependency.</param>
    /// <returns>An instance of the <see cref="Type"/>'s real type.</returns>
    /// <exception cref="ArgumentException"></exception>
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