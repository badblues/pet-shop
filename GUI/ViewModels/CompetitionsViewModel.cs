using System.Collections.Generic;
using GUI.Core;
using Persistence.Models;
using Persistence.Repositories;

namespace GUI.ViewModels;

internal class CompetitionsViewModel : ViewModel
{
    private readonly CompetitionRepository _competitionRepository;
    private IEnumerable<Competition> _competitions = new List<Competition>();

    public IEnumerable<Competition> Competitions
    {
        get => _competitions;
        set
        {
            _competitions = value;
            OnPropertyChanged(nameof(Competitions));
        }
    }

    public CompetitionsViewModel(CompetitionRepository competitionRepository)
    {
        _competitionRepository = competitionRepository;
        Competitions = _competitionRepository.GetAll();
    }
}
