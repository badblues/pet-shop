using System.Windows.Controls;
using System.Windows.Input;
using GUI.ViewModels;
using Persistence.Models;

namespace GUI.Views;

public partial class ClientsView : UserControl
{

    public ClientsView()
    {
        InitializeComponent();
    }

    private void Clients_MouseLeftButton(object sender, MouseButtonEventArgs e)
    {
        ClientsViewModel context = (ClientsViewModel)DataContext;
        DockPanel dockPanel = (DockPanel)sender;
        Client? client = dockPanel.DataContext as Client;
        context.SelectClient(client);
    }
}
