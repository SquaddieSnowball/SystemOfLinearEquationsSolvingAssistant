namespace SystemOfLinearEquationsSolvingAssistant.Dependencies.Entities;

internal sealed class DependencyDescriptor
{
    public Type AbstractType { get; }

    public Type RealType { get; }

    public DependencyObjectLifetime DependencyObjectLifetime { get; }

    public object? DependencyObject { get; set; }

    public DependencyDescriptor(Type abstractType, Type realType, DependencyObjectLifetime dependencyObjectLifetime) =>
        (AbstractType, RealType, DependencyObjectLifetime) = (abstractType, realType, dependencyObjectLifetime);
}