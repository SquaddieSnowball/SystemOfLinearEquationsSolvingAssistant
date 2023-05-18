using System.Reflection;
using SystemOfLinearEquationsSolvingAssistant.Dependencies.Entities;

namespace SystemOfLinearEquationsSolvingAssistant.Dependencies;

public static class DependenciesContainer
{
    private static readonly List<DependencyDescriptor> _dependencyDescriptors = new();

    public static void Register<T>(DependencyObjectLifetime dependencyObjectLifetime)
        where T : class
    {
        try
        {
            Register<T, T>(dependencyObjectLifetime);
        }
        catch (ArgumentException ex) when (ex.Message.Contains("This dependency has already been registered.") is true)
        {
            throw new ArgumentException("This dependency has already been registered.", nameof(T));
        }
        catch
        {
            throw;
        }
    }

    public static void Register<TAbstract, TConcrete>(DependencyObjectLifetime dependencyObjectLifetime)
        where TConcrete : class, TAbstract
    {
        if (Enum.IsDefined(dependencyObjectLifetime) is false)
            throw new ArgumentException("The specified dependency object lifetime value does not exist.",
                nameof(dependencyObjectLifetime));

        Type abstractType = typeof(TAbstract);
        Type concreteType = typeof(TConcrete);

        if (_dependencyDescriptors.FirstOrDefault(d => d.AbstractType.Equals(abstractType) is true) is not null)
            throw new ArgumentException("This dependency has already been registered.", nameof(TAbstract));

        _dependencyDescriptors.Add(new DependencyDescriptor(abstractType, concreteType, dependencyObjectLifetime));
    }

    public static T? Resolve<T>()
    {
        try
        {
            return (T?)Resolve(typeof(T));
        }
        catch (ArgumentException ex) when (ex.Message.Contains("This dependency has not yet been registered.") is true)
        {
            throw new ArgumentException("This dependency has not yet been registered.", nameof(T));
        }
        catch
        {
            throw;
        }
    }

    public static object? Resolve(Type abstractType)
    {
        DependencyDescriptor? dependencyDescriptor = _dependencyDescriptors
            .FirstOrDefault(d => d.AbstractType.Equals(abstractType) is true) ??
            throw new ArgumentException("This dependency has not yet been registered.", nameof(abstractType));

        if ((dependencyDescriptor.DependencyObjectLifetime is DependencyObjectLifetime.Singleton) &&
            (dependencyDescriptor.DependencyObject is not null))
            return dependencyDescriptor.DependencyObject;

        IEnumerable<ParameterInfo[]> constructorParametersEnumerable = dependencyDescriptor.RealType
            .GetConstructors().Select(c => c.GetParameters()).OrderByDescending(p => p.Length);

        if (constructorParametersEnumerable.Any() is false)
            throw new ArgumentException("The dependency object cannot be instantiated because " +
                $"\"{dependencyDescriptor.RealType.FullName}\" does not provide public constructors.");

        foreach (ParameterInfo[] constructorParameters in constructorParametersEnumerable)
        {
            if (constructorParameters.Any(p => _dependencyDescriptors.FirstOrDefault
                (d => d.AbstractType.Equals(p.ParameterType) is true) is null) is true)
                continue;

            object?[] constructorArguments = constructorParameters.Select(p => Resolve(p.ParameterType)).ToArray();
            object? objectInstance = Activator.CreateInstance(dependencyDescriptor.RealType, constructorArguments);

            if (dependencyDescriptor.DependencyObjectLifetime is DependencyObjectLifetime.Singleton)
                dependencyDescriptor.DependencyObject = objectInstance;

            return objectInstance;
        }

        throw new ArgumentException("The dependency object cannot be instantiated because " +
            $"\"{dependencyDescriptor.RealType.FullName}\" does not provide suitable constructors.");
    }
}