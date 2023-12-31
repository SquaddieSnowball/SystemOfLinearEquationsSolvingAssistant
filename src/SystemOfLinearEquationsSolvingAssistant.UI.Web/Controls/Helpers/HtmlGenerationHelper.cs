using System.Text;
using SystemOfLinearEquationsSolvingAssistant.UI.Web.Extensions.Controls.Base.Entities;

namespace SystemOfLinearEquationsSolvingAssistant.UI.Web.Controls.Helpers;

/// <summary>
/// Provides methods used as helpers when generating HTML markup.
/// </summary>
internal static class HtmlGenerationHelper
{
    /// <summary>
    /// Divides HTML attributes into specified categories.
    /// </summary>
    /// <param name="attributes">HTML attributes for categorization.</param>
    /// <param name="categories">The categories to divide the HTML attributes into.</param>
    /// <returns>A dictionary containing HTML attributes divided into categories.</returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static Dictionary<string, IEnumerable<HtmlAttribute>> DivideByCategory(
        this IEnumerable<HtmlAttribute> attributes, IEnumerable<string>? categories = default)
    {
        if (attributes is null)
            throw new ArgumentNullException(nameof(attributes), "Attributes must not be null.");

        Dictionary<string, IEnumerable<HtmlAttribute>> attributesByCategory = new();

        if (categories is not null)
        {
            categories = categories.Where(c => string.IsNullOrEmpty(c) is false).Distinct().OrderByDescending(c => c.Length);

            foreach (string category in categories)
                attributesByCategory.Add(category, new List<HtmlAttribute>());
        }

        attributesByCategory.Add(string.Empty, new List<HtmlAttribute>());

        foreach (HtmlAttribute attribute in attributes)
        {
            foreach (string category in attributesByCategory.Keys)
            {
                if (attribute.Parameter.StartsWith(category) is true)
                {
                    ((List<HtmlAttribute>)attributesByCategory[category]).Add(attribute);

                    break;
                }
            }
        }

        return attributesByCategory;
    }

    /// <summary>
    /// Adds HTML attributes to <see cref="StringBuilder"/> containing HTML markup.
    /// </summary>
    /// <param name="stringBuilder"><see cref="StringBuilder"/> to add HTML attributes.</param>
    /// <param name="attributes">HTML attributes to add.</param>
    /// <param name="attributesCategoryName">The category name to remove from the attribute names.</param>
    /// <param name="attributesToIgnore">Attributes to ignore.</param>
    /// <returns>The current instance of <see cref="StringBuilder"/>.</returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static StringBuilder AppendHtmlAttributes(this StringBuilder stringBuilder, IEnumerable<HtmlAttribute> attributes,
        string? attributesCategoryName = default, IEnumerable<HtmlAttribute?>? attributesToIgnore = default)
    {
        if (stringBuilder is null)
            throw new ArgumentNullException(nameof(stringBuilder), "String builder must not be null.");

        if (attributes is null)
            throw new ArgumentNullException(nameof(attributes), "Attributes must not be null.");

        int attributesCategoryNameLength = attributesCategoryName?.Length ?? 0;

        foreach (HtmlAttribute attribute in attributes)
        {
            if ((attributesToIgnore?.Contains(attribute) ?? false) is false)
            {
                string attributeParameter = attribute.Parameter;

                if (attributesCategoryNameLength is not 0)
                {
                    attributeParameter = attribute.Parameter.Remove(0, attributesCategoryNameLength);

                    if (string.IsNullOrEmpty(attributeParameter) is true)
                        continue;
                }

                _ = stringBuilder.Append(' ' + attributeParameter);

                string? attributeValue =
                    (attribute.PropertyBinding is not null) ?
                    (attribute.PropertyBinding.Value?.ToString()) :
                    attribute.Value;

                if (string.IsNullOrEmpty(attributeValue) is false)
                    _ = stringBuilder.Append($"=\"{attributeValue}\"");
            }
        }

        return stringBuilder;
    }
}