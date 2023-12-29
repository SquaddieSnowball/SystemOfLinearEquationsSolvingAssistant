using System.Reflection;
using System.Text.RegularExpressions;
using SystemOfLinearEquationsSolvingAssistant.UI.Web.Controls;
using SystemOfLinearEquationsSolvingAssistant.UI.Web.Controls.Base;
using SystemOfLinearEquationsSolvingAssistant.UI.Web.Controls.CompositeTags.Base;
using SystemOfLinearEquationsSolvingAssistant.UI.Web.Extensions.Controls.Base.Entities;
using SystemOfLinearEquationsSolvingAssistant.UI.Web.Helpers.HtmlViewVisualsParser;
using SystemOfLinearEquationsSolvingAssistant.UI.Web.Services.Abstractions;
using SystemOfLinearEquationsSolvingAssistant.UI.Web.Services.Entities.HtmlViewVisualsParser;

namespace SystemOfLinearEquationsSolvingAssistant.UI.Web.Services.Implementations;

internal sealed class HtmlViewVisualsParser : IHtmlViewVisualsParser
{
    private const string CodePattern = @"(?<indent>[ \t]*)<!--@.+?@-->";
    private const string TagPattern = @"<\s*(?<tag>\w+)(?<attribute>(\s+(?<parameter>\S+)=""(?<value>[^""]*)""))*\s*(?<closing>/?)>";
    private const string CompositeTagPattern = @"&\s*(?<tag>\w+)(?<attribute>(\s+(?<parameter>\S+)=""(?<value>[^""]*)""))*\s*&";
    private const string PropertyBindingPattern = @"^\$\{(?<name>\w+)\}$";

    public ParsingResults Parse(string viewVisualsPath)
    {
        string viewVisuals;
        string[] stringChunks;
        CodeElement[] codeElements;

        try
        {
            viewVisuals = File.ReadAllText(viewVisualsPath);

            stringChunks = Regex.Split(viewVisuals, CodePattern, RegexOptions.Singleline)
                .Where(s => string.IsNullOrWhiteSpace(s) is false).Select(s => s.Trim('\n', '\r')).ToArray();
            codeElements = Regex.Matches(viewVisuals, CodePattern, RegexOptions.Singleline)
                .Select(m => ParseCodeElement(m)).ToArray();
        }
        catch
        {
            throw;
        }

        return new ParsingResults(stringChunks, codeElements);
    }

    private static CodeElement ParseCodeElement(Match codeElementMatch)
    {
        Match[] tagMatches = Regex.Matches(codeElementMatch.Value, TagPattern).ToArray();
        Match[] compositeTagMatches = Regex.Matches(codeElementMatch.Value, CompositeTagPattern).ToArray();

        if (tagMatches.Any() is false && compositeTagMatches.Any() is false)
            ThrowHelper.HtmlViewVisualsParsingException("The code element does not contain valid HTML elements.",
                index: codeElementMatch.Index);

        Dictionary<int, HtmlElement> indexHtmlElementPairs = new();

        foreach (Match tagMatch in tagMatches)
        {
            Tag tag = new(tagMatch.Groups["tag"].Value, tagMatch.Groups["closing"].Value.Any());

            SetHtmlElementAttributes(tag, tagMatch);

            indexHtmlElementPairs.Add(tagMatch.Index, tag);
        }

        foreach (Match compositeTagMatch in compositeTagMatches)
        {
            Type? compositeTagType = Assembly.GetExecutingAssembly().GetTypes()
                .FirstOrDefault(t => t.Name.Equals(compositeTagMatch.Groups["tag"].Value, StringComparison.Ordinal));

            if (compositeTagType is null || compositeTagType.IsSubclassOf(typeof(CompositeTag)) is false)
                ThrowHelper.HtmlViewVisualsParsingException("Invalid control name.",
                    compositeTagMatch.Groups["tag"].Value, compositeTagMatch.Index);

            CompositeTag compositeTag =
                (CompositeTag)Activator.CreateInstance(compositeTagType!, compositeTagMatch.Groups["tag"].Value)!;

            SetHtmlElementAttributes(compositeTag, compositeTagMatch);

            indexHtmlElementPairs.Add(compositeTagMatch.Index, compositeTag);
        }

        HtmlElement[] htmlElements = indexHtmlElementPairs.OrderBy(ie => ie.Key).Select(ie => ie.Value).ToArray();
        int nestingLevel = codeElementMatch.Groups["indent"].Value.Length / 4;

        return new CodeElement(htmlElements, nestingLevel);
    }

    private static void SetHtmlElementAttributes(HtmlElement htmlElement, Match htmlElementMatch)
    {
        for (var i = 0; i < htmlElementMatch.Groups["attribute"].Captures.Count; i++)
        {
            Match attributePropertyBindingMatch =
                Regex.Match(htmlElementMatch.Groups["value"].Captures[i].Value, PropertyBindingPattern);

            PropertyBinding? attributePropertyBinding =
                attributePropertyBindingMatch.Success is true ?
                new PropertyBinding(attributePropertyBindingMatch.Groups["name"].Value) :
                default;

            htmlElement.AddAttribute(new HtmlAttribute(
                htmlElementMatch.Groups["parameter"].Captures[i].Value,
                htmlElementMatch.Groups["value"].Captures[i].Value,
                attributePropertyBinding));
        }
    }
}