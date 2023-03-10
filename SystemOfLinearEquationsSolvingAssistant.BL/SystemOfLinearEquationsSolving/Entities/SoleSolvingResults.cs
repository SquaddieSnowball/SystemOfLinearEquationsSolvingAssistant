namespace SystemOfLinearEquationsSolvingAssistant.BL.SystemOfLinearEquationsSolving.Entities;

public sealed class SoleSolvingResults
{
    public double[] SolutionSet { get; }

    public TimeSpan ElapsedTime { get; }

    public SoleSolvingResults(double[] solutionSet, TimeSpan elapsedTime) =>
        (SolutionSet, ElapsedTime) = (solutionSet, elapsedTime);
}