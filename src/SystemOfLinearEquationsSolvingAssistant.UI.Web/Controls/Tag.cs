﻿using System.Text;
using SystemOfLinearEquationsSolvingAssistant.UI.Web.Controls.Base;
using SystemOfLinearEquationsSolvingAssistant.UI.Web.Controls.Exceptions;
using SystemOfLinearEquationsSolvingAssistant.UI.Web.Extensions.Controls.Base.Entities;

namespace SystemOfLinearEquationsSolvingAssistant.UI.Web.Controls;

/// <summary>
/// Represents a tag.
/// </summary>
internal sealed class Tag : HtmlElement
{
    /// <summary>
    /// Gets a value indicating whether the tag is closed.
    /// </summary>
    public bool IsClosed { get; }

    /// <summary>
    /// Initializes a new instance of <see cref="Tag"/> with the specified name and a value indicating whether the tag is closed.
    /// </summary>
    /// <param name="name">Tag name.</param>
    /// <param name="isClosed">A value indicating whether the tag is closed.</param>
    public Tag(string name, bool isClosed = true) : base(name) => IsClosed = isClosed;

    public override string GenerateHtml(int nestingLevel)
    {
        if (nestingLevel < 0)
            throw new ArgumentException("Nesting level must not be negative.", nameof(nestingLevel));

        Validate();

        HtmlAttribute? contentAttribute =
            Attributes.FirstOrDefault(a => a.Parameter.Equals("#content", StringComparison.Ordinal));

        if ((IsClosed is false) && (contentAttribute is not null))
            throw new HtmlElementValidationException($"The tag containing the \"#content\" attribute must be closed.");

        StringBuilder htmlTagStringBuilder = new();

        _ = htmlTagStringBuilder.Append($"{new string(' ', nestingLevel * 4)}<{Name}");

        foreach (HtmlAttribute attribute in Attributes)
        {
            if (attribute.Equals(contentAttribute) is true)
                continue;

            _ = htmlTagStringBuilder.Append(' ' + attribute.Parameter);

            string attributeValue =
                (attribute.PropertyBinding is not null) ?
                (attribute.PropertyBinding.Value?.ToString() ?? string.Empty) :
                attribute.Value;

            if (string.IsNullOrEmpty(attributeValue) is false)
                _ = htmlTagStringBuilder.Append($"=\"{attributeValue}\"");
        }

        _ = htmlTagStringBuilder.Append('>');

        if (contentAttribute is not null)
        {
            _ = htmlTagStringBuilder.Append(
                (contentAttribute.PropertyBinding is not null) ?
                (contentAttribute.PropertyBinding.Value?.ToString() ?? string.Empty) :
                contentAttribute.Value);
        }

        if (IsClosed is true)
            _ = htmlTagStringBuilder.Append($"</{Name}>");

        return htmlTagStringBuilder.ToString();
    }
}