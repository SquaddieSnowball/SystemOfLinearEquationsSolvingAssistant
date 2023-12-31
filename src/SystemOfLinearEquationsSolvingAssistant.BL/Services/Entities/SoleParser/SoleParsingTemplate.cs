using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace SystemOfLinearEquationsSolvingAssistant.BL.Services.Entities.SoleParser;

/// <summary>
/// Represents a system of linear equations parsing template.
/// </summary>
public sealed class SoleParsingTemplate
{
    /// <summary>
    /// Gets a string separating the integer and fractional parts of numbers in a system of linear equations.
    /// </summary>
    public string DecimalSeparator { get; }

    /// <summary>
    /// Gets a string separating variables in a system of linear equations.
    /// </summary>
    public string VariableSeparator { get; }

    /// <summary>
    /// Gets the regular expression used to parse the lines in matrix A of a system of linear equations.
    /// </summary>
    public Regex RegexLineA { get; private set; }

    /// <summary>
    /// Gets the regular expression used to parse the lines in vector B of a system of linear equations.
    /// </summary>
    public Regex RegexLineB { get; private set; }

    /// <summary>
    /// Initializes a new instance of <see cref="SoleParsingTemplate"/> with the specified decimal and variable separator.
    /// </summary>
    /// <param name="decimalSeparator">A string separating the integer and fractional parts of numbers 
    /// in a system of linear equations.</param>
    /// <param name="variableSeparator">A string separating variables in a system of linear equations.</param>
    /// <exception cref="ArgumentException"></exception>
    public SoleParsingTemplate(string decimalSeparator, string variableSeparator)
    {
        if (string.IsNullOrEmpty(decimalSeparator) is true)
            throw new ArgumentException("Decimal separator must not be null or empty.", nameof(decimalSeparator));

        if (string.IsNullOrEmpty(variableSeparator) is true)
            throw new ArgumentException("Variable separator must not be null or empty.", nameof(variableSeparator));

        (DecimalSeparator, VariableSeparator) = (decimalSeparator, variableSeparator);
        GenerateRegularExpressions();
    }

    [MemberNotNull(nameof(RegexLineA), nameof(RegexLineB))]
    private void GenerateRegularExpressions()
    {
        string regexDecimalSeparator = Regex.Escape(DecimalSeparator);
        string regexVariableSeparator = Regex.Escape(VariableSeparator);

        string regexPatternA =
            @$"^-?\d+({regexDecimalSeparator}\d+)*({regexVariableSeparator}-?\d+({regexDecimalSeparator}\d+)*)*$";
        string regexPatternB =
            @$"^-?\d+({regexDecimalSeparator}\d+)*$";

        RegexLineA = new Regex(regexPatternA);
        RegexLineB = new Regex(regexPatternB);
    }
}