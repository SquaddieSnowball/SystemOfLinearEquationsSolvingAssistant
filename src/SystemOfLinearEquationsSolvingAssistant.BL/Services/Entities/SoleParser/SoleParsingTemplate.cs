using System.Text.RegularExpressions;

namespace SystemOfLinearEquationsSolvingAssistant.BL.Services.Entities.SoleParser;

public sealed class SoleParsingTemplate
{
    public string DecimalSeparator { get; }

    public string VariableSeparator { get; }

    public Regex RegexLineA { get; }

    public Regex RegexLineB { get; }

    public SoleParsingTemplate(string decimalSeparator, string variableSeparator)
    {
        if (string.IsNullOrEmpty(decimalSeparator) is true)
            throw new ArgumentException("Decimal separator must not be null or empty.", nameof(decimalSeparator));

        if (string.IsNullOrEmpty(variableSeparator) is true)
            throw new ArgumentException("Variable separator must not be null or empty.", nameof(variableSeparator));

        (DecimalSeparator, VariableSeparator) = (decimalSeparator, variableSeparator);

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