﻿using System.Globalization;
using SystemOfLinearEquationsSolvingAssistant.BL.Services.Entities.SoleParser;

namespace SystemOfLinearEquationsSolvingAssistant.BL.Helpers.SoleParser;

/// <summary>
/// Provides helper methods used to parse systems of linear equations.
/// </summary>
internal static class SoleParserHelper
{
    /// <summary>
    /// Parses matrix A of a system of linear equations from the text.
    /// </summary>
    /// <param name="text">Text to parse.</param>
    /// <param name="parsingTemplate">A template that will be used during the parsing process.</param>
    /// <returns>An array containing matrix A of a system of linear equations.</returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentException"></exception>
    public static double[,] ParseFromTextA(string text, SoleParsingTemplate parsingTemplate)
    {
        if (text is null)
            throw new ArgumentNullException(nameof(text), "Text must not be null.");

        if (parsingTemplate is null)
            throw new ArgumentNullException(nameof(parsingTemplate), "Parsing template must not be null.");

        string[] rowLines = text.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

        NumberFormatInfo parsingNumberFormatInfo = new()
        {
            NumberDecimalSeparator = parsingTemplate.DecimalSeparator
        };

        int n = rowLines.Length;
        double[,] a = new double[n, n];

        for (var i = 0; i < n; i++)
        {
            if (parsingTemplate.RegexLineA.IsMatch(rowLines[i]) is false)
                throw new ArgumentException($"The line \"{i + 1}\" does not match the parsing template.", nameof(text));

            string[] numberLines = rowLines[i].Split(parsingTemplate.VariableSeparator, StringSplitOptions.RemoveEmptyEntries);

            if (numberLines.Length != n)
            {
                throw new ArgumentException($"The amount of numbers in the line \"{i + 1}\" " +
                    $"does not match the number of lines.", nameof(text));
            }

            for (var j = 0; j < n; j++)
                a[i, j] = double.Parse(numberLines[j], parsingNumberFormatInfo);
        }

        return a;
    }

    /// <summary>
    /// Parses vector B of a system of linear equations from the text.
    /// </summary>
    /// <param name="text">Text to parse.</param>
    /// <param name="parsingTemplate">A template that will be used during the parsing process.</param>
    /// <returns>An array containing vector B of a system of linear equations.</returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentException"></exception>
    public static double[] ParseFromTextB(string text, SoleParsingTemplate parsingTemplate)
    {
        if (text is null)
            throw new ArgumentNullException(nameof(text), "Text must not be null.");

        if (parsingTemplate is null)
            throw new ArgumentNullException(nameof(parsingTemplate), "Parsing template must not be null.");

        string[] numberLines = text.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

        NumberFormatInfo parsingNumberFormatInfo = new()
        {
            NumberDecimalSeparator = parsingTemplate.DecimalSeparator
        };

        int n = numberLines.Length;
        double[] b = new double[n];

        for (var i = 0; i < n; i++)
        {
            if (parsingTemplate.RegexLineB.IsMatch(numberLines[i]) is false)
                throw new ArgumentException($"The line \"{i + 1}\" does not match the parsing template.", nameof(text));

            b[i] = double.Parse(numberLines[i], parsingNumberFormatInfo);
        }

        return b;
    }
}