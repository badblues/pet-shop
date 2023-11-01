using System.Collections.Generic;
using System.Windows.Input;
using GUI.Core;
using Persistence.Models;
using Persistence.Repositories;

namespace GUI.ViewModels;

internal class AnimalsViewModel : ViewModel
{
    private readonly AnimalRepository _animalRepository;
    private readonly ClientRepository _clientRepository;
    private readonly BreedRepository _breedRepository;
    private IEnumerable<Animal> _animals = new List<Animal>();
    private Animal? _selectedAnimal;

    public ICommand AddAnimalCommand { get; set; }
    public ICommand UpdateAnimalCommand { get; set; }
    public ICommand DeleteAnimalCommand { get; set; }

    public Animal? SelectedAnimal
    {
        get => _selectedAnimal;
        set
        {
            _selectedAnimal = value;
            OnPropertyChanged(nameof(SelectedAnimal));
        }
    }

    public IEnumerable<Animal> Animals
    {
        get => _animals;
        set
        {
            _animals = value;
            OnPropertyChanged(nameof(Animals));
        }
    }

    public IEnumerable<Gender> Genders { get; set; }
    public IEnumerable<Breed> Breeds { get; set; }
    public IEnumerable<Client> Clients { get; set; }

    public string EnteredName { get; set; }
    public string EnteredAge { get; set; }
    public Gender EnteredGender { get; set; }
    public Breed EnteredBreed { get; set; }
    public string EnteredExteriorDescription { get; set; }
    public string EnteredPedigree { get; set; }
    public string EnteredVeterinarian { get; set; }
    public Client EnteredOwner { get; set; }

    public AnimalsViewModel(
        AnimalRepository animalRepository,
        ClientRepository clientRepository,
        BreedRepository breedRepository)
    {
        _animalRepository = animalRepository;
        _clientRepository = clientRepository;
        _breedRepository = breedRepository;

        AddAnimalCommand = new RelayCommand(AddAnimal, o => true);
        UpdateAnimalCommand = new RelayCommand(UpdateAnimal, o => true);
        DeleteAnimalCommand = new RelayCommand(DeleteAnimal, o => true);

        Animals = _animalRepository.GetAll();
        Clients = _clientRepository.GetAll();
        Breeds = _breedRepository.GetAll();
        Genders = new List<Gender>() { Gender.Male, Gender.Female };
    }

    public void SelectAnimal(Animal animal)
    {
        SelectedAnimal = animal;
    }

    public void AddAnimal(object? unused)
    {
        if (EnteredName?.Length > 0 && EnteredBreed is not null)
        {
            Animal newAnimal = new()
            {
                Name = EnteredName,
                Gender = EnteredGender,
                BreedId = EnteredBreed.Id,
                Breed = EnteredBreed,
                ExteriorDescription = EnteredExteriorDescription,
                Pedigree = EnteredPedigree,
                Veterinarian = EnteredVeterinarian,
                ClientId = EnteredOwner?.Id,
                Client = EnteredOwner
            };
            if (EnteredAge?.Length > 0)
            {
                newAnimal.Age = int.Parse(EnteredAge);
            }

            _animalRepository.Add(newAnimal);
            Animals = _animalRepository.GetAll();
        }
    }

    public void DeleteAnimal(object? parameter)
    {
        if (parameter is Animal animal)
        {
            _animalRepository.Delete(animal.Id);
            Animals = _animalRepository.GetAll();
            SelectedAnimal = null;
        }
    }

    public void UpdateAnimal(object? parameter)
    {
        if (parameter is Animal animal)
        {
            _animalRepository.Update(animal);
            Animals = _animalRepository.GetAll();
            SelectedAnimal = null;
            SelectedAnimal = _animalRepository.Get(animal.Id);
        }
    }
}
