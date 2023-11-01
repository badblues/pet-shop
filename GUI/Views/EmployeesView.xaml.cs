using System.Windows.Controls;
using System.Windows.Input;
using GUI.ViewModels;
using Persistence.Models;

namespace GUI.Views;

public partial class EmployeesView : UserControl
{
    public EmployeesView()
    {
        InitializeComponent();
    }

    private void Employees_MouseLeftButton(object sender, MouseButtonEventArgs e)
    {
        EmployeesViewModel context = (EmployeesViewModel)DataContext;
        DockPanel dockPanel = (DockPanel)sender;
        Employee? employee = dockPanel.DataContext as Employee;
        context.SelectEmployee(employee);
    }

    private void PositiveDoubleTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        if (!double.TryParse(e.Text, out _))
        {
            e.Handled = true;
        }
    }
}
