using System.Collections.Generic;
using System.Windows.Input;
using GUI.Core;
using Persistence.Models;
using Persistence.Repositories;

namespace GUI.ViewModels;

public class ClientsViewModel : ViewModel
{
    private readonly ClientRepository _clientRepository;
    private IEnumerable<Client> _clients = new List<Client>();
    private Client? _selectedClient;
    private string _enteredName;
    private string _enteredAddress;

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

    public IEnumerable<Client> Clients
    {
        get => _clients;
        set
        {
            _clients = value;
            OnPropertyChanged(nameof(Clients));
        }
    }

    public string EnteredName
    {
        get => _enteredName;
        set
        {
            _enteredName = value;
            OnPropertyChanged(nameof(EnteredName));
        }
    }

    public string EnteredAdress
    {
        get => _enteredAddress;
        set
        {
            _enteredAddress = value;
            OnPropertyChanged(nameof(EnteredAdress));
        }
    }

    public ClientsViewModel(ClientRepository clientRepository)
    {
        _clientRepository = clientRepository;

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
        if (EnteredName?.Length > 0 && EnteredAdress?.Length > 0)
        {
            Client newClient = new() { EnteredName = EnteredName, EnteredAdress = EnteredAdress };
            _clientRepository.Add(newClient);
            Clients = _clientRepository.GetAll();
        }
    }

    public void DeleteClient(object? parameter)
    {
        if (parameter is Client client)
        {
            _clientRepository.Delete(client.Id);
            Clients = _clientRepository.GetAll();
            SelectedClient = null;
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
}
