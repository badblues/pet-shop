using System.Collections.Generic;
using GUI.Core;
using Persistence.Models;
using Persistence.Repositories;

namespace GUI.ViewModels;

internal class ApplicationsViewModel : ViewModel
{
    private readonly ApplicationRepository _applicationRepository;
    private IEnumerable<Application> _applications = new List<Application>();

    public IEnumerable<Application> Applications
    {
        get => _applications;
        set
        {
            _applications = value;
            OnPropertyChanged(nameof(Applications));
        }
    }

    public ApplicationsViewModel(ApplicationRepository applicationRepository)
    {
        _applicationRepository = applicationRepository;
        Applications = _applicationRepository.GetAll();
    }
}
