namespace SystemOfLinearEquationsSolvingAssistant.BL.SystemOfLinearEquationsSolving.Entities;

public sealed class Sole : ICloneable
{
    public double[,] A { get; }

    public double[] B { get; }

    public int Dimension { get; }

    public Sole(double[,] a, double[] b)
    {
        if (a is null)
            throw new ArgumentNullException(nameof(a), "Matrix A must not be null.");

        if (b is null)
            throw new ArgumentNullException(nameof(b), "Vector B must not be null.");

        if (a.GetLength(0) != a.GetLength(1))
            throw new ArgumentException("Matrix A must be square.", nameof(a));

        if (a.GetLength(0) != b.Length)
            throw new ArgumentException("The dimension of the matrix A must be equal to the dimension of the vector B.");

        (A, B, Dimension) = (a, b, b.Length);
    }

    public object Clone() =>
        new Sole((double[,])A.Clone(), (double[])B.Clone());
}