using System;
using System.Collections.Generic;
using System.Linq;
using SystemOfLinearEquationsSolvingAssistant.BL.SystemOfLinearEquationsSolving.Entities;
using SystemOfLinearEquationsSolvingAssistant.UI.Desktop.Services.Interfaces;

namespace SystemOfLinearEquationsSolvingAssistant.UI.Desktop.Services.Implementations;

internal sealed class SoleSolvingAlgorithmNameService : ISoleSolvingAlgorithmNameService
{
    private readonly Dictionary<SoleSolvingAlgorithmSerial, string> _algorithmValueNamesSerial = new();
    private readonly Dictionary<SoleSolvingAlgorithmParallel, string> _algorithmValueNamesParallel = new();

    public IEnumerable<string> AlgorithmNamesSerial => _algorithmValueNamesSerial.Values;

    public IEnumerable<string> AlgorithmNamesParallel => _algorithmValueNamesParallel.Values;

    public SoleSolvingAlgorithmNameService()
    {
        AddAlgorithmNamesSerial();
        AddAlgorithmNamesParallel();
    }

    public string GetAlgorithmNameSerial(SoleSolvingAlgorithmSerial solvingAlgorithm) =>
        (_algorithmValueNamesSerial.ContainsKey(solvingAlgorithm) is true) ?
        _algorithmValueNamesSerial[solvingAlgorithm] :
        throw new ArgumentException("The specified algorithm does not have a registered name.", nameof(solvingAlgorithm));

    public string GetAlgorithmNameParallel(SoleSolvingAlgorithmParallel solvingAlgorithm) =>
        (_algorithmValueNamesParallel.ContainsKey(solvingAlgorithm) is true) ?
        _algorithmValueNamesParallel[solvingAlgorithm] :
        throw new ArgumentException("The specified algorithm does not have a registered name.", nameof(solvingAlgorithm));

    public SoleSolvingAlgorithmSerial GetAlgorithmByNameSerial(string solvingAlgorithmName) =>
        (_algorithmValueNamesSerial.ContainsValue(solvingAlgorithmName) is true) ?
        _algorithmValueNamesSerial.First(vn => vn.Value.Equals(solvingAlgorithmName, StringComparison.Ordinal) is true).Key :
        throw new ArgumentException("The algorithm with the specified name does not exist.", nameof(solvingAlgorithmName));

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