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
    private string _name;
    private string _address;

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

    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            OnPropertyChanged(nameof(Name));
        }
    }

    public string Address
    {
        get => _address;
        set
        {
            _address = value;
            OnPropertyChanged(nameof(Address));
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
        if (Name.Length > 0 && Address.Length > 0)
        {
            Client newClient = new() { Name = Name, Address = Address };
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
