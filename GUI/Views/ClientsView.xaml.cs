using System.Windows.Controls;
using System.Windows.Input;
using GUI.Controls;
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
        if (DataContext is ClientsViewModel)
        {
            var context = (ClientsViewModel)DataContext;
            var clientItem = (ClientItem)sender;
            context.OnSelectClient(clientItem.Client);
        }

    } 
}
