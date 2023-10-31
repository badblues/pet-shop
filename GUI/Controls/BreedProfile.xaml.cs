using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using GUI.CustomEventArgs;
using Persistence.Models;

namespace GUI.Controls;

public partial class BreedProfile : UserControl
{
    public static readonly DependencyProperty DeleteCommandProperty =
        DependencyProperty.Register("DeleteCommand", typeof(ICommand), typeof(BreedProfile));
    public static readonly DependencyProperty UpdateCommandProperty =
        DependencyProperty.Register("UpdateCommand", typeof(ICommand), typeof(BreedProfile));
    public static readonly DependencyProperty BreedProperty =
        DependencyProperty.Register("Breed", typeof(Breed), typeof(BreedProfile));

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

    public Breed Breed
    {
        get => (Breed)GetValue(BreedProperty);
        set => SetValue(BreedProperty, value);
    }

    public string EnteredName { get; set; }

    public BreedProfile()
    {
        InitializeComponent();
    }

    private void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        if (DeleteCommand != null && DeleteCommand.CanExecute(null))
        {
            DeleteCommand.Execute(Breed);
        }
    }

    private void UpdateButton_Click(object sender, RoutedEventArgs e)
    {
        if (EnteredName?.Length == 0)
        {
            return;
        }

        if (UpdateCommand != null && UpdateCommand.CanExecute(null))
        {
            if (EnteredName?.Length > 0)
            {
                Breed.Name = EnteredName;
            }

            UpdateCommand.Execute(Breed);
        }
    }
}
