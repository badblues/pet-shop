﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using GUI.Core;
using Persistence.Models;
using Persistence.Repositories;

namespace GUI.ViewModels;

internal class CompetitionsViewModel : ViewModel
{
    private readonly CompetitionRepository _competitionRepository;
    private readonly ParticipationRepository _participationRepository;
    private readonly AnimalRepository _animalRepository;
    private ICollection<Competition> _competitions = new List<Competition>();
    private Competition? _selectedCompetition;
    private IEnumerable<Animal> _availableAnimals = new List<Animal>();

    public ICommand AddCompetitionCommand { get; set; }
    public ICommand AddParticipationCommand { get; set; }
    public ICommand UpdateCompetitionCommand { get; set; }
    public ICommand DeleteCompetitionCommand { get; set; }
    public ICommand DeleteParticipationCommand { get; set; }

    public Competition? SelectedCompetition
    {
        get => _selectedCompetition;
        set
        {
            _selectedCompetition = value;
            OnPropertyChanged(nameof(SelectedCompetition));
        }
    }

    public ICollection<Competition> Competitions
    {
        get => _competitions;
        set
        {
            _competitions = value;
            OnPropertyChanged(nameof(Competitions));
        }
    }

    public IEnumerable<Animal> AvailableAnimals
    {
        get => _availableAnimals;
        set
        {
            _availableAnimals = value;
            OnPropertyChanged(nameof(AvailableAnimals));
        }
    }

    public string EnteredName { get; set; }
    public string EnteredLocation { get; set; }
    public DateTime? EnteredDate { get; set; }
    public Animal EnteredAnimal { get; set; }
    public string EnteredAward { get; set; }

    public CompetitionsViewModel(CompetitionRepository competitionRepository,
        AnimalRepository animalRepository,
        ParticipationRepository participationRepository)
    {
        _competitionRepository = competitionRepository;
        _animalRepository = animalRepository;
        _participationRepository = participationRepository;

        AddCompetitionCommand = new RelayCommand(AddCompetition, o => true);
        AddParticipationCommand = new RelayCommand(AddParticipation, o => true);
        UpdateCompetitionCommand = new RelayCommand(UpdateCompetition, o => true);
        DeleteCompetitionCommand = new RelayCommand(DeleteCompetition, o => true);
        DeleteParticipationCommand = new RelayCommand(DeleteParticipation, o => true);

        Competitions = _competitionRepository.GetAll();
    }

    public void SelectCompetition(Competition competition)
    {
        SelectedCompetition = competition;
        AvailableAnimals = _animalRepository.GetAll().Except(SelectedCompetition.Participations.Select(p => p.Animal));
    }

    public void AddCompetition(object? unused)
    {
        if (string.IsNullOrWhiteSpace(EnteredName)
            || string.IsNullOrWhiteSpace(EnteredLocation)
            || EnteredDate is null)
        {
            return;
        }
        Competition newCompetition = new()
        {
            Name = EnteredName,
            Location = EnteredLocation,
            Date = (DateTime)EnteredDate,
        };

        _competitionRepository.Add(newCompetition);
        Competitions = _competitionRepository.GetAll();
    }

    public void DeleteCompetition(object? parameter)
    {
        if (parameter is Competition competition)
        {
            _competitionRepository.Delete(competition.Id);
            Competitions = _competitionRepository.GetAll();
            SelectedCompetition = null;
        }
    }

    public void UpdateCompetition(object? parameter)
    {
        if (parameter is Competition competition)
        {
            _competitionRepository.Update(competition);
            Competitions = _competitionRepository.GetAll();
            SelectedCompetition = null;
            SelectedCompetition = _competitionRepository.Get(competition.Id);
        }
    }

    public void AddParticipation(object? unused)
    {
        if (string.IsNullOrWhiteSpace(EnteredAward)
            || SelectedCompetition is null
            || EnteredAnimal is null)
        {
            return;
        }
        Participation newParticipation = new()
        {
            Animal = EnteredAnimal,
            Competition = SelectedCompetition,
            Award = EnteredAward
        };

        _participationRepository.Add(newParticipation);
        SelectedCompetition.Participations.Add(newParticipation);
        AvailableAnimals = AvailableAnimals.Except(SelectedCompetition.Participations.Select(p => p.Animal));
    }

    public void DeleteParticipation(object parameter)
    {
        if (parameter is Participation participation)
        {
            _participationRepository.Delete(participation.Animal.Id, participation.Competition.Id);
            if (SelectedCompetition is not null)
            {
                SelectedCompetition.Participations.Remove(participation);
                AvailableAnimals = _animalRepository.GetAll().Except(SelectedCompetition.Participations.Select(p => p.Animal));
            }
        }
    }
}
