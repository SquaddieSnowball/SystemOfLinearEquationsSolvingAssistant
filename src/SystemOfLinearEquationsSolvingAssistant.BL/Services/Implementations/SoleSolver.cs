using System.Diagnostics;
using SystemOfLinearEquationsSolvingAssistant.BL.Entities;
using SystemOfLinearEquationsSolvingAssistant.BL.Helpers.SoleSolver;
using SystemOfLinearEquationsSolvingAssistant.BL.Services.Abstractions;
using SystemOfLinearEquationsSolvingAssistant.BL.Services.Entities.SoleSolver;

namespace SystemOfLinearEquationsSolvingAssistant.BL.Services.Implementations;

/// <summary>
/// Provides methods used to solve systems of linear equations.
/// </summary>
public sealed class SoleSolver : ISoleSolver
{
    /// <summary>
    /// Solves a system of linear equations using serial algorithm.
    /// </summary>
    /// <param name="sole">A system of linear equations to solve.</param>
    /// <param name="solvingAlgorithm">The algorithm that will be used during the solving.</param>
    /// <returns>A new instance of the <see cref="SoleSolvingResults"/>.</returns>
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

    /// <summary>
    /// Solves a system of linear equations using parallel algorithm.
    /// </summary>
    /// <param name="sole">A system of linear equations to solve.</param>
    /// <param name="solvingAlgorithm">The algorithm that will be used during the solving.</param>
    /// <param name="numberOfThreads">The number of threads that will be used during the solving.</param>
    /// <returns>A new instance of the <see cref="SoleSolvingResults"/>.</returns>
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

    /// <summary>
    /// Asynchronously solves a system of linear equations using serial algorithm.
    /// </summary>
    /// <param name="sole">A system of linear equations to solve.</param>
    /// <param name="solvingAlgorithm">The algorithm that will be used during the solving.</param>
    /// <returns>A task representing an asynchronous operation 
    /// that wraps a new <see cref="SoleSolvingResults"/> instance.</returns>
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

    /// <summary>
    /// Asynchronously solves a system of linear equations using parallel algorithm.
    /// </summary>
    /// <param name="sole">A system of linear equations to solve.</param>
    /// <param name="solvingAlgorithm">The algorithm that will be used during the solving.</param>
    /// <param name="numberOfThreads">The number of threads that will be used during the solving.</param>
    /// <returns>A task representing an asynchronous operation 
    /// that wraps a new <see cref="SoleSolvingResults"/> instance.</returns>
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