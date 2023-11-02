using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Persistence.Models;
using Application = Persistence.Models.Application;

namespace GUI.Controls;

public partial class ApplicationProfile : UserControl
{

    public static readonly DependencyProperty DeleteCommandProperty =
        DependencyProperty.Register("DeleteCommand", typeof(ICommand), typeof(ApplicationProfile));
    public static readonly DependencyProperty UpdateCommandProperty =
        DependencyProperty.Register("UpdateCommand", typeof(ICommand), typeof(ApplicationProfile));
    public static readonly DependencyProperty ChangeStatusCommandProperty =
        DependencyProperty.Register("ChangeStatusCommand", typeof(ICommand), typeof(ApplicationProfile));
    public static readonly DependencyProperty ApplicationProperty =
        DependencyProperty.Register("Application", typeof(Application), typeof(ApplicationProfile));
    public static readonly DependencyProperty BreedsProperty =
        DependencyProperty.Register("Breeds", typeof(IEnumerable<Breed>), typeof(ApplicationProfile));
    public static readonly DependencyProperty EmployeesProperty =
        DependencyProperty.Register("Employees", typeof(IEnumerable<Employee>), typeof(ApplicationProfile));

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

    public ICommand ChangeStatusCommand
    {
        get => (ICommand)GetValue(ChangeStatusCommandProperty);
        set => SetValue(ChangeStatusCommandProperty, value);
    }

    public Application Application
    {
        get => (Application)GetValue(ApplicationProperty);
        set => SetValue(ApplicationProperty, value);
    }
    public IEnumerable<Gender> Genders { get; set; }
    public IEnumerable<Breed> Breeds
    {
        get => (IEnumerable<Breed>)GetValue(BreedsProperty);
        set => SetValue(BreedsProperty, value);
    }
    public IEnumerable<Employee> Employees
    {
        get => (IEnumerable<Employee>)GetValue(EmployeesProperty);
        set => SetValue(EmployeesProperty, value);
    }

    public Employee EnteredEmployee { get; set; }
    public Breed EnteredBreed { get; set; }
    public Gender? EnteredGender { get; set; }

    public ApplicationProfile()
    {
        Genders = new List<Gender>() { Gender.Male, Gender.Female };
        InitializeComponent();
    }

    private void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        if (DeleteCommand != null && DeleteCommand.CanExecute(null))
        {
            DeleteCommand.Execute(Application);
        }
    }

    private void UpdateButton_Click(object sender, RoutedEventArgs e)
    {
        if (EnteredEmployee is null
            && EnteredBreed is null
            && EnteredGender is null)
        {
            return;
        }
        if (UpdateCommand != null && UpdateCommand.CanExecute(null))
        {
            if (EnteredEmployee is not null)
            {
                Application.Employee = EnteredEmployee;
            }

            if (EnteredBreed is not null)
            {
                Application.Breed = EnteredBreed;
            }

            if (EnteredGender is not null)
            {
                Application.Gender = EnteredGender;
            }

            UpdateCommand.Execute(Application);
        }
        return;
    }

    private void ChangeStatusButton_Click(object sender, RoutedEventArgs e)
    {
        if (ChangeStatusCommand != null && ChangeStatusCommand.CanExecute(null))
        {
            ChangeStatusCommand.Execute(Application);
        }
    }
}
