using System.Collections.Generic;
using GUI.Core;
using Persistence.Models;
using Persistence.Repositories;

namespace GUI.ViewModels;

public class BreedsViewModel : ViewModel
{
    public IEnumerable<Breed> Breeds
    {
        get => _breeds;
        set
        {
            _breeds = value;
            OnPropertyChanged(nameof(Breeds));
        }
    }

    public BreedsViewModel(BreedRepository breedRepository)
    {
        _breedRepository = breedRepository;
        Breeds = _breedRepository.GetAll();
    }

    private BreedRepository _breedRepository;
    private IEnumerable<Breed> _breeds = new List<Breed>();
}
