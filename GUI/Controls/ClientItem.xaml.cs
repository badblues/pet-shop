using System.Windows;
using System.Windows.Controls;
using Persistence.Models;

namespace GUI.Controls;

public partial class ClientItem : UserControl
{

    public static readonly DependencyProperty ClientProperty =
        DependencyProperty.Register("Client", typeof(Client), typeof(ClientItem));
    public Client Client
    {
        get { return (Client)GetValue(ClientProperty); }
        set { SetValue(ClientProperty, value); }
    }

    public ClientItem()
    {
        InitializeComponent();
    }
}
