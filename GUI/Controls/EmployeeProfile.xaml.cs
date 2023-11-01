using System.Windows;
using System.Windows.Controls;
using Persistence.Models;
using System.Windows.Input;

namespace GUI.Controls;

public partial class EmployeeProfile : UserControl
{
    public static readonly DependencyProperty DeleteCommandProperty =
        DependencyProperty.Register("DeleteCommand", typeof(ICommand), typeof(EmployeeProfile));
    public static readonly DependencyProperty UpdateCommandProperty =
        DependencyProperty.Register("UpdateCommand", typeof(ICommand), typeof(EmployeeProfile));
    public static readonly DependencyProperty EmployeeProperty =
        DependencyProperty.Register("Employee", typeof(Employee), typeof(EmployeeProfile));

    public ICommand DeleteCommand
    {
        get => (ICommand)GetValue(DeleteCommandProperty);
        set => SetValue(DeleteCommandProperty, value);
    }

    public ICommand UpdateCommand
    {
        get => (ICommand)GetValue(UpdateCommandProperty);
        set => SetValue(UpdateCommandProperty, value);
    }

    public Employee Employee
    {
        get => (Employee)GetValue(EmployeeProperty);
        set => SetValue(EmployeeProperty, value);
    }

    public string EnteredName { get; set; }
    public string EnteredAddress { get; set; }
    public string EnteredPosition { get; set; }
    public string EnteredSalary { get; set; }

    public EmployeeProfile()
    {
        InitializeComponent();
    }

    private void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        if (DeleteCommand != null && DeleteCommand.CanExecute(null))
        {
            DeleteCommand.Execute(Employee);
        }
    }

    private void UpdateButton_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(EnteredName)
            && string.IsNullOrWhiteSpace(EnteredAddress)
            && string.IsNullOrWhiteSpace(EnteredPosition)
            && string.IsNullOrWhiteSpace(EnteredSalary))
        {
            return;
        }
        if (UpdateCommand != null && UpdateCommand.CanExecute(null))
        {
            if (EnteredName?.Length > 0)
            {
                Employee.Name = EnteredName;
            }

            if (EnteredAddress?.Length > 0)
            {
                Employee.Address = EnteredAddress;
            }

            if (EnteredPosition?.Length > 0)
            {
                Employee.Position = EnteredPosition;
            }

            if (EnteredSalary?.Length > 0)
            {
                Employee.Salary = double.Parse(EnteredSalary);
            }

            UpdateCommand.Execute(Employee);
        }
        return;
    }

    private void PositiveDoubleTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        if (!double.TryParse(e.Text, out _))
        {
            e.Handled = true;
        }
    }
}
