using System.Windows;
using System.Windows.Controls;

namespace SystemOfLinearEquationsSolvingAssistant.UI.Desktop.Views.Extensions.Controls;

/// <summary>
/// Represents an input data grid.
/// </summary>
internal sealed class InputDataGrid : DataGrid
{
    protected override void OnLoadingRow(DataGridRowEventArgs e)
    {
        base.OnLoadingRow(e);

        e.Row.Header = (e.Row.GetIndex() + 1).ToString();
    }

    protected override void OnPreparingCellForEdit(DataGridPreparingCellForEditEventArgs e)
    {
        base.OnPreparingCellForEdit(e);

        e.EditingElement.Style = (Style)Application.Current.Resources["InputDataGridCellTextBoxStyle"];
    }
}