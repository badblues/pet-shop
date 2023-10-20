using System.Collections.Generic;
using System.ComponentModel;
using Persistence.Models;
using Persistence.Repositories;

namespace GUI.ViewModels;

public class MainVM : INotifyPropertyChanged
{

    public event PropertyChangedEventHandler? PropertyChanged;
    public IEnumerable<Client> Clients { get; set; }
    private ClientRepository _clientRepository;

    public MainVM(ClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
        Clients = _clientRepository.GetAll();
    }

    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
