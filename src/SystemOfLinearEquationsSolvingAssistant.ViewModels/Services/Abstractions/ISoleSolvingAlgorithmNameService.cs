using SystemOfLinearEquationsSolvingAssistant.BL.Services.Entities.SoleSolver;

namespace SystemOfLinearEquationsSolvingAssistant.ViewModels.Services.Abstractions;

/// <summary>
/// Provides methods used to manage algorithms for solving systems of linear equations.
/// </summary>
public interface ISoleSolvingAlgorithmNameService
{
    /// <summary>
    /// Gets the names of serial algorithms.
    /// </summary>
    IEnumerable<string> AlgorithmNamesSerial { get; }

    /// <summary>
    /// Gets the names of parallel algorithms.
    /// </summary>
    IEnumerable<string> AlgorithmNamesParallel { get; }

    /// <summary>
    /// Gets the name of the serial algorithm.
    /// </summary>
    /// <param name="solvingAlgorithm">Algorithm to get the name for.</param>
    /// <returns>Algorithm name.</returns>
    string GetAlgorithmNameSerial(SoleSolvingAlgorithmSerial solvingAlgorithm);

    /// <summary>
    /// Gets the name of the parallel algorithm.
    /// </summary>
    /// <param name="solvingAlgorithm">Algorithm to get the name for.</param>
    /// <returns>Algorithm name.</returns>
    string GetAlgorithmNameParallel(SoleSolvingAlgorithmParallel solvingAlgorithm);

    /// <summary>
    /// Gets the serial algorithm by name.
    /// </summary>
    /// <param name="solvingAlgorithmName">Algorithm name.</param>
    /// <returns>Solving algorithm.</returns>
    SoleSolvingAlgorithmSerial GetAlgorithmByNameSerial(string solvingAlgorithmName);

    /// <summary>
    /// Gets the parallel algorithm by name.
    /// </summary>
    /// <param name="solvingAlgorithmName">Algorithm name.</param>
    /// <returns>Solving algorithm.</returns>
    SoleSolvingAlgorithmParallel GetAlgorithmByNameParallel(string solvingAlgorithmName);
}