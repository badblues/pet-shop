using System.Collections.Generic;
using System.Linq;
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
    private readonly ParticipationRepository _participationRepository;
    private readonly CompetitionRepository _competitionRepository;
    private ICollection<Animal> _animals = new List<Animal>();
    private ICollection<Animal> _filteredAnimals = new List<Animal>();
    private IEnumerable<Competition> _availableCompetitions = new List<Competition>();
    private Animal? _selectedAnimal;
    private string _searchText;

    public ICommand AddAnimalCommand { get; set; }
    public ICommand AddParticipationCommand { get; set; }
    public ICommand UpdateAnimalCommand { get; set; }
    public ICommand DeleteAnimalCommand { get; set; }
    public ICommand DeleteParticipationCommand { get; set; }

    public Animal? SelectedAnimal
    {
        get => _selectedAnimal;
        set
        {
            _selectedAnimal = value;
            OnPropertyChanged(nameof(SelectedAnimal));
        }
    }

    public ICollection<Animal> Animals
    {
        get => _animals;
        set
        {
            _animals = value;
            OnPropertyChanged(nameof(Animals));
            FilterAnimals();
        }
    }

    public ICollection<Animal> FilteredAnimals
    {
        get => _filteredAnimals;
        set
        {
            _filteredAnimals = value;
            OnPropertyChanged(nameof(FilteredAnimals));
        }
    }

    public IEnumerable<Competition> AvailableCompetitions
    {
        get => _availableCompetitions;
        set
        {
            _availableCompetitions = value;
            OnPropertyChanged(nameof(AvailableCompetitions));
        }
    }

    public ICollection<Gender> Genders { get; set; }
    public ICollection<Breed> Breeds { get; set; }
    public ICollection<Client> Clients { get; set; }

    public string EnteredName { get; set; }
    public string EnteredAge { get; set; }
    public Gender? EnteredGender { get; set; }
    public Breed EnteredBreed { get; set; }
    public string EnteredExteriorDescription { get; set; }
    public string EnteredPedigree { get; set; }
    public string EnteredVeterinarian { get; set; }
    public Client EnteredOwner { get; set; }
    public Competition EnteredCompetition { get; set; }
    public string EnteredAward { get; set; }

    public string SearchText
    {
        get => _searchText;
        set
        {
            if (_searchText != value)
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
                FilterAnimals();
            }
        }
    }

    public AnimalsViewModel(
        AnimalRepository animalRepository,
        ClientRepository clientRepository,
        BreedRepository breedRepository,
        ParticipationRepository participationRepository,
        CompetitionRepository competitionRepository)
    {
        _animalRepository = animalRepository;
        _clientRepository = clientRepository;
        _breedRepository = breedRepository;
        _participationRepository = participationRepository;
        _competitionRepository = competitionRepository;

        AddAnimalCommand = new RelayCommand(AddAnimal, o => true);
        AddParticipationCommand = new RelayCommand(AddParticipation, o => true);
        UpdateAnimalCommand = new RelayCommand(UpdateAnimal, o => true);
        DeleteAnimalCommand = new RelayCommand(DeleteAnimal, o => true);
        DeleteParticipationCommand = new RelayCommand(DeleteParticipation, o => true);

        Animals = _animalRepository.GetAll();
        Clients = _clientRepository.GetAll();
        Breeds = _breedRepository.GetAll();
        Genders = new List<Gender>() { Gender.Male, Gender.Female };
        FilterAnimals();
    }

    public void SelectAnimal(Animal animal)
    {
        SelectedAnimal = animal;
        AvailableCompetitions = _competitionRepository.GetAll().Except(SelectedAnimal.Participations.Select(p => p.Competition));
    }

    public void AddAnimal(object? unused)
    {
        if (string.IsNullOrWhiteSpace(EnteredName)
            || EnteredBreed is null
            || EnteredGender is null)
        {
            return;
        }
        Animal newAnimal = new()
        {
            Name = EnteredName,
            Age = EnteredAge is null ? null : int.Parse(EnteredAge),
            Gender = (Gender)EnteredGender,
            Breed = EnteredBreed,
            ExteriorDescription = EnteredExteriorDescription,
            Pedigree = EnteredPedigree,
            Veterinarian = EnteredVeterinarian,
            Client = EnteredOwner
        };

        _animalRepository.Add(newAnimal);
        Animals = _animalRepository.GetAll();
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

    public void DeleteAnimal(object? parameter)
    {
        if (parameter is Animal animal)
        {
            foreach (Participation participation in animal.Participations)
                _participationRepository.Delete(participation.Animal.Id, participation.Competition.Id);
            _animalRepository.Delete(animal.Id);
            Animals = _animalRepository.GetAll();
            SelectedAnimal = null;
        }
    }

    public void AddParticipation(object? unused)
    {
        if (string.IsNullOrWhiteSpace(EnteredAward)
            || EnteredCompetition is null
            || SelectedAnimal is null)
        {
            return;
        }
        Participation newParticipation = new()
        {
            Animal = SelectedAnimal,
            Competition = EnteredCompetition,
            Award = EnteredAward
        };

        _participationRepository.Add(newParticipation);
        SelectedAnimal.Participations.Add(newParticipation);
        AvailableCompetitions = AvailableCompetitions.Except(SelectedAnimal.Participations.Select(p => p.Competition));
    }

    public void DeleteParticipation(object parameter)
    {
        if (parameter is Participation participation)
        {
            _participationRepository.Delete(participation.Animal.Id, participation.Competition.Id);
            if (SelectedAnimal is not null)
            {
                SelectedAnimal.Participations.Remove(participation);
                AvailableCompetitions = _competitionRepository.GetAll().Except(SelectedAnimal.Participations.Select(p => p.Competition));
            }
        }
    }

    private void FilterAnimals()
    {
        ICollection<Animal> filteredAnimals = new List<Animal>();

        if (string.IsNullOrWhiteSpace(SearchText))
        {
            foreach (Animal animal in Animals)
            {
                filteredAnimals.Add(animal);
            }
        }
        else
        {
            string searchTextLower = SearchText.ToLower();
            foreach (Animal animal in Animals)
            {
                if (animal.Name.ToLower().Contains(searchTextLower)
                    || animal.Breed.Name.ToLower().Contains(searchTextLower))
                {
                    filteredAnimals.Add(animal);
                }
            }
        }

        FilteredAnimals = filteredAnimals;
    }
}
