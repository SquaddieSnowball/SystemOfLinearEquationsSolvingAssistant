namespace SystemOfLinearEquationsSolvingAssistant.BL.Entities;

/// <summary>
/// Represents a system of linear equations.
/// </summary>
public sealed class Sole : ICloneable
{
    /// <summary>
    /// Gets matrix A of the system of linear equations.
    /// </summary>
    public double[,] A { get; }

    /// <summary>
    /// Gets vector B of the system of linear equations.
    /// </summary>
    public double[] B { get; }

    /// <summary>
    /// Gets the dimension of the system of linear equations.
    /// </summary>
    public int Dimension { get; }

    /// <summary>
    /// Initializes a new instance of <see cref="Sole"/> with the specified matrix A and vector B.
    /// </summary>
    /// <param name="a">Matrix A of the system of linear equations.</param>
    /// <param name="b">Vector B of the system of linear equations.</param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentException"></exception>
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

        (A, B, Dimension) = (a, b, a.GetLength(0));
    }

    /// <summary>
    /// Creates a new <see cref="Sole"/> that is a copy of the current instance.
    /// </summary>
    /// <returns>A new <see cref="Sole"/> that is a copy of this instance.</returns>
    public object Clone() => new Sole((double[,])A.Clone(), (double[])B.Clone());
}