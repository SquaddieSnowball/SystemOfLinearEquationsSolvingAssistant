using SystemOfLinearEquationsSolvingAssistant.BL.SystemOfLinearEquationsSolving.Entities;
using SystemOfLinearEquationsSolvingAssistant.BL.SystemOfLinearEquationsSolving.Parsers.Entities;
using SystemOfLinearEquationsSolvingAssistant.BL.SystemOfLinearEquationsSolving.Parsers.Helpers;

namespace SystemOfLinearEquationsSolvingAssistant.BL.SystemOfLinearEquationsSolving.Parsers.Implementations;

public sealed class SoleFileParser : ISoleParser
{
    public void Initialize(string initializationString) { }

    public Sole Parse(string pathA, string pathB, SoleParsingTemplate parsingTemplate)
    {
        if (string.IsNullOrEmpty(pathA) is true)
            throw new ArgumentException("The path to the file with matrix A must not be null or empty.", nameof(pathA));

        if (string.IsNullOrEmpty(pathB) is true)
            throw new ArgumentException("The path to the file with vector B must not be null or empty.", nameof(pathA));

        if (parsingTemplate is null)
            throw new ArgumentNullException(nameof(parsingTemplate), "The parsing template must not be null.");

        string fileA;
        string fileB;

        try
        {
            fileA = File.ReadAllText(pathA);
            fileB = File.ReadAllText(pathB);
        }
        catch
        {
            throw;
        }

        double[,] a;
        double[] b;

        try
        {
            a = SoleParsingHelper.ParseFromTextA(fileA, parsingTemplate);
            b = SoleParsingHelper.ParseFromTextB(fileB, parsingTemplate);
        }
        catch
        {
            throw;
        }

        Sole sole;

        try
        {
            sole = new Sole(a, b);
        }
        catch
        {
            throw;
        }

        return sole;
    }

    public async Task<Sole> ParseAsync(string pathA, string pathB, SoleParsingTemplate parsingTemplate)
    {
        if (string.IsNullOrEmpty(pathA) is true)
            throw new ArgumentException("The path to the file with matrix A must not be null or empty.", nameof(pathA));

        if (string.IsNullOrEmpty(pathB) is true)
            throw new ArgumentException("The path to the file with vector B must not be null or empty.", nameof(pathA));

        if (parsingTemplate is null)
            throw new ArgumentNullException(nameof(parsingTemplate), "The parsing template must not be null.");

        string fileA;
        string fileB;

        try
        {
            fileA = await File.ReadAllTextAsync(pathA);
            fileB = await File.ReadAllTextAsync(pathB);
        }
        catch
        {
            throw;
        }

        double[,] a;
        double[] b;

        try
        {
            a = await Task.Run(() => SoleParsingHelper.ParseFromTextA(fileA, parsingTemplate));
            b = await Task.Run(() => SoleParsingHelper.ParseFromTextB(fileB, parsingTemplate));
        }
        catch
        {
            throw;
        }

        Sole sole;

        try
        {
            sole = new Sole(a, b);
        }
        catch
        {
            throw;
        }

        return sole;
    }
}