using SystemOfLinearEquationsSolvingAssistant.BL.Services.Entities.SoleSolver;

namespace SystemOfLinearEquationsSolvingAssistant.ViewModels.Services.Abstractions;

public interface ISoleSolvingAlgorithmNameService
{
    IEnumerable<string> AlgorithmNamesSerial { get; }

    IEnumerable<string> AlgorithmNamesParallel { get; }

    string GetAlgorithmNameSerial(SoleSolvingAlgorithmSerial solvingAlgorithm);

    string GetAlgorithmNameParallel(SoleSolvingAlgorithmParallel solvingAlgorithm);

    SoleSolvingAlgorithmSerial GetAlgorithmByNameSerial(string solvingAlgorithmName);

    SoleSolvingAlgorithmParallel GetAlgorithmByNameParallel(string solvingAlgorithmName);
}