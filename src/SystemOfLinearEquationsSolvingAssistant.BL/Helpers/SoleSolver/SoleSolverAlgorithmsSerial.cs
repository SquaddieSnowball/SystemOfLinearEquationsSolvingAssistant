using SystemOfLinearEquationsSolvingAssistant.BL.Entities;
using SystemOfLinearEquationsSolvingAssistant.BL.Services.Entities.SoleSolver;

namespace SystemOfLinearEquationsSolvingAssistant.BL.Helpers.SoleSolver;

/// <summary>
/// Provides methods used for serial solving of the systems of linear equations.
/// </summary>
internal static class SoleSolverAlgorithmsSerial
{
    /// <summary>
    /// Gets a delegate containing the specified algorithm method.
    /// </summary>
    /// <param name="solvingAlgorithm">Algorithm for obtaining a delegate containing a method.</param>
    /// <returns>A delegate containing the specified algorithm method.</returns>
    /// <exception cref="NotImplementedException"></exception>
    public static Func<Sole, double[]> GetAlgorithmMethod(SoleSolvingAlgorithmSerial solvingAlgorithm) =>
        solvingAlgorithm switch
        {
            SoleSolvingAlgorithmSerial.GaussianElimination => GaussianElimination,
            _ => throw new NotImplementedException("This solving algorithm has not yet been implemented.")
        };

    private static double[] GaussianElimination(Sole sole)
    {
        ValidateAlgorithmArguments(sole);

        Sole soleClone = (Sole)sole.Clone();

        for (var p = 0; p < soleClone.Dimension - 1; p++)
        {
            int pivotRow = p;

            for (var r = p + 1; r < soleClone.Dimension; r++)
            {
                if (Math.Abs(soleClone.A[r, p]) > Math.Abs(soleClone.A[pivotRow, p]))
                    pivotRow = r;
            }

            if (soleClone.A[pivotRow, p] is 0)
                return Array.Empty<double>();

            if (pivotRow != p)
            {
                for (var c = p; c < soleClone.Dimension; c++)
                    (soleClone.A[pivotRow, c], soleClone.A[p, c]) = (soleClone.A[p, c], soleClone.A[pivotRow, c]);

                (soleClone.B[pivotRow], soleClone.B[p]) = (soleClone.B[p], soleClone.B[pivotRow]);
            }

            for (var r = p + 1; r < soleClone.Dimension; r++)
            {
                if (soleClone.A[r, p] is not 0)
                {
                    double coefficient = soleClone.A[r, p] / soleClone.A[p, p];

                    for (var c = p; c < soleClone.Dimension; c++)
                        soleClone.A[r, c] -= soleClone.A[p, c] * coefficient;

                    soleClone.B[r] -= soleClone.B[p] * coefficient;
                }
            }
        }

        double[] solutionSet = new double[soleClone.Dimension];

        for (var p = soleClone.Dimension - 1; p >= 0; p--)
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

    private static void ValidateAlgorithmArguments(Sole sole)
    {
        if (sole is null)
            throw new ArgumentNullException(nameof(sole), "System of linear equations must not be null.");
    }
}