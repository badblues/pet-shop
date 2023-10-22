using System;
using System.Windows;
using System.Windows.Controls;
using GUI.CustomEventArgs;
using Persistence.Models;

namespace GUI.Controls;

public partial class ClientProfile : UserControl
{

    public static readonly DependencyProperty ClientProperty =
        DependencyProperty.Register("Client", typeof(Client), typeof(ClientProfile));
    public Client Client
    {
        get { return (Client)GetValue(ClientProperty); }
        set { SetValue(ClientProperty, value); }
    }

    public event EventHandler<ClientEventArgs> DeleteClicked;

    public ClientProfile()
    {
        InitializeComponent();
    }

    private void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        ClientEventArgs args = new ClientEventArgs(Client);
        DeleteClicked?.Invoke(this, args);
    }
}
