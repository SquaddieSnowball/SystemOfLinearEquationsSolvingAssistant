﻿using SystemOfLinearEquationsSolvingAssistant.BL.SystemOfLinearEquationsSolving.Entities;

namespace SystemOfLinearEquationsSolvingAssistant.BL.SystemOfLinearEquationsSolving.Solvers;

public interface ISoleSolver
{
    SoleSolvingResults SolveSerial(Sole sole, SoleSolvingAlgorithmSerial solvingAlgorithm);

    SoleSolvingResults SolveParallel(Sole sole, SoleSolvingAlgorithmParallel solvingAlgorithm, int numberOfThreads);

    Task<SoleSolvingResults> SolveSerialAsync(Sole sole, SoleSolvingAlgorithmSerial solvingAlgorithm);

    Task<SoleSolvingResults> SolveParallelAsync(Sole sole, SoleSolvingAlgorithmParallel solvingAlgorithm, int numberOfThreads);
}