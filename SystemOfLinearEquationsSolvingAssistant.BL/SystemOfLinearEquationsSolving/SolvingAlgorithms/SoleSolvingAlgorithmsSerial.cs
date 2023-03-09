using SystemOfLinearEquationsSolvingAssistant.BL.SystemOfLinearEquationsSolving.Entities;

namespace SystemOfLinearEquationsSolvingAssistant.BL.SystemOfLinearEquationsSolving.SolvingAlgorithms;

internal static class SoleSolvingAlgorithmsSerial
{
    internal static Func<Sole, double[]> GetAlgorithmMethod(SoleSolvingAlgorithmSerial solvingAlgorithm) =>
        solvingAlgorithm switch
        {
            _ => throw new NotImplementedException("This solving algorithm has not yet been implemented.")
        };
}