namespace SystemOfLinearEquationsSolvingAssistant.Dependencies.Entities;

internal sealed class Dependency
{
    public Type AbstractType { get; }

    public Type RealType { get; }

    public DependencyObjectLifetime DependencyObjectLifetime { get; }

    public object? DependencyObject { get; set; }

    public Dependency(Type abstractType, Type realType, DependencyObjectLifetime dependencyObjectLifetime) =>
        (AbstractType, RealType, DependencyObjectLifetime) = (abstractType, realType, dependencyObjectLifetime);
}