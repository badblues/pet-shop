using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;
using System.Windows.Media.Animation;
using GUI.Core;
using Persistence.Models;
using Persistence.Repositories;

namespace GUI.ViewModels;

public class ClientsViewModel : ViewModel
{
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

    public ClientsViewModel(ClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
        Clients = _clientRepository.GetAll();
    }

    public void SelectClient(Client client)
    {
        SelectedClient = client;
    }

    public void AddClient()
    {
        if (_enteredName.Length > 0 && _enteredAddress.Length > 0)
        {
            Client newClient = new Client() { Name = _enteredName, Address = _enteredAddress };
            _clientRepository.Add(newClient);
            Clients = _clientRepository.GetAll();
        }
    }

    public void ChangeNameText(string name)
    {
        _enteredName = name;
    }

    public void ChangeAddressText(string address)
    {
        _enteredAddress = address;
    }

    public void DeleteClient(Client client)
    {
        _clientRepository.Delete(client.Id);
        Clients = _clientRepository.GetAll();
        SelectedClient = null;
    }

    public void UpdateClient(Client client)
    {
        _clientRepository.Update(client);
        Clients = _clientRepository.GetAll();
        SelectedClient = null;
        SelectedClient = _clientRepository.Get(client.Id);
    }

    private ClientRepository _clientRepository;
    private IEnumerable<Client> _clients;
    private Client _selectedClient;
    private string _enteredName = "";
    private string _enteredAddress = "";

}
