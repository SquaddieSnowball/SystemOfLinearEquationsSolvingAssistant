using SystemOfLinearEquationsSolvingAssistant.BL.SystemOfLinearEquationsSolving.Entities;

namespace SystemOfLinearEquationsSolvingAssistant.ViewModels.Services.Interfaces;

public interface ISoleSolvingAlgorithmNameService
{
    IEnumerable<string> AlgorithmNamesSerial { get; }

    IEnumerable<string> AlgorithmNamesParallel { get; }

    string GetAlgorithmNameSerial(SoleSolvingAlgorithmSerial solvingAlgorithm);

    string GetAlgorithmNameParallel(SoleSolvingAlgorithmParallel solvingAlgorithm);

    SoleSolvingAlgorithmSerial GetAlgorithmByNameSerial(string solvingAlgorithmName);

    SoleSolvingAlgorithmParallel GetAlgorithmByNameParallel(string solvingAlgorithmName);
}