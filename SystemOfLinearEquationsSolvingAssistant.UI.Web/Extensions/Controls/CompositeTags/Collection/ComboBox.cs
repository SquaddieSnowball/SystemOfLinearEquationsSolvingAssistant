using System.Text;
using SystemOfLinearEquationsSolvingAssistant.UI.Web.Extensions.Controls.Base.Entities;
using SystemOfLinearEquationsSolvingAssistant.UI.Web.Extensions.Controls.CompositeTags.Base;
using SystemOfLinearEquationsSolvingAssistant.UI.Web.Extensions.Controls.CompositeTags.Helpers;

namespace SystemOfLinearEquationsSolvingAssistant.UI.Web.Extensions.Controls.CompositeTags.Collection;

internal sealed class ComboBox : CompositeTag
{
    public ComboBox(string name) : base(name) { }

    public override string GenerateHtml(int nestingLevel)
    {
        if (nestingLevel < 0)
            throw new ArgumentException("Nesting level must not be negative.", nameof(nestingLevel));

        try
        {
            Validate();
        }
        catch
        {
            throw;
        }

        Dictionary<string, IEnumerable<HtmlAttribute>> attributesByCategory =
            Attributes.DivideByCategory(new string[] { "#", "#i-" });

        HtmlAttribute? selectedAttribute =
            attributesByCategory["#"].FirstOrDefault(a => a.Parameter.Equals("#selected", StringComparison.Ordinal));
        HtmlAttribute? itemsAttribute =
            attributesByCategory["#"].FirstOrDefault(a => a.Parameter.Equals("#items", StringComparison.Ordinal));

        StringBuilder comboBoxStringBuilder = new();

        _ = comboBoxStringBuilder.Append($"{new string(' ', nestingLevel * 4)}<select");
        _ = comboBoxStringBuilder.AppendHtmlAttributes(attributesByCategory[string.Empty]);
        _ = comboBoxStringBuilder.Append('>');

        object? selectedItem =
            (selectedAttribute?.PropertyBinding is not null) ?
            selectedAttribute.PropertyBinding.Value :
            selectedAttribute?.Value;

        IEnumerable<object?>? items = itemsAttribute?.PropertyBinding?.Value as IEnumerable<object?>;

        if (items is not null)
        {
            string? stringSelectedItem = selectedItem?.ToString();
            string[] stringItems = items.Select(i => i?.ToString()).Where(s => string.IsNullOrEmpty(s) is false).ToArray()!;

            if (stringItems.Any() is true)
            {
                _ = comboBoxStringBuilder.AppendLine();

                foreach (string stringItem in stringItems)
                {
                    _ = comboBoxStringBuilder.Append($"{new string(' ', (nestingLevel + 1) * 4)}<option");
                    _ = comboBoxStringBuilder.AppendHtmlAttributes(attributesByCategory["#i-"], "#i-");

                    if (stringItem.Equals(stringSelectedItem, StringComparison.Ordinal) is true)
                    {
                        stringSelectedItem = default;

                        _ = comboBoxStringBuilder.Append(" selected");
                    }

                    string valueAttributeValue = string.Join('-', new string(
                        stringItem
                        .ToLower()
                        .Where(c => (char.IsLetterOrDigit(c) is true) || (char.IsWhiteSpace(c) is true))
                        .ToArray())
                        .Split(' ', StringSplitOptions.RemoveEmptyEntries));

                    _ = comboBoxStringBuilder.AppendLine($" value=\"{valueAttributeValue}\">{stringItem}</option>");
                }

                _ = comboBoxStringBuilder.Append(new string(' ', nestingLevel * 4));
            }
        }

        _ = comboBoxStringBuilder.Append("</select>");

        return comboBoxStringBuilder.ToString();
    }
}