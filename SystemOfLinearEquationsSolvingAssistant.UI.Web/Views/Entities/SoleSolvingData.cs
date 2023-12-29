namespace SystemOfLinearEquationsSolvingAssistant.UI.Web.Views.Entities;

internal sealed class SoleSolvingData
{
    public string[][] A { get; set; } = default!;

    public string[] B { get; set; } = default!;

    public string SolvingAlgorithm { get; set; } = default!;

    public int? ThreadsNum { get; set; }
}