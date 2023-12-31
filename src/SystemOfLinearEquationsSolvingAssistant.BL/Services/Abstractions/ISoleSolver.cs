using SystemOfLinearEquationsSolvingAssistant.BL.Entities;
using SystemOfLinearEquationsSolvingAssistant.BL.Services.Entities.SoleSolver;

namespace SystemOfLinearEquationsSolvingAssistant.BL.Services.Abstractions;

/// <summary>
/// Provides methods used to solve systems of linear equations.
/// </summary>
public interface ISoleSolver
{
    /// <summary>
    /// Solves a system of linear equations using serial algorithm.
    /// </summary>
    /// <param name="sole">A system of linear equations to solve.</param>
    /// <param name="solvingAlgorithm">The algorithm that will be used during the solving.</param>
    /// <returns>A new instance of the <see cref="SoleSolvingResults"/>.</returns>
    SoleSolvingResults SolveSerial(Sole sole, SoleSolvingAlgorithmSerial solvingAlgorithm);

    /// <summary>
    /// Solves a system of linear equations using parallel algorithm.
    /// </summary>
    /// <param name="sole">A system of linear equations to solve.</param>
    /// <param name="solvingAlgorithm">The algorithm that will be used during the solving.</param>
    /// <param name="numberOfThreads">The number of threads that will be used during the solving.</param>
    /// <returns>A new instance of the <see cref="SoleSolvingResults"/>.</returns>
    SoleSolvingResults SolveParallel(Sole sole, SoleSolvingAlgorithmParallel solvingAlgorithm, int numberOfThreads);

    /// <summary>
    /// Asynchronously solves a system of linear equations using serial algorithm.
    /// </summary>
    /// <param name="sole">A system of linear equations to solve.</param>
    /// <param name="solvingAlgorithm">The algorithm that will be used during the solving.</param>
    /// <returns>A task representing an asynchronous operation 
    /// that wraps a new <see cref="SoleSolvingResults"/> instance.</returns>
    Task<SoleSolvingResults> SolveSerialAsync(Sole sole, SoleSolvingAlgorithmSerial solvingAlgorithm);

    /// <summary>
    /// Asynchronously solves a system of linear equations using parallel algorithm.
    /// </summary>
    /// <param name="sole">A system of linear equations to solve.</param>
    /// <param name="solvingAlgorithm">The algorithm that will be used during the solving.</param>
    /// <param name="numberOfThreads">The number of threads that will be used during the solving.</param>
    /// <returns>A task representing an asynchronous operation 
    /// that wraps a new <see cref="SoleSolvingResults"/> instance.</returns>
    Task<SoleSolvingResults> SolveParallelAsync(Sole sole, SoleSolvingAlgorithmParallel solvingAlgorithm, int numberOfThreads);
}