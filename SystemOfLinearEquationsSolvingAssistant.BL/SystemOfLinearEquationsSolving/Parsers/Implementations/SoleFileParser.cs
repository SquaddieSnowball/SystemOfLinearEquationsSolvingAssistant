using SystemOfLinearEquationsSolvingAssistant.BL.SystemOfLinearEquationsSolving.Entities;
using SystemOfLinearEquationsSolvingAssistant.BL.SystemOfLinearEquationsSolving.Parsers.Entities;
using SystemOfLinearEquationsSolvingAssistant.BL.SystemOfLinearEquationsSolving.Parsers.Helpers;

namespace SystemOfLinearEquationsSolvingAssistant.BL.SystemOfLinearEquationsSolving.Parsers.Implementations;

public sealed class SoleFileParser : ISoleParser
{
    void ISoleParser.Initialize(string initializationString) { }

    public Sole Parse(string pathA, string pathB, SoleParsingTemplate parsingTemplate)
    {
        if (string.IsNullOrEmpty(pathA) is true)
            throw new ArgumentException("The path to the file with matrix A must not be null or empty.", nameof(pathA));

        if (string.IsNullOrEmpty(pathB) is true)
            throw new ArgumentException("The path to the file with vector B must not be null or empty.", nameof(pathA));

        if (parsingTemplate is null)
            throw new ArgumentNullException(nameof(parsingTemplate), "Parsing template must not be null.");

        string fileTextA;
        string fileTextB;

        try
        {
            fileTextA = File.ReadAllText(pathA);
            fileTextB = File.ReadAllText(pathB);
        }
        catch
        {
            throw;
        }

        double[,] a;
        double[] b;

        try
        {
            a = SoleParsingHelper.ParseFromTextA(fileTextA, parsingTemplate);
        }
        catch (ArgumentException ex)
        {
            throw new ArgumentException($"Matrix A: {ex.Message}", ex);
        }
        catch
        {
            throw;
        }

        try
        {
            b = SoleParsingHelper.ParseFromTextB(fileTextB, parsingTemplate);
        }
        catch (ArgumentException ex)
        {
            throw new ArgumentException($"Vector B: {ex.Message}", ex);
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
            throw new ArgumentNullException(nameof(parsingTemplate), "Parsing template must not be null.");

        string fileTextA;
        string fileTextB;

        try
        {
            fileTextA = await File.ReadAllTextAsync(pathA);
            fileTextB = await File.ReadAllTextAsync(pathB);
        }
        catch
        {
            throw;
        }

        double[,] a;
        double[] b;

        try
        {
            a = await Task.Run(() => SoleParsingHelper.ParseFromTextA(fileTextA, parsingTemplate));
        }
        catch (ArgumentException ex)
        {
            throw new ArgumentException($"Matrix A: {ex.Message}", ex);
        }
        catch
        {
            throw;
        }

        try
        {
            b = await Task.Run(() => SoleParsingHelper.ParseFromTextB(fileTextB, parsingTemplate));
        }
        catch (ArgumentException ex)
        {
            throw new ArgumentException($"Vector B: {ex.Message}", ex);
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