using SystemOfLinearEquationsSolvingAssistant.BL.SystemOfLinearEquationsSolving.Entities;

namespace SystemOfLinearEquationsSolvingAssistant.BL.SystemOfLinearEquationsSolving.SolvingAlgorithms;

internal static class SoleSolvingAlgorithmsParallel
{
    internal static Func<Sole, int, double[]> GetAlgorithmMethod(SoleSolvingAlgorithmParallel solvingAlgorithm) =>
        solvingAlgorithm switch
        {
            _ => throw new NotImplementedException("This solving algorithm has not yet been implemented.")
        };
}