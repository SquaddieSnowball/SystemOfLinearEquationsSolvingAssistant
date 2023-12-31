using SystemOfLinearEquationsSolvingAssistant.BL.Entities;
using SystemOfLinearEquationsSolvingAssistant.BL.Services.Entities.SoleParser;

namespace SystemOfLinearEquationsSolvingAssistant.BL.Services.Abstractions;

/// <summary>
/// Provides methods used to parse systems of linear equations.
/// </summary>
public interface ISoleParser
{
    /// <summary>
    /// Initializes the system of linear equations parser.
    /// </summary>
    /// <param name="initializationString">Initialization string.</param>
    void Initialize(string initializationString);

    /// <summary>
    /// Parses the system of linear equations.
    /// </summary>
    /// <param name="pathA">Path to matrix A of a system of linear equations.</param>
    /// <param name="pathB">Path to vector B of a system of linear equations.</param>
    /// <param name="parsingTemplate">A template that will be used during the parsing process.</param>
    /// <returns>A new instance of the <see cref="Sole"/>.</returns>
    Sole Parse(string pathA, string pathB, SoleParsingTemplate parsingTemplate);

    /// <summary>
    /// Asynchronously parses a system of linear equations.
    /// </summary>
    /// <param name="pathA">Path to matrix A of a system of linear equations.</param>
    /// <param name="pathB">Path to vector B of a system of linear equations.</param>
    /// <param name="parsingTemplate">A template that will be used during the parsing process.</param>
    /// <returns>A task representing an asynchronous operation that wraps a new <see cref="Sole"/> instance.</returns>
    Task<Sole> ParseAsync(string pathA, string pathB, SoleParsingTemplate parsingTemplate);
}