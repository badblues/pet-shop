using System.Collections.Generic;
using GUI.Core;
using Persistence.Models;
using Persistence.Repositories;

namespace GUI.ViewModels;

public class ClientsViewModel : ViewModel
{
    private ClientRepository _clientRepository;
    private IEnumerable<Client> _clients;
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

}
