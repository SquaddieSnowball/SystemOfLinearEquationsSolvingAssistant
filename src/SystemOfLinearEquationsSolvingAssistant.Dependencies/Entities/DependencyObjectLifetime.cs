namespace SystemOfLinearEquationsSolvingAssistant.Dependencies.Entities;

/// <summary>
/// Represents the lifetime of a dependency object.
/// </summary>
public enum DependencyObjectLifetime
{
    /// <summary>
    /// Represents the transient lifetime of a dependency object.
    /// </summary>
    Transient,

    /// <summary>
    /// Represents the singletone lifetime of a dependency object.
    /// </summary>
    Singleton
}