using System.Collections.Generic;
using GUI.Core;
using Persistence.Models;
using Persistence.Repositories;

namespace GUI.ViewModels;

public class BreedsViewModel : ViewModel
{
    private BreedRepository _breedRepository;
    private IEnumerable<Breed> _breeds = new List<Breed>();
    private Breed? _selectedBreed;
    private string _enteredName;

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

    public BreedsViewModel(BreedRepository breedRepository)
    {
        _breedRepository = breedRepository;
        Breeds = _breedRepository.GetAll();
    }

    public void SelectBreed(Breed breed)
    {
        SelectedBreed = breed;
    }

    public void AddBreed()
    {
        if (_enteredName.Length > 0)
        {
            Breed newBreed = new Breed() { Name = _enteredName };
            _breedRepository.Add(newBreed);
            Breeds = _breedRepository.GetAll();
        }
    }

    public void ChangeNameText(string name)
    {
        _enteredName = name;
    }

    public void UpdateBreed(Breed breed)
    {
        _breedRepository.Update(breed);
        Breeds = _breedRepository.GetAll();
        SelectedBreed = null;
        SelectedBreed = _breedRepository.Get(breed.Id);
    }

    public void DeleteBreed(Breed breed)
    {
        _breedRepository.Delete(breed.Id);
        Breeds = _breedRepository.GetAll();
        SelectedBreed = null;
    }
}
