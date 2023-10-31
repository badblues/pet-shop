using System.Collections.Generic;
using System.Net.Sockets;
using System.Windows.Input;
using GUI.Core;
using Persistence.Models;
using Persistence.Repositories;

namespace GUI.ViewModels;

public class BreedsViewModel : ViewModel
{
    private readonly BreedRepository _breedRepository;
    private IEnumerable<Breed> _breeds = new List<Breed>();
    private Breed? _selectedBreed;
    private string _name;

    public ICommand AddBreedCommand { get; set; }
    public ICommand UpdateBreedCommand { get; set; }
    public ICommand DeleteBreedCommand { get; set; }

    public IEnumerable<Breed> Breeds
    {
        get => _breeds;
        set
        {
            _breeds = value;
            OnPropertyChanged(nameof(Breeds));
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

    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            OnPropertyChanged(nameof(Name));
        }
    }

    public BreedsViewModel(BreedRepository breedRepository)
    {
        _breedRepository = breedRepository;

        AddBreedCommand = new RelayCommand(AddBreed, o => true);
        UpdateBreedCommand = new RelayCommand(UpdateBreed, o => true);
        DeleteBreedCommand = new RelayCommand(DeleteBreed, o => true);

        Breeds = _breedRepository.GetAll();
    }

    public void SelectBreed(Breed breed)
    {
        SelectedBreed = breed;
    }

    public void AddBreed(object? unused)
    {
        if (Name?.Length > 0)
        {
            Breed newBreed = new() { Name = Name };
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
            _breedRepository.Delete(breed.Id);
            Breeds = _breedRepository.GetAll();
            SelectedBreed = null;
        }
    }
}
