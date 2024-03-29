﻿using System.Data;
using System.Text;
using SystemOfLinearEquationsSolvingAssistant.UI.Web.Controls.CompositeTags.Base;
using SystemOfLinearEquationsSolvingAssistant.UI.Web.Controls.Helpers;
using SystemOfLinearEquationsSolvingAssistant.UI.Web.Extensions.Controls.Base.Entities;

namespace SystemOfLinearEquationsSolvingAssistant.UI.Web.Controls.CompositeTags;

/// <summary>
/// Represents a composite tag used to display data in a grid.
/// </summary>
internal sealed class DataGrid : CompositeTag
{
    /// <summary>
    /// Initializes a new instance of <see cref="DataGrid"/> with the specified name.
    /// </summary>
    /// <param name="name">Tag name.</param>
    public DataGrid(string name) : base(name) { }

    public override string GenerateHtml(int nestingLevel)
    {
        if (nestingLevel < 0)
            throw new ArgumentException("Nesting level must not be negative.", nameof(nestingLevel));

        Validate();

        Dictionary<string, IEnumerable<HtmlAttribute>> attributesByCategory =
            Attributes.DivideByCategory(new string[] { "#", "#h-", "#c-" });

        HtmlAttribute? columnsNumerationAttribute =
            attributesByCategory["#"].FirstOrDefault(a => a.Parameter.Equals("#numc", StringComparison.Ordinal));
        HtmlAttribute? rowsNumerationAttribute =
            attributesByCategory["#"].FirstOrDefault(a => a.Parameter.Equals("#numr", StringComparison.Ordinal));
        HtmlAttribute? itemsAttribute =
            attributesByCategory["#"].FirstOrDefault(a => a.Parameter.Equals("#items", StringComparison.Ordinal));

        StringBuilder dataGridStringBuilder = new();

        _ = dataGridStringBuilder.Append($"{new string(' ', nestingLevel * 4)}<table");
        _ = dataGridStringBuilder.AppendHtmlAttributes(attributesByCategory[string.Empty]);
        _ = dataGridStringBuilder.Append('>');

        DataTable? items = itemsAttribute?.PropertyBinding?.Value as DataTable;

        if ((items is not null) && (items.Columns.Count > 0) && (items.Rows.Count > 0))
        {
            _ = dataGridStringBuilder.AppendLine();

            if (columnsNumerationAttribute is not null)
            {
                _ = dataGridStringBuilder.AppendLine($"{new string(' ', (nestingLevel + 1) * 4)}<tr>");
                _ = dataGridStringBuilder.Append($"{new string(' ', (nestingLevel + 2) * 4)}<th");
                _ = dataGridStringBuilder.AppendHtmlAttributes(attributesByCategory["#h-"], "#h-");
                _ = dataGridStringBuilder.AppendLine("></th>");

                for (var i = 0; i < items.Columns.Count; i++)
                {
                    _ = dataGridStringBuilder.Append($"{new string(' ', (nestingLevel + 2) * 4)}<th");
                    _ = dataGridStringBuilder.AppendHtmlAttributes(attributesByCategory["#h-"], "#h-");
                    _ = dataGridStringBuilder.AppendLine($" scope=\"col\">{i + 1}</th>");
                }

                _ = dataGridStringBuilder.AppendLine($"{new string(' ', (nestingLevel + 1) * 4)}</tr>");
            }

            for (var i = 0; i < items.Rows.Count; i++)
            {
                _ = dataGridStringBuilder.AppendLine($"{new string(' ', (nestingLevel + 1) * 4)}<tr>");

                if (rowsNumerationAttribute is not null)
                {
                    _ = dataGridStringBuilder.Append($"{new string(' ', (nestingLevel + 2) * 4)}<th");
                    _ = dataGridStringBuilder.AppendHtmlAttributes(attributesByCategory["#h-"], "#h-");
                    _ = dataGridStringBuilder.AppendLine($" scope=\"row\">{i + 1}</th>");
                }

                foreach (object? item in items.Rows[i].ItemArray)
                {
                    _ = dataGridStringBuilder.Append($"{new string(' ', (nestingLevel + 2) * 4)}<td");
                    _ = dataGridStringBuilder.AppendHtmlAttributes(attributesByCategory["#c-"], "#c-");
                    _ = dataGridStringBuilder.AppendLine($">{item}</td>");
                }

                _ = dataGridStringBuilder.AppendLine($"{new string(' ', (nestingLevel + 1) * 4)}</tr>");
            }

            _ = dataGridStringBuilder.Append(new string(' ', nestingLevel * 4));
        }

        _ = dataGridStringBuilder.Append("</table>");

        return dataGridStringBuilder.ToString();
    }
}