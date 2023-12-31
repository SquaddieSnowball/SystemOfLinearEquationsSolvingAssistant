using SystemOfLinearEquationsSolvingAssistant.BL.Services.Entities.SoleSolver;
using SystemOfLinearEquationsSolvingAssistant.ViewModels.Services.Abstractions;

namespace SystemOfLinearEquationsSolvingAssistant.ViewModels.Services.Implementations;

/// <summary>
/// Provides methods used to manage algorithms for solving systems of linear equations.
/// </summary>
public sealed class SoleSolvingAlgorithmNameService : ISoleSolvingAlgorithmNameService
{
    private readonly Dictionary<SoleSolvingAlgorithmSerial, string> _algorithmValueNamesSerial = new();
    private readonly Dictionary<SoleSolvingAlgorithmParallel, string> _algorithmValueNamesParallel = new();

    /// <summary>
    /// Gets the names of serial algorithms.
    /// </summary>
    public IEnumerable<string> AlgorithmNamesSerial => _algorithmValueNamesSerial.Values;

    /// <summary>
    /// Gets the names of parallel algorithms.
    /// </summary>
    public IEnumerable<string> AlgorithmNamesParallel => _algorithmValueNamesParallel.Values;

    /// <summary>
    /// Initializes a new instance of <see cref="SoleSolvingAlgorithmNameService"/>.
    /// </summary>
    public SoleSolvingAlgorithmNameService()
    {
        AddAlgorithmNamesSerial();
        AddAlgorithmNamesParallel();
    }

    /// <summary>
    /// Gets the name of the serial algorithm.
    /// </summary>
    /// <param name="solvingAlgorithm">Algorithm to get the name for.</param>
    /// <returns>Algorithm name.</returns>
    /// <exception cref="ArgumentException"></exception>
    public string GetAlgorithmNameSerial(SoleSolvingAlgorithmSerial solvingAlgorithm) =>
        (_algorithmValueNamesSerial.ContainsKey(solvingAlgorithm) is true) ?
        _algorithmValueNamesSerial[solvingAlgorithm] :
        throw new ArgumentException("The specified algorithm does not have a registered name.", nameof(solvingAlgorithm));

    /// <summary>
    /// Gets the name of the parallel algorithm.
    /// </summary>
    /// <param name="solvingAlgorithm">Algorithm to get the name for.</param>
    /// <returns>Algorithm name.</returns>
    /// <exception cref="ArgumentException"></exception>
    public string GetAlgorithmNameParallel(SoleSolvingAlgorithmParallel solvingAlgorithm) =>
        (_algorithmValueNamesParallel.ContainsKey(solvingAlgorithm) is true) ?
        _algorithmValueNamesParallel[solvingAlgorithm] :
        throw new ArgumentException("The specified algorithm does not have a registered name.", nameof(solvingAlgorithm));

    /// <summary>
    /// Gets the serial algorithm by name.
    /// </summary>
    /// <param name="solvingAlgorithmName">Algorithm name.</param>
    /// <returns>Solving algorithm.</returns>
    /// <exception cref="ArgumentException"></exception>
    public SoleSolvingAlgorithmSerial GetAlgorithmByNameSerial(string solvingAlgorithmName) =>
        (_algorithmValueNamesSerial.ContainsValue(solvingAlgorithmName) is true) ?
        _algorithmValueNamesSerial.First(vn => vn.Value.Equals(solvingAlgorithmName, StringComparison.Ordinal) is true).Key :
        throw new ArgumentException("The algorithm with the specified name does not exist.", nameof(solvingAlgorithmName));

    /// <summary>
    /// Gets the parallel algorithm by name.
    /// </summary>
    /// <param name="solvingAlgorithmName">Algorithm name.</param>
    /// <returns>Solving algorithm.</returns>
    /// <exception cref="ArgumentException"></exception>
    public SoleSolvingAlgorithmParallel GetAlgorithmByNameParallel(string solvingAlgorithmName) =>
        (_algorithmValueNamesParallel.ContainsValue(solvingAlgorithmName) is true) ?
        _algorithmValueNamesParallel.First(vn => vn.Value.Equals(solvingAlgorithmName, StringComparison.Ordinal) is true).Key :
        throw new ArgumentException("The algorithm with the specified name does not exist.", nameof(solvingAlgorithmName));

    private void AddAlgorithmNamesSerial()
    {
        _algorithmValueNamesSerial
            .Add(SoleSolvingAlgorithmSerial.GaussianElimination, "Gaussian elimination");
    }

    private void AddAlgorithmNamesParallel()
    {
        _algorithmValueNamesParallel
            .Add(SoleSolvingAlgorithmParallel.GaussianEliminationCyclicMapping, "Gaussian elimination (cyclic mapping)");
    }
}