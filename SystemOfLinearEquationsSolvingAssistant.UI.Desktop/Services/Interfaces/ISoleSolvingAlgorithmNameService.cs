using System.Collections.Generic;
using SystemOfLinearEquationsSolvingAssistant.BL.SystemOfLinearEquationsSolving.Entities;

namespace SystemOfLinearEquationsSolvingAssistant.UI.Desktop.Services.Interfaces;

internal interface ISoleSolvingAlgorithmNameService
{
    IEnumerable<string> AlgorithmNamesSerial { get; }

    IEnumerable<string> AlgorithmNamesParallel { get; }

    string GetAlgorithmNameSerial(SoleSolvingAlgorithmSerial solvingAlgorithm);

    string GetAlgorithmNameParallel(SoleSolvingAlgorithmParallel solvingAlgorithm);

    SoleSolvingAlgorithmSerial GetAlgorithmByNameSerial(string solvingAlgorithmName);

    SoleSolvingAlgorithmParallel GetAlgorithmByNameParallel(string solvingAlgorithmName);
}