﻿using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Persistence.Models;

namespace GUI.Controls;

public partial class AnimalProfile : UserControl
{
    public static readonly DependencyProperty DeleteCommandProperty =
        DependencyProperty.Register("DeleteCommand", typeof(ICommand), typeof(AnimalProfile));
    public static readonly DependencyProperty UpdateCommandProperty =
        DependencyProperty.Register("UpdateCommand", typeof(ICommand), typeof(AnimalProfile));
    public static readonly DependencyProperty AnimalProperty =
        DependencyProperty.Register("Animal", typeof(Animal), typeof(AnimalProfile));
    public static readonly DependencyProperty ClientsProperty =
        DependencyProperty.Register("Clients", typeof(ICollection<Client>), typeof(AnimalProfile));
    public static readonly DependencyProperty BreedsProperty =
        DependencyProperty.Register("Breeds", typeof(ICollection<Breed>), typeof(AnimalProfile));

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

    public Animal Animal
    {
        get => (Animal)GetValue(AnimalProperty);
        set => SetValue(AnimalProperty, value);
    }
    public ICollection<Gender> Genders { get; set; }
    public ICollection<Breed> Breeds
    {
        get => (ICollection<Breed>)GetValue(BreedsProperty);
        set => SetValue(BreedsProperty, value);
    }
    public ICollection<Client> Clients
    {
        get => (ICollection<Client>)GetValue(ClientsProperty);
        set => SetValue(ClientsProperty, value);
    }

    public string EnteredName { get; set; }
    public string EnteredAge { get; set; }
    public Gender? EnteredGender { get; set; }
    public Breed EnteredBreed { get; set; }
    public string EnteredExteriorDescription { get; set; }
    public string EnteredPedigree { get; set; }
    public string EnteredVeterinarian { get; set; }
    public Client EnteredOwner { get; set; }

    public AnimalProfile()
    {
        Genders = new List<Gender>() { Gender.Male, Gender.Female };
        InitializeComponent();
    }

    private void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        if (DeleteCommand != null && DeleteCommand.CanExecute(null))
        {
            DeleteCommand.Execute(Animal);
        }
    }

    private void UpdateButton_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(EnteredName)
            && string.IsNullOrWhiteSpace(EnteredAge)
            && EnteredBreed is null
            && string.IsNullOrWhiteSpace(EnteredExteriorDescription)
            && string.IsNullOrWhiteSpace(EnteredPedigree)
            && string.IsNullOrWhiteSpace(EnteredVeterinarian)
            && EnteredGender is null
            && EnteredOwner is null)
        {
            return;
        }
        if (UpdateCommand != null && UpdateCommand.CanExecute(null))
        {
            if (EnteredName?.Length > 0)
            {
                Animal.Name = EnteredName;
            }

            if (EnteredGender is not null)
            {
                Animal.Gender = (Gender)EnteredGender;
            }

            if (EnteredAge?.Length > 0)
            {
                Animal.Age = int.Parse(EnteredAge);
            }

            if (EnteredBreed is not null)
            {
                Animal.Breed = EnteredBreed;
            }
            if (EnteredExteriorDescription?.Length > 0)
            {
                Animal.ExteriorDescription = EnteredExteriorDescription;
            }

            if (EnteredPedigree?.Length > 0)
            {
                Animal.Pedigree = EnteredPedigree;
            }

            if (EnteredVeterinarian?.Length > 0)
            {
                Animal.Veterinarian = EnteredVeterinarian;
            }

            if (EnteredOwner is not null)
            {
                Animal.Client = EnteredOwner;
            }
            UpdateCommand.Execute(Animal);
        }
        return;
    }

    private void PositiveIntegerTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        if (!int.TryParse(e.Text, out _))
        {
            e.Handled = true;
        }
    }
}
