using System.Collections.Generic;
using System.Windows.Input;
using GUI.Core;
using Persistence.Models;
using Persistence.Repositories;

namespace GUI.ViewModels;

public class ClientsViewModel : ViewModel
{
    public Client SelectedClient
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
        IEnumerator<Client> enumerator = Clients.GetEnumerator();
        enumerator.MoveNext();
        SelectedClient = enumerator.Current;
    }

    public void OnSelectClient(Client client)
    {
        SelectedClient = client;
    }

    private ClientRepository _clientRepository;
    private IEnumerable<Client> _clients;
    private Client _selectedClient;

}
