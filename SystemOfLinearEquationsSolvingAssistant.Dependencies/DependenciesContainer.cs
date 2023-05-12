using System.Reflection;
using SystemOfLinearEquationsSolvingAssistant.Dependencies.Entities;

namespace SystemOfLinearEquationsSolvingAssistant.Dependencies;

public static class DependenciesContainer
{
    private static readonly MethodInfo _resolveMethod = typeof(DependenciesContainer).GetMethod(nameof(Resolve))!;
    private static readonly List<Dependency> _dependencies = new();

    public static void Register<T>(DependencyObjectLifetime dependencyObjectLifetime)
    {
        try
        {
            Register<T, T>(dependencyObjectLifetime);
        }
        catch (ArgumentException ex) when
            (ex.Message.Equals("This dependency has already been registered.", StringComparison.Ordinal) is true)
        {
            throw new ArgumentException("This dependency has already been registered.", nameof(T));
        }
        catch
        {
            throw;
        }
    }

    public static void Register<TAbstract, TConcrete>(DependencyObjectLifetime dependencyObjectLifetime)
        where TConcrete : TAbstract
    {
        if (Enum.IsDefined(dependencyObjectLifetime) is false)
            throw new ArgumentException("The specified dependency object lifetime value does not exist.",
                nameof(dependencyObjectLifetime));

        Type abstractType = typeof(TAbstract);
        Type concreteType = typeof(TConcrete);

        if (_dependencies.FirstOrDefault(d => d.AbstractType.Equals(abstractType) is true) is not null)
            throw new ArgumentException("This dependency has already been registered.", nameof(TAbstract));

        _dependencies.Add(new Dependency(abstractType, concreteType, dependencyObjectLifetime));
    }

    public static T? Resolve<T>()
    {
        Dependency? dependency = _dependencies.FirstOrDefault(d => d.AbstractType.Equals(typeof(T)) is true) ??
            throw new ArgumentException("This dependency has not yet been registered.", nameof(T));

        if ((dependency.DependencyObjectLifetime is DependencyObjectLifetime.Singleton) &&
            (dependency.DependencyObject != default))
            return (T?)dependency.DependencyObject;

        IEnumerable<ParameterInfo[]> constructorParametersEnumerable = dependency.RealType
            .GetConstructors().Select(c => c.GetParameters()).OrderByDescending(p => p.Length);

        if (constructorParametersEnumerable.Any() is false)
            throw new ArgumentException("The dependency object cannot be instantiated because " +
                $"\"{dependency.RealType.FullName}\" does not provide public constructors.");

        foreach (ParameterInfo[] constructorParameters in constructorParametersEnumerable)
        {
            if (constructorParameters.Any(p => _dependencies.FirstOrDefault
                (d => d.AbstractType.Equals(p.ParameterType) is true) is null) is true)
                continue;

            object?[] constructorArguments = constructorParameters
                .Select(p => _resolveMethod.MakeGenericMethod(p.ParameterType).Invoke(default, default))
                .ToArray();

            object? objectInstance = Activator.CreateInstance(dependency.RealType, constructorArguments);

            if (dependency.DependencyObjectLifetime is DependencyObjectLifetime.Singleton)
                dependency.DependencyObject = objectInstance;

            return (T?)objectInstance;
        }

        throw new ArgumentException("The dependency object cannot be instantiated because " +
            $"\"{dependency.RealType.FullName}\" does not provide suitable constructors.");
    }
}