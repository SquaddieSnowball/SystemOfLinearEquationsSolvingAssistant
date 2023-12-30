using System.Diagnostics;
using SystemOfLinearEquationsSolvingAssistant.BL.Entities;
using SystemOfLinearEquationsSolvingAssistant.BL.Helpers.SoleSolver;
using SystemOfLinearEquationsSolvingAssistant.BL.Services.Abstractions;
using SystemOfLinearEquationsSolvingAssistant.BL.Services.Entities.SoleSolver;

namespace SystemOfLinearEquationsSolvingAssistant.BL.Services.Implementations;

public sealed class SoleSolver : ISoleSolver
{
    public SoleSolvingResults SolveSerial(Sole sole, SoleSolvingAlgorithmSerial solvingAlgorithm)
    {
        Func<Sole, double[]> solvingAlgorithmMethod = SoleSolverAlgorithmsSerial.GetAlgorithmMethod(solvingAlgorithm);
        Stopwatch stopwatch = new();
        double[] solutionSet;

        try
        {
            stopwatch.Start();
            solutionSet = solvingAlgorithmMethod(sole);
        }
        finally
        {
            stopwatch.Stop();
        }

        return new SoleSolvingResults(solutionSet, stopwatch.Elapsed);
    }

    public SoleSolvingResults SolveParallel(Sole sole,
        SoleSolvingAlgorithmParallel solvingAlgorithm, int numberOfThreads)
    {
        Func<Sole, int, double[]> solvingAlgorithmMethod = SoleSolverAlgorithmsParallel.GetAlgorithmMethod(solvingAlgorithm);
        Stopwatch stopwatch = new();
        double[] solutionSet;

        try
        {
            stopwatch.Start();
            solutionSet = solvingAlgorithmMethod(sole, numberOfThreads);
        }
        finally
        {
            stopwatch.Stop();
        }

        return new SoleSolvingResults(solutionSet, stopwatch.Elapsed);
    }

    public async Task<SoleSolvingResults> SolveSerialAsync(Sole sole, SoleSolvingAlgorithmSerial solvingAlgorithm)
    {
        Func<Sole, double[]> solvingAlgorithmMethod = SoleSolverAlgorithmsSerial.GetAlgorithmMethod(solvingAlgorithm);
        Stopwatch stopwatch = new();
        double[] solutionSet;

        try
        {
            stopwatch.Start();
            solutionSet = await Task.Run(() => solvingAlgorithmMethod(sole));
        }
        finally
        {
            stopwatch.Stop();
        }

        return new SoleSolvingResults(solutionSet, stopwatch.Elapsed);
    }

    public async Task<SoleSolvingResults> SolveParallelAsync(Sole sole,
        SoleSolvingAlgorithmParallel solvingAlgorithm, int numberOfThreads)
    {
        Func<Sole, int, double[]> solvingAlgorithmMethod = SoleSolverAlgorithmsParallel.GetAlgorithmMethod(solvingAlgorithm);
        Stopwatch stopwatch = new();
        double[] solutionSet;

        try
        {
            stopwatch.Start();
            solutionSet = await Task.Run(() => solvingAlgorithmMethod(sole, numberOfThreads));
        }
        finally
        {
            stopwatch.Stop();
        }

        return new SoleSolvingResults(solutionSet, stopwatch.Elapsed);
    }
}