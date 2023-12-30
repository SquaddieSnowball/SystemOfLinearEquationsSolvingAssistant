using SystemOfLinearEquationsSolvingAssistant.BL.Entities;
using SystemOfLinearEquationsSolvingAssistant.BL.Services.Abstractions;
using SystemOfLinearEquationsSolvingAssistant.BL.Services.Entities.SoleSolver;
using SystemOfLinearEquationsSolvingAssistant.BL.Services.Implementations;
using SystemOfLinearEquationsSolvingAssistant.Dependencies;
using SystemOfLinearEquationsSolvingAssistant.Dependencies.Entities;

namespace SystemOfLinearEquationsSolvingAssistant.BL.UnitTests.Services;

[TestClass]
public sealed class SoleSolverTests
{
    [ClassInitialize]
    public static void ClassInitialize(TestContext testContext)
    {
        DependenciesContainer.Register<ISoleSolver, SoleSolver>(DependencyObjectLifetime.Singleton);
    }

    [TestMethod]
    public void SolveSerial_GaussianElimination_ReturnsCorrectSolutionSet()
    {
        ISoleSolver soleSolver = CreateSoleSolver();
        double[,] a = new double[,]
        {
            { 3, 2, 1 },
            { 1, 2, 1 },
            { 4, 3, -2 }
        };
        double[] b = new double[] { 10, 8, 4 };
        double[] expected = new double[] { 1, 2, 3 };

        double[] actual = soleSolver
            .SolveSerial(new Sole(a, b), SoleSolvingAlgorithmSerial.GaussianElimination)
            .SolutionSet
            .Select(n => Math.Round(n))
            .ToArray();

        CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void SolveParallel_GaussianEliminationCyclicMapping_ReturnsCorrectSolutionSet()
    {
        ISoleSolver soleSolver = CreateSoleSolver();
        double[,] a = new double[,]
        {
            { 3, 2, 1 },
            { 1, 2, 1 },
            { 4, 3, -2 }
        };
        double[] b = new double[] { 10, 8, 4 };
        double[] expected = new double[] { 1, 2, 3 };

        double[] actual = soleSolver
            .SolveParallel(new Sole(a, b), SoleSolvingAlgorithmParallel.GaussianEliminationCyclicMapping, 2)
            .SolutionSet
            .Select(n => Math.Round(n))
            .ToArray();

        CollectionAssert.AreEqual(expected, actual);
    }

    private static ISoleSolver CreateSoleSolver() => DependenciesContainer.Resolve<ISoleSolver>()!;
}