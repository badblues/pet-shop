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
    private Application? _selectedApplication;

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
}
