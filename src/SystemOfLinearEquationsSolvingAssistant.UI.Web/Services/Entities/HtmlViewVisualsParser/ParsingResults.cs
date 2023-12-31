namespace SystemOfLinearEquationsSolvingAssistant.UI.Web.Services.Entities.HtmlViewVisualsParser;

/// <summary>
/// Represents the results of the parsing.
/// </summary>
internal sealed class ParsingResults
{
    private readonly string[] _stringChunks;
    private readonly CodeElement[] _codeElements;

    /// <summary>
    /// Gets string fragments located between code elements.
    /// </summary>
    public IEnumerable<string> StringChunks => _stringChunks;

    /// <summary>
    /// Gets code elements.
    /// </summary>
    public IEnumerable<CodeElement> CodeElements => _codeElements;

    /// <summary>
    /// Initializes a new instance of <see cref="ParsingResults"/> with the specified string chunks and code elements.
    /// </summary>
    /// <param name="stringChunks">String fragments located between code elements.</param>
    /// <param name="codeElements">Code elements.</param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentException"></exception>
    public ParsingResults(IEnumerable<string> stringChunks, IEnumerable<CodeElement> codeElements)
    {
        if (stringChunks is null)
            throw new ArgumentNullException(nameof(stringChunks), "String chunks must not be null.");

        if (codeElements is null)
            throw new ArgumentNullException(nameof(codeElements), "Code elements must not be null.");

        if ((stringChunks.Count() - 1) != codeElements.Count())
            throw new ArgumentException("The number of string chunks must exceed the number of code elements by 1.");

        (_stringChunks, _codeElements) = (stringChunks.ToArray(), codeElements.ToArray());
    }

    /// <summary>
    /// Gets the enumerator for the current <see cref="ParsingResults"/> instance.
    /// </summary>
    /// <returns>The enumerator for the current <see cref="ParsingResults"/> instance.</returns>
    public IEnumerator<(string, CodeElement?)> GetEnumerator()
    {
        for (var i = 0; i < _stringChunks.Length; i++)
        {
            if (i == (_stringChunks.Length - 1))
            {
                yield return (_stringChunks[i], default);
                yield break;
            }

            yield return (_stringChunks[i], _codeElements[i]);
        }
    }
}