using System.Collections.Generic;
using GUI.Core;
using Persistence.Models;
using Persistence.Repositories;

namespace GUI.ViewModels;

internal class AnimalsViewModel : ViewModel
{
    private readonly AnimalRepository _animalRepository;
    private IEnumerable<Animal> _animals = new List<Animal>();

    public IEnumerable<Animal> Animals
    {
        get => _animals;
        set
        {
            _animals = value;
            OnPropertyChanged(nameof(Animals));
        }
    }

    public AnimalsViewModel(AnimalRepository animalRepository)
    {
        _animalRepository = animalRepository;
        Animals = _animalRepository.GetAll();
    }
}
