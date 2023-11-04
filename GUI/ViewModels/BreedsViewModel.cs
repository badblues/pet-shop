using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using GUI.Core;
using Persistence.Models;
using Persistence.Repositories;

namespace GUI.ViewModels;

public class BreedsViewModel : ViewModel
{
    private readonly BreedRepository _breedRepository;
    private ICollection<Breed> _breeds = new List<Breed>();
    private ICollection<Breed>  _filteredBreeds = new List<Breed>();
    private Breed? _selectedBreed;
    private string _searchText;

    public ICommand AddBreedCommand { get; set; }
    public ICommand UpdateBreedCommand { get; set; }
    public ICommand DeleteBreedCommand { get; set; }

    public ICollection<Breed> Breeds
    {
        get => _breeds;
        set
        {
            _breeds = value;
            OnPropertyChanged(nameof(Breeds));
            FilterBreeds();
        }
    }

    public ICollection<Breed> FilteredBreeds
    {
        get => _filteredBreeds;
        set
        {
            _filteredBreeds = value;
            OnPropertyChanged(nameof(FilteredBreeds));
        }
    }

    public Breed? SelectedBreed
    {
        get => _selectedBreed;
        set
        {
            _selectedBreed = value;
            OnPropertyChanged(nameof(SelectedBreed));
        }
    }

    public string EnteredName { get; set; }

    public string SearchText
    {
        get => _searchText;
        set
        {
            if (_searchText != value)
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
                FilterBreeds();
            }
        }
    }

    public BreedsViewModel(BreedRepository breedRepository)
    {
        _breedRepository = breedRepository;

        AddBreedCommand = new RelayCommand(AddBreed, o => true);
        UpdateBreedCommand = new RelayCommand(UpdateBreed, o => true);
        DeleteBreedCommand = new RelayCommand(DeleteBreed, o => true);

        Breeds = _breedRepository.GetAll();
        FilterBreeds();
    }

    public void SelectBreed(Breed breed)
    {
        SelectedBreed = breed;
    }

    public void AddBreed(object? unused)
    {
        if (EnteredName?.Length > 0)
        {
            Breed newBreed = new() { Name = EnteredName };
            _breedRepository.Add(newBreed);
            Breeds = _breedRepository.GetAll();
        }
    }

    public void UpdateBreed(object? parameter)
    {
        if (parameter is Breed breed)
        {
            _breedRepository.Update(breed);
            Breeds = _breedRepository.GetAll();
            SelectedBreed = null;
            SelectedBreed = _breedRepository.Get(breed.Id);
        }
    }

    public void DeleteBreed(object? parameter)
    {
        if (parameter is Breed breed)
        {
            try
            {
                _breedRepository.Delete(breed.Id);
                Breeds = _breedRepository.GetAll();
                SelectedBreed = null;
            }
            catch(Exception e)
            {
                string message = "Could not delete breed. There are animals or applications with that breed.";
                MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }

    private void FilterBreeds()
    {
        ICollection<Breed> filteredBreeds = new List<Breed>();

        if (string.IsNullOrWhiteSpace(SearchText))
        {
            foreach (Breed breed in Breeds)
            {
                filteredBreeds.Add(breed);
            }
        }
        else
        {
            string searchTextLower = SearchText.ToLower();
            foreach (Breed breed in Breeds)
            {
                if (breed.Name.ToLower().Contains(searchTextLower))
                {
                    filteredBreeds.Add(breed);
                }
            }
        }

        FilteredBreeds = filteredBreeds;
    }
}
