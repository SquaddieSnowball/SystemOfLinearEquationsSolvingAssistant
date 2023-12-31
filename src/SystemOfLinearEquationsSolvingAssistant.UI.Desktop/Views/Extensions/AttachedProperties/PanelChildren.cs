using System.Windows;
using System.Windows.Controls;

namespace SystemOfLinearEquationsSolvingAssistant.UI.Desktop.Views.Extensions.AttachedProperties;

/// <summary>
/// Represents an attached property used to control the children of a <see cref="Panel"/>.
/// </summary>
internal sealed class PanelChildren
{
    /// <summary>
    /// Represents a property used to control margin.
    /// </summary>
    public static readonly DependencyProperty MarginProperty =
        DependencyProperty.RegisterAttached("Margin", typeof(Thickness), typeof(PanelChildren),
            new FrameworkPropertyMetadata(new Thickness(), MarginChangedCallback));

    /// <summary>
    /// Gets the value of the margin.
    /// </summary>
    /// <param name="panel"><see cref="Panel"/> to get the value.</param>
    /// <returns></returns>
    public static Thickness GetMargin(Panel panel) => (Thickness)panel.GetValue(MarginProperty);

    /// <summary>
    /// Sets the value of the margin.
    /// </summary>
    /// <param name="panel"><see cref="Panel"/> to set the value.</param>
    /// <param name="value">The value to be set.</param>
    public static void SetMargin(Panel panel, Thickness value) => panel.SetValue(MarginProperty, value);

    private static void MarginChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not Panel panel)
            return;

        if (panel.IsLoaded is true)
            SetPanelChildrenMargin(panel, true);
        else
            panel.Loaded += (_, _) => SetPanelChildrenMargin(panel, false);
    }

    private static void SetPanelChildrenMargin(Panel panel, bool overwriteCurrentValues)
    {
        foreach (object? child in panel.Children)
        {
            if ((child is FrameworkElement frameworkElement) && ((overwriteCurrentValues is true) ||
                (frameworkElement.ReadLocalValue(FrameworkElement.MarginProperty) == DependencyProperty.UnsetValue)))
                frameworkElement.Margin = GetMargin(panel);
        }
    }
}