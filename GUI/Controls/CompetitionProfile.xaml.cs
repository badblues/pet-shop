using System.Windows;
using System.Windows.Controls;
using Persistence.Models;
using System.Windows.Input;
using System;

namespace GUI.Controls;

public partial class CompetitionProfile : UserControl
{

    public static readonly DependencyProperty DeleteCommandProperty =
        DependencyProperty.Register("DeleteCommand", typeof(ICommand), typeof(CompetitionProfile));
    public static readonly DependencyProperty UpdateCommandProperty =
        DependencyProperty.Register("UpdateCommand", typeof(ICommand), typeof(CompetitionProfile));
    public static readonly DependencyProperty CompetitionProperty =
        DependencyProperty.Register("Competition", typeof(Competition), typeof(CompetitionProfile));

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

    public Competition Competition
    {
        get => (Competition)GetValue(CompetitionProperty);
        set => SetValue(CompetitionProperty, value);
    }
    public string EnteredName { get; set; }
    public string EnteredLocation { get; set; }
    public DateTime? EnteredDate { get; set; }

    public CompetitionProfile()
    {
        InitializeComponent();
    }

    private void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        if (DeleteCommand != null && DeleteCommand.CanExecute(null))
        {
            DeleteCommand.Execute(Competition);
        }
    }

    private void UpdateButton_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(EnteredName)
            && string.IsNullOrWhiteSpace(EnteredLocation)
            && EnteredDate is null)
        {
            return;
        }
        if (UpdateCommand != null && UpdateCommand.CanExecute(null))
        {
            if (EnteredName?.Length > 0)
            {
                Competition.Name = EnteredName;
            }

            if (EnteredLocation?.Length > 0)
            {
                Competition.Location = EnteredLocation;
            }

            if (EnteredDate is not null)
            {
                Competition.Date = (DateTime)EnteredDate;
            }

            UpdateCommand.Execute(Competition);
        }
        return;
    }
}

