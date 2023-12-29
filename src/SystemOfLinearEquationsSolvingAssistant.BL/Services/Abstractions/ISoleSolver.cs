using SystemOfLinearEquationsSolvingAssistant.BL.Entities;
using SystemOfLinearEquationsSolvingAssistant.BL.Services.Entities.SoleSolver;

namespace SystemOfLinearEquationsSolvingAssistant.BL.Services.Abstractions;

public interface ISoleSolver
{
    SoleSolvingResults SolveSerial(Sole sole, SoleSolvingAlgorithmSerial solvingAlgorithm);

    SoleSolvingResults SolveParallel(Sole sole, SoleSolvingAlgorithmParallel solvingAlgorithm, int numberOfThreads);

    Task<SoleSolvingResults> SolveSerialAsync(Sole sole, SoleSolvingAlgorithmSerial solvingAlgorithm);

    Task<SoleSolvingResults> SolveParallelAsync(Sole sole, SoleSolvingAlgorithmParallel solvingAlgorithm, int numberOfThreads);
}