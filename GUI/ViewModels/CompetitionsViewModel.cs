using System;
using System.Collections.Generic;
using System.Windows.Input;
using GUI.Core;
using Persistence.Models;
using Persistence.Repositories;

namespace GUI.ViewModels;

internal class CompetitionsViewModel : ViewModel
{
    private readonly CompetitionRepository _competitionRepository;
    private IEnumerable<Competition> _competitions = new List<Competition>();
    private Competition? _selectedCompetition;

    public ICommand AddCompetitionCommand { get; set; }
    public ICommand UpdateCompetitionCommand { get; set; }
    public ICommand DeleteCompetitionCommand { get; set; }

    public Competition? SelectedCompetition
    {
        get => _selectedCompetition;
        set
        {
            _selectedCompetition = value;
            OnPropertyChanged(nameof(SelectedCompetition));
        }
    }

    public IEnumerable<Competition> Competitions
    {
        get => _competitions;
        set
        {
            _competitions = value;
            OnPropertyChanged(nameof(Competitions));
        }
    }

    public string EnteredName { get; set; }
    public string EnteredLocation { get; set; }
    public DateTime? EnteredDate { get; set; }

    public CompetitionsViewModel(CompetitionRepository competitionRepository)
    {
        _competitionRepository = competitionRepository;

        AddCompetitionCommand = new RelayCommand(AddCompetition, o => true);
        UpdateCompetitionCommand = new RelayCommand(UpdateCompetition, o => true);
        DeleteCompetitionCommand = new RelayCommand(DeleteCompetition, o => true);

        Competitions = _competitionRepository.GetAll();
    }

    public void SelectCompetition(Competition competition)
    {
        SelectedCompetition = competition;
    }

    public void AddCompetition(object? unused)
    {
        if (EnteredName is null
            || EnteredName.Length == 0
            || EnteredLocation is null
            || EnteredLocation.Length == 0
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
}
