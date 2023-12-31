namespace SystemOfLinearEquationsSolvingAssistant.BL.Services.Entities.SoleSolver;

/// <summary>
/// Represents the results of solving a system of linear equations.
/// </summary>
public sealed class SoleSolvingResults
{
    /// <summary>
    /// Gets the solution set.
    /// </summary>
    public double[] SolutionSet { get; }

    /// <summary>
    /// Gets the time elapsed during the solution process.
    /// </summary>
    public TimeSpan ElapsedTime { get; }

    /// <summary>
    /// Initializes a new instance of <see cref="SoleSolvingResults"/> with the specified solution set and elapsed time.
    /// </summary>
    /// <param name="solutionSet">The solution set.</param>
    /// <param name="elapsedTime">The time elapsed during the solution process.</param>
    public SoleSolvingResults(double[] solutionSet, TimeSpan elapsedTime) =>
        (SolutionSet, ElapsedTime) =
        (solutionSet, elapsedTime);
}