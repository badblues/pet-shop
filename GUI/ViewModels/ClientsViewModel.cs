using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using GUI.Core;
using NHibernate.Util;
using Persistence.Models;
using Persistence.Repositories;
using Application = Persistence.Models.Application;

namespace GUI.ViewModels;

public class ClientsViewModel : ViewModel
{
    private readonly ClientRepository _clientRepository;
    private readonly AnimalRepository _animalRepository;
    private readonly ApplicationRepository _applicationRepository;
    private readonly ParticipationRepository _participationRepository;
    private ICollection<Client> _clients = new List<Client>();
    private Client? _selectedClient;

    public ICommand AddClientCommand { get; set; }
    public ICommand UpdateClientCommand { get; set; }
    public ICommand DeleteClientCommand { get; set; }

    public Client? SelectedClient
    {
        get => _selectedClient;
        set
        {
            _selectedClient = value;
            OnPropertyChanged(nameof(SelectedClient));
        }
    }

    public ICollection<Client> Clients
    {
        get => _clients;
        set
        {
            _clients = value;
            OnPropertyChanged(nameof(Clients));
        }
    }

    public string EnteredName { get; set; }

    public string EnteredAddress { get; set; }

    public ClientsViewModel(
        ClientRepository clientRepository,
        AnimalRepository animalRepository,
        ApplicationRepository applicationRepository,
        ParticipationRepository participationRepository)
    {
        _clientRepository = clientRepository;
        _animalRepository = animalRepository;
        _applicationRepository = applicationRepository;
        _participationRepository = participationRepository;

        AddClientCommand = new RelayCommand(AddClient, o => true);
        UpdateClientCommand = new RelayCommand(UpdateClient, o => true);
        DeleteClientCommand = new RelayCommand(DeleteClient, o => true);

        Clients = _clientRepository.GetAll();
    }

    public void SelectClient(Client client)
    {
        SelectedClient = client;
    }

    public void AddClient(object? unused)
    {
        if (EnteredName?.Length > 0 && EnteredAddress?.Length > 0)
        {
            Client newClient = new() { Name = EnteredName, Address = EnteredAddress };
            _clientRepository.Add(newClient);
            Clients = _clientRepository.GetAll();
        }
    }

    public void UpdateClient(object? parameter)
    {
        if (parameter is Client client)
        {
            _clientRepository.Update(client);
            Clients = _clientRepository.GetAll();
            SelectedClient = null;
            SelectedClient = _clientRepository.Get(client.Id);
        }
    }

    public void DeleteClient(object? parameter)
    {
        if (parameter is Client client)
        {
            string message = "This action will delete all animals and applications belonging to client, continue?";
            MessageBoxResult result = MessageBox.Show(message, "Confirmation", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (result == MessageBoxResult.Cancel)
            {
                return;
            }

            foreach(Animal animal in client.Animals)
            {
                foreach (Participation participation in animal.Participations)
                    _participationRepository.Delete(participation.Animal.Id, participation.Competition.Id);
                _animalRepository.Delete(animal.Id);
            }
            foreach(Application application in client.Applications)
                _applicationRepository.Delete(application.Id);
            _clientRepository.Delete(client.Id);
            Clients = _clientRepository.GetAll();
            SelectedClient = null;
        }
    }
}
