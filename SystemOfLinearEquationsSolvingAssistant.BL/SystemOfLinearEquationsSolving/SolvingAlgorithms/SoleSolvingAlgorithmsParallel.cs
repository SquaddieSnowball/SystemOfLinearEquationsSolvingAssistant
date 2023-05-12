using SystemOfLinearEquationsSolvingAssistant.BL.SystemOfLinearEquationsSolving.Entities;
using SystemOfLinearEquationsSolvingAssistant.BL.SystemOfLinearEquationsSolving.SolvingAlgorithms.Helpers;

namespace SystemOfLinearEquationsSolvingAssistant.BL.SystemOfLinearEquationsSolving.SolvingAlgorithms;

internal static class SoleSolvingAlgorithmsParallel
{
    public static Func<Sole, int, double[]> GetAlgorithmMethod(SoleSolvingAlgorithmParallel solvingAlgorithm) =>
        solvingAlgorithm switch
        {
            SoleSolvingAlgorithmParallel.GaussianEliminationCyclicMapping => GaussianEliminationCyclicMapping,
            _ => throw new NotImplementedException("This solving algorithm has not yet been implemented.")
        };

    private static double[] GaussianEliminationCyclicMapping(Sole sole, int numberOfThreads)
    {
        static void PerformElimintaions(Sole sole, ref int p, int numberOfThreads,
            Barrier barrier, CancellationToken cancellationToken)
        {
            int threadNumber = int.Parse(Thread.CurrentThread.Name!);

            barrier.SignalAndWait(CancellationToken.None);

            while ((p < sole.Dimension - 1) && (cancellationToken.IsCancellationRequested is false))
            {
                for (var r = p + 1; r < sole.Dimension; r++)
                    if ((r % numberOfThreads) == threadNumber)
                        if (sole.A[r, p] is not 0)
                        {
                            double coefficient = sole.A[r, p] / sole.A[p, p];

                            for (var c = p; c < sole.Dimension; c++)
                                sole.A[r, c] -= sole.A[p, c] * coefficient;

                            sole.B[r] -= sole.B[p] * coefficient;
                        }

                barrier.SignalAndWait(CancellationToken.None);
            }
        }

        try
        {
            CheckSolvingAlgorithmArguments(sole, numberOfThreads);
        }
        catch
        {
            throw;
        }

        Sole soleClone = (Sole)sole.Clone();
        int p = default;
        Barrier barrier = new(numberOfThreads + 1);
        CancellationTokenSource cancellationTokenSource = new();

        Thread[] threads = new Thread[numberOfThreads];

        for (var t = 0; t < numberOfThreads; t++)
        {
            threads[t] = new Thread(() =>
                PerformElimintaions(soleClone, ref p, numberOfThreads, barrier, cancellationTokenSource.Token))
            {
                Name = t.ToString()
            };

            threads[t].Start();
        }

        for (p = 0; p < soleClone.Dimension - 1; p++)
        {
            int pivotRow = p;

            for (var r = p + 1; r < soleClone.Dimension; r++)
                if (Math.Abs(soleClone.A[r, p]) > Math.Abs(soleClone.A[pivotRow, p]))
                    pivotRow = r;

            if (soleClone.A[pivotRow, p] is 0)
            {
                cancellationTokenSource.Cancel();
                barrier.SignalAndWait();

                foreach (Thread thread in threads)
                    thread.Join();

                return Array.Empty<double>();
            }

            if (pivotRow != p)
            {
                for (var c = p; c < soleClone.Dimension; c++)
                    (soleClone.A[pivotRow, c], soleClone.A[p, c]) = (soleClone.A[p, c], soleClone.A[pivotRow, c]);

                (soleClone.B[pivotRow], soleClone.B[p]) = (soleClone.B[p], soleClone.B[pivotRow]);
            }

            barrier.SignalAndWait();
            barrier.ParticipantsRemainingWait(1);
        }

        barrier.SignalAndWait();

        foreach (Thread thread in threads)
            thread.Join();

        double[] solutionSet = new double[soleClone.Dimension];

        for (p = soleClone.Dimension - 1; p >= 0; p--)
        {
            if (soleClone.A[p, p] is 0)
                return Array.Empty<double>();

            solutionSet[p] = soleClone.B[p];

            for (var c = soleClone.Dimension - 1; c > p; c--)
                solutionSet[p] -= soleClone.A[p, c] * solutionSet[c];

            solutionSet[p] /= soleClone.A[p, p];
        }

        return solutionSet;
    }

    private static void CheckSolvingAlgorithmArguments(Sole sole, int numberOfThreads)
    {
        if (sole is null)
            throw new ArgumentNullException(nameof(sole), "System of linear equations must not be null.");

        if (numberOfThreads < 2)
            throw new ArgumentException("Number of threads must be greater than 1.", nameof(numberOfThreads));
    }
}