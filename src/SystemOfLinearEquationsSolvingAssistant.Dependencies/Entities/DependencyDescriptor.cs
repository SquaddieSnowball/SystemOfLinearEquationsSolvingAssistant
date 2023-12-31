namespace SystemOfLinearEquationsSolvingAssistant.Dependencies.Entities;

/// <summary>
/// Represents a dependency descriptor.
/// </summary>
internal sealed class DependencyDescriptor
{
    /// <summary>
    /// Gets the abstract type of the dependency.
    /// </summary>
    public Type AbstractType { get; }

    /// <summary>
    /// Gets the real type of the dependency.
    /// </summary>
    public Type RealType { get; }

    /// <summary>
    /// Gets the lifetime of the dependency object.
    /// </summary>
    public DependencyObjectLifetime Lifetime { get; }

    /// <summary>
    /// Gets the dependency object.
    /// </summary>
    public object? DependencyObject { get; set; }

    /// <summary>
    /// Initializes a new instance of <see cref="DependencyDescriptor"/> with the specified 
    /// abstract type, real type, and lifetime of the dependency object.
    /// </summary>
    /// <param name="abstractType">Abstract type of the dependency.</param>
    /// <param name="realType">Real type of the dependency.</param>
    /// <param name="lifetime">The lifetime of the dependency object.</param>
    public DependencyDescriptor(Type abstractType, Type realType, DependencyObjectLifetime lifetime) =>
        (AbstractType, RealType, Lifetime) =
        (abstractType, realType, lifetime);
}