using System.Windows;
using System.Windows.Controls;

namespace SystemOfLinearEquationsSolvingAssistant.UI.Desktop.Extensions.AttachedProperties;

internal sealed class PanelChildren
{
    public static readonly DependencyProperty MarginProperty =
        DependencyProperty.RegisterAttached("Margin", typeof(Thickness), typeof(PanelChildren),
            new FrameworkPropertyMetadata(new Thickness(), MarginChangedCallback));

    public static Thickness GetMargin(Panel panel) => (Thickness)panel.GetValue(MarginProperty);

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
            if ((child is FrameworkElement frameworkElement) && ((overwriteCurrentValues is true) ||
                (frameworkElement.ReadLocalValue(FrameworkElement.MarginProperty) == DependencyProperty.UnsetValue)))
                frameworkElement.Margin = GetMargin(panel);
    }
}