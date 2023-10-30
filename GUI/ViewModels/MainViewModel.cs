using GUI.Core;
using GUI.Services;

namespace GUI.ViewModels;

public class MainViewModel : ViewModel
{
    private INavigationService _navigationService;

    public INavigationService NavigationService
    {
        get => _navigationService;
        set
        {
            _navigationService = value;
            OnPropertyChanged(nameof(_navigationService));
        }
    }

    public RelayCommand NavigateToHomeCommand { get; set; }
    public RelayCommand NavigateToClientsCommand { get; set; }
    public RelayCommand NavigateToBreedsCommand { get; set; }
    public RelayCommand NavigateToEmployeesCommand { get; set; }

    public MainViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
        NavigateToHomeCommand = new RelayCommand(o => NavigationService.NavigateTo<HomeViewModel>(), o => true);
        NavigateToClientsCommand = new RelayCommand(o => NavigationService.NavigateTo<ClientsViewModel>(), o => true);
        NavigateToBreedsCommand = new RelayCommand(o => NavigationService.NavigateTo<BreedsViewModel>(), o => true);
        NavigateToEmployeesCommand = new RelayCommand(o => NavigationService.NavigateTo<EmployeesViewModel>(), o => true);
        NavigateToHomeCommand.Execute(null);
    }
}

