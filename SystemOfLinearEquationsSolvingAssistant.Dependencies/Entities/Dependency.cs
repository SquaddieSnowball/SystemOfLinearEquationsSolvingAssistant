namespace SystemOfLinearEquationsSolvingAssistant.Dependencies.Entities;

internal sealed class Dependency
{
    internal Type AbstractType { get; }

    internal Type RealType { get; }

    internal DependencyObjectLifetime DependencyObjectLifetime { get; }

    internal object? DependencyObject { get; set; }

    internal Dependency(Type abstractType, Type realType, DependencyObjectLifetime dependencyObjectLifetime) =>
        (AbstractType, RealType, DependencyObjectLifetime) = (abstractType, realType, dependencyObjectLifetime);
}