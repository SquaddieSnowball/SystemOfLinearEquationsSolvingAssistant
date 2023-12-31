using SystemOfLinearEquationsSolvingAssistant.BL.Entities;
using SystemOfLinearEquationsSolvingAssistant.BL.Helpers.SoleParser;
using SystemOfLinearEquationsSolvingAssistant.BL.Services.Abstractions;
using SystemOfLinearEquationsSolvingAssistant.BL.Services.Entities.SoleParser;

namespace SystemOfLinearEquationsSolvingAssistant.BL.Services.Implementations.SoleParser;

/// <summary>
/// Provides methods used to parse systems of linear equations from files.
/// </summary>
public sealed class SoleFileParser : ISoleParser
{
    void ISoleParser.Initialize(string initializationString) { }

    /// <summary>
    /// Parses a system of linear equations from files.
    /// </summary>
    /// <param name="pathA">Path to the file containing matrix A of the system of linear equations.</param>
    /// <param name="pathB">Path to the file containing vector B of the system of linear equations.</param>
    /// <param name="parsingTemplate">A template that will be used during the parsing process.</param>
    /// <returns>A new instance of the <see cref="Sole"/>.</returns>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    public Sole Parse(string pathA, string pathB, SoleParsingTemplate parsingTemplate)
    {
        if (string.IsNullOrEmpty(pathA) is true)
            throw new ArgumentException("The path to the file with matrix A must not be null or empty.", nameof(pathA));

        if (string.IsNullOrEmpty(pathB) is true)
            throw new ArgumentException("The path to the file with vector B must not be null or empty.", nameof(pathA));

        if (parsingTemplate is null)
            throw new ArgumentNullException(nameof(parsingTemplate), "Parsing template must not be null.");

        string fileTextA = File.ReadAllText(pathA);
        string fileTextB = File.ReadAllText(pathB);

        double[,] a;
        double[] b;

        try
        {
            a = SoleParserHelper.ParseFromTextA(fileTextA, parsingTemplate);
        }
        catch (ArgumentException ex)
        {
            throw new ArgumentException($"Matrix A: \"{ex.Message}\"", ex);
        }

        try
        {
            b = SoleParserHelper.ParseFromTextB(fileTextB, parsingTemplate);
        }
        catch (ArgumentException ex)
        {
            throw new ArgumentException($"Vector B: \"{ex.Message}\"", ex);
        }

        return new Sole(a, b);
    }

    /// <summary>
    /// Asynchronously parses a system of linear equations from files.
    /// </summary>
    /// <param name="pathA">Path to the file containing matrix A of the system of linear equations.</param>
    /// <param name="pathB">Path to the file containing vector B of the system of linear equations.</param>
    /// <param name="parsingTemplate">A template that will be used during the parsing process.</param>
    /// <returns>A task representing an asynchronous operation that wraps a new <see cref="Sole"/> instance.</returns>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    public async Task<Sole> ParseAsync(string pathA, string pathB, SoleParsingTemplate parsingTemplate)
    {
        if (string.IsNullOrEmpty(pathA) is true)
            throw new ArgumentException("The path to the file with matrix A must not be null or empty.", nameof(pathA));

        if (string.IsNullOrEmpty(pathB) is true)
            throw new ArgumentException("The path to the file with vector B must not be null or empty.", nameof(pathA));

        if (parsingTemplate is null)
            throw new ArgumentNullException(nameof(parsingTemplate), "Parsing template must not be null.");

        string fileTextA = await File.ReadAllTextAsync(pathA);
        string fileTextB = await File.ReadAllTextAsync(pathB);

        double[,] a;
        double[] b;

        try
        {
            a = await Task.Run(() => SoleParserHelper.ParseFromTextA(fileTextA, parsingTemplate));
        }
        catch (ArgumentException ex)
        {
            throw new ArgumentException($"Matrix A: \"{ex.Message}\"", ex);
        }

        try
        {
            b = await Task.Run(() => SoleParserHelper.ParseFromTextB(fileTextB, parsingTemplate));
        }
        catch (ArgumentException ex)
        {
            throw new ArgumentException($"Vector B: \"{ex.Message}\"", ex);
        }

        return new Sole(a, b);
    }
}