using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Persistence.Models;

namespace GUI.Controls;

public partial class ClientProfile : UserControl
{
    public static readonly DependencyProperty DeleteCommandProperty =
        DependencyProperty.Register("DeleteCommand", typeof(ICommand), typeof(ClientProfile));
    public static readonly DependencyProperty UpdateCommandProperty =
        DependencyProperty.Register("UpdateCommand", typeof(ICommand), typeof(ClientProfile));
    public static readonly DependencyProperty ClientProperty =
        DependencyProperty.Register("Client", typeof(Client), typeof(ClientProfile));

    public ICommand DeleteCommand
    {
        get => (ICommand)GetValue(DeleteCommandProperty);
        set => SetValue(DeleteCommandProperty, value);
    }

    public ICommand UpdateCommand
    {
        get => (ICommand)GetValue(UpdateCommandProperty);
        set => SetValue(UpdateCommandProperty, value);
    }

    public Client Client
    {
        get => (Client)GetValue(ClientProperty);
        set => SetValue(ClientProperty, value);
    }

    public string EnteredName { get; set; }
    public string EnteredAddress { get; set; }

    public ClientProfile()
    {
        InitializeComponent();
    }

    private void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        if (DeleteCommand != null && DeleteCommand.CanExecute(null))
        {
            DeleteCommand.Execute(Client);
        }
    }

    private void UpdateButton_Click(object sender, RoutedEventArgs e)
    {
        if (EnteredName?.Length == 0 && EnteredAddress?.Length == 0)
        {
            return;
        }

        if (UpdateCommand != null && UpdateCommand.CanExecute(null))
        {
            if (EnteredName?.Length > 0)
            {
                Client.Name = EnteredName;
            }

            if (EnteredAddress?.Length > 0)
            {
                Client.Address = EnteredAddress;
            }
            UpdateCommand.Execute(Client);
        }
    }
}
