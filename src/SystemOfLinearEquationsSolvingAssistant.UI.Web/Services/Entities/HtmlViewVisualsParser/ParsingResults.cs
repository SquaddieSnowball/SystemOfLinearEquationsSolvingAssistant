namespace SystemOfLinearEquationsSolvingAssistant.UI.Web.Services.Entities.HtmlViewVisualsParser;

internal sealed class ParsingResults
{
    private readonly string[] _stringChunks;
    private readonly CodeElement[] _codeElements;

    public IEnumerable<string> StringChunks => _stringChunks;

    public IEnumerable<CodeElement> CodeElements => _codeElements;

    public ParsingResults(IEnumerable<string> stringChunks, IEnumerable<CodeElement> codeElements)
    {
        if (stringChunks is null)
            throw new ArgumentNullException(nameof(stringChunks), "String chunks must not be null.");

        if (codeElements is null)
            throw new ArgumentNullException(nameof(codeElements), "Code elements must not be null.");

        if (stringChunks.Count() - 1 != codeElements.Count())
            throw new ArgumentException("The number of string chunks must exceed the number of code elements by 1.");

        (_stringChunks, _codeElements) = (stringChunks.ToArray(), codeElements.ToArray());
    }

    public IEnumerator<(string, CodeElement?)> GetEnumerator()
    {
        for (var i = 0; i < _stringChunks.Length; i++)
        {
            if (i == _stringChunks.Length - 1)
            {
                yield return (_stringChunks[i], default);
                yield break;
            }

            yield return (_stringChunks[i], _codeElements[i]);
        }
    }
}