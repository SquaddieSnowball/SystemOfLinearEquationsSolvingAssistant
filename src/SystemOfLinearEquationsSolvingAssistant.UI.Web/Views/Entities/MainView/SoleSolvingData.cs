namespace SystemOfLinearEquationsSolvingAssistant.UI.Web.Views.Entities.MainView;

/// <summary>
/// Represents a type used to deserialize JSON data to solve a system of linear equations.
/// </summary>
internal sealed class SoleSolvingData
{
    /// <summary>
    /// Gets or sets the data of matrix A.
    /// </summary>
    public string[][] A { get; set; } = default!;

    /// <summary>
    /// Gets or sets the data of vector B.
    /// </summary>
    public string[] B { get; set; } = default!;

    /// <summary>
    /// Gets or sets solving algorithm data.
    /// </summary>
    public string SolvingAlgorithm { get; set; } = default!;

    /// <summary>
    /// Gets or sets the number of threads data.
    /// </summary>
    public int? ThreadsNum { get; set; }
}