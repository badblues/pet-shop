using System;
using System.Collections.Generic;
using System.Windows.Input;
using GUI.Core;
using Persistence.Models;
using Persistence.Repositories;

namespace GUI.ViewModels;

internal class ApplicationsViewModel : ViewModel
{
    private readonly ApplicationRepository _applicationRepository;
    private readonly ClientRepository _clientRepository;
    private readonly BreedRepository _breedRepository;
    private readonly EmployeeRepository _employeeRepository;
    private ICollection<Application> _applications = new List<Application>();
    private ICollection<Application> _filteredApplications = new List<Application>();
    private Application? _selectedApplication;
    private string _searchText;
    private bool _filterNotCompleted = false;

    public ICommand AddApplicationCommand { get; set; }
    public ICommand UpdateApplicationCommand { get; set; }
    public ICommand ChangeApplicationStatusCommand { get; set; }
    public ICommand DeleteApplicationCommand { get; set; }

    public Application? SelectedApplication
    {
        get => _selectedApplication;
        set
        {
            _selectedApplication = value;
            OnPropertyChanged(nameof(SelectedApplication));
        }
    }

    public ICollection<Application> Applications
    {
        get => _applications;
        set
        {
            _applications = value;
            OnPropertyChanged(nameof(Applications));
            FilterApplications();
        }
    }
    
    public ICollection<Application> FilteredApplications
    {
        get => _filteredApplications;
        set
        {
            _filteredApplications = value;
            OnPropertyChanged(nameof(FilteredApplications));
        }
    }

    public ICollection<Gender> Genders { get; set; }
    public ICollection<Breed> Breeds { get; set; }
    public ICollection<Client> Clients { get; set; }
    public ICollection<Employee> Employees { get; set; }

    public Client EnteredClient { get; set; }
    public Employee EnteredEmployee { get; set; }
    public Breed EnteredBreed { get; set; }
    public Gender? EnteredGender { get; set; }

    public string SearchText
    {
        get => _searchText;
        set
        {
            if (_searchText != value)
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
                FilterApplications();
            }
        }
    }

    public bool FilterNotCompleted
    {
        get => _filterNotCompleted;
        set
        {
            if (_filterNotCompleted != value)
            {
                _filterNotCompleted = value;
                OnPropertyChanged(nameof(FilterNotCompleted));
                FilterApplications();
            }
        }
    }

    public ApplicationsViewModel(
        ApplicationRepository applicationRepository,
        ClientRepository clientRepository,
        BreedRepository breedRepository,
        EmployeeRepository employeeRepository)
    {
        _applicationRepository = applicationRepository;
        _clientRepository = clientRepository;
        _breedRepository = breedRepository;
        _employeeRepository = employeeRepository;

        AddApplicationCommand = new RelayCommand(AddApplication, o => true);
        UpdateApplicationCommand = new RelayCommand(UpdateApplication, o => true);
        ChangeApplicationStatusCommand = new RelayCommand(ChangeApplicationStatus, o => true);
        DeleteApplicationCommand = new RelayCommand(DeleteApplication, o => true);

        Clients = _clientRepository.GetAll();
        Breeds = _breedRepository.GetAll();
        Employees = _employeeRepository.GetAll();
        Genders = new List<Gender>() { Gender.Male, Gender.Female };
        Applications = _applicationRepository.GetAll();
        FilterApplications();
    }

    public void SelectApplication(Application application)
    {
        SelectedApplication = application;
    }

    public void AddApplication(object? unused)
    {
        if (EnteredClient is null
            || EnteredEmployee is null
            || EnteredBreed is null)
        {
            return;
        }
        Application newApplication = new()
        {
            Client = EnteredClient,
            Employee = EnteredEmployee,
            Breed = EnteredBreed,
            Gender = EnteredGender,
            ApplicationDate = DateTime.Now,
            Completed = false
        };

        _applicationRepository.Add(newApplication);
        Applications = _applicationRepository.GetAll();
    }

    public void DeleteApplication(object? parameter)
    {
        if (parameter is Application application)
        {
            _applicationRepository.Delete(application.Id);
            Applications = _applicationRepository.GetAll();
            SelectedApplication = null;
        }
    }

    public void UpdateApplication(object? parameter)
    {
        if (parameter is Application application)
        {
            _applicationRepository.Update(application);
            Applications = _applicationRepository.GetAll();
            SelectedApplication = null;
            SelectedApplication = _applicationRepository.Get(application.Id);
        }
    }

    public void ChangeApplicationStatus(object? parameter)
    {
        if (parameter is Application application)
        {
            application.Completed = !application.Completed;
            _applicationRepository.Update(application);
            Applications = _applicationRepository.GetAll();
            SelectedApplication = null;
            SelectedApplication = _applicationRepository.Get(application.Id);
        }
    }

    private void FilterApplications()
    {
        ICollection<Application> filteredApplications = new List<Application>();

        if (string.IsNullOrWhiteSpace(SearchText))
        {
            foreach (Application application in Applications)
            {
                if (FilterNotCompleted && !application.Completed || !FilterNotCompleted)
                    filteredApplications.Add(application);
            }
        }
        else
        {
            string searchTextLower = SearchText.ToLower();
            foreach (Application application in Applications)
            {
                if (application.Breed.Name.ToLower().Contains(searchTextLower)
                    && (FilterNotCompleted && !application.Completed || !FilterNotCompleted))
                {
                    filteredApplications.Add(application);
                }
            }
        }

        FilteredApplications = filteredApplications;
    }
}
