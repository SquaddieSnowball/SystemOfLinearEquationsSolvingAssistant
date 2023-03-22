using System.Diagnostics;
using SystemOfLinearEquationsSolvingAssistant.BL.SystemOfLinearEquationsSolving.Entities;
using SystemOfLinearEquationsSolvingAssistant.BL.SystemOfLinearEquationsSolving.SolvingAlgorithms;

namespace SystemOfLinearEquationsSolvingAssistant.BL.SystemOfLinearEquationsSolving.Solvers.Implementations;

public sealed class SoleSolver : ISoleSolver
{
    public SoleSolvingResults SolveSerial(Sole sole, SoleSolvingAlgorithmSerial solvingAlgorithm)
    {
        Func<Sole, double[]> solvingAlgorithmMethod;

        try
        {
            solvingAlgorithmMethod = SoleSolvingAlgorithmsSerial.GetAlgorithmMethod(solvingAlgorithm);
        }
        catch
        {
            throw;
        }

        Stopwatch stopwatch = new();
        double[] solutionSet;

        try
        {
            stopwatch.Start();
            solutionSet = solvingAlgorithmMethod(sole);
        }
        catch
        {
            throw;
        }
        finally
        {
            stopwatch.Stop();
        }

        return new SoleSolvingResults(solutionSet, stopwatch.Elapsed);
    }

    public SoleSolvingResults SolveParallel(Sole sole, SoleSolvingAlgorithmParallel solvingAlgorithm,
        int numberOfThreads)
    {
        Func<Sole, int, double[]> solvingAlgorithmMethod;

        try
        {
            solvingAlgorithmMethod = SoleSolvingAlgorithmsParallel.GetAlgorithmMethod(solvingAlgorithm);
        }
        catch
        {
            throw;
        }

        Stopwatch stopwatch = new();
        double[] solutionSet;

        try
        {
            stopwatch.Start();
            solutionSet = solvingAlgorithmMethod(sole, numberOfThreads);
        }
        catch
        {
            throw;
        }
        finally
        {
            stopwatch.Stop();
        }

        return new SoleSolvingResults(solutionSet, stopwatch.Elapsed);
    }

    public async Task<SoleSolvingResults> SolveSerialAsync(Sole sole, SoleSolvingAlgorithmSerial solvingAlgorithm)
    {
        Func<Sole, double[]> solvingAlgorithmMethod;

        try
        {
            solvingAlgorithmMethod = SoleSolvingAlgorithmsSerial.GetAlgorithmMethod(solvingAlgorithm);
        }
        catch
        {
            throw;
        }

        Stopwatch stopwatch = new();
        double[] solutionSet;

        try
        {
            stopwatch.Start();
            solutionSet = await Task.Run(() => solvingAlgorithmMethod(sole));
        }
        catch
        {
            throw;
        }
        finally
        {
            stopwatch.Stop();
        }

        return new SoleSolvingResults(solutionSet, stopwatch.Elapsed);
    }

    public async Task<SoleSolvingResults> SolveParallelAsync(Sole sole, SoleSolvingAlgorithmParallel solvingAlgorithm,
        int numberOfThreads)
    {
        Func<Sole, int, double[]> solvingAlgorithmMethod;

        try
        {
            solvingAlgorithmMethod = SoleSolvingAlgorithmsParallel.GetAlgorithmMethod(solvingAlgorithm);
        }
        catch
        {
            throw;
        }

        Stopwatch stopwatch = new();
        double[] solutionSet;

        try
        {
            stopwatch.Start();
            solutionSet = await Task.Run(() => solvingAlgorithmMethod(sole, numberOfThreads));
        }
        catch
        {
            throw;
        }
        finally
        {
            stopwatch.Stop();
        }

        return new SoleSolvingResults(solutionSet, stopwatch.Elapsed);
    }
}