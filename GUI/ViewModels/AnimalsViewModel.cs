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
    private IEnumerable<Animal> _animals = new List<Animal>();
    private Animal? _selectedAnimal;
    private IEnumerable<Participation> _participations = new List<Participation>();
    private IEnumerable<Competition> _availableCompetitions = new List<Competition>();

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

    public IEnumerable<Animal> Animals
    {
        get => _animals;
        set
        {
            _animals = value;
            OnPropertyChanged(nameof(Animals));
        }
    }

    public IEnumerable<Participation> Participations
    {
        get => _participations;
        set
        {
            _participations = value;
            OnPropertyChanged(nameof(Participations));
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

    public IEnumerable<Gender> Genders { get; set; }
    public IEnumerable<Breed> Breeds { get; set; }
    public IEnumerable<Client> Clients { get; set; }

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
    }

    public void SelectAnimal(Animal animal)
    {
        SelectedAnimal = animal;
        Participations = _participationRepository.GetByAnimalId(SelectedAnimal.Id);
        AvailableCompetitions = _competitionRepository.GetAll().Except(Participations.Select(p => p.Competition));
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
        Participations = _participationRepository.GetAll();
        AvailableCompetitions = AvailableCompetitions.Except(Participations.Select(p => p.Competition));
    }

    public void DeleteParticipation(object parameter)
    {
        if (parameter is Participation participation)
        {
            _participationRepository.Delete(participation.Animal.Id, participation.Competition.Id);
            if (SelectedAnimal is not null)
            {
                Participations = _participationRepository.GetByAnimalId(SelectedAnimal.Id);
                AvailableCompetitions = _competitionRepository.GetAll().Except(Participations.Select(p => p.Competition));
            }
        }
    }
}
