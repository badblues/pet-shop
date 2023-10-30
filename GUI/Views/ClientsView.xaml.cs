using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using GUI.CustomEventArgs;
using GUI.ViewModels;
using Persistence.Models;

namespace GUI.Views;

public partial class ClientsView : UserControl
{
    private readonly ClientsViewModel _context;

    public ClientsView()
    {
        InitializeComponent();
        _context = (ClientsViewModel)DataContext;
    }

    private void Clients_MouseLeftButton(object sender, MouseButtonEventArgs e)
    {
        DockPanel dockPanel = (DockPanel)sender;
        Client? client = dockPanel.DataContext as Client;
        _context.SelectClient(client);
    }

    private void AddClient_Click(object sender, RoutedEventArgs e)
    {
        _context.AddClient();
    }

    private void RichTextBoxName_TextChanged(object sender, TextChangedEventArgs e)
    {
        string text = new TextRange(richTextBoxName.Document.ContentStart, richTextBoxName.Document.ContentEnd).Text;
        text = text.TrimEnd('\r', '\n');
        _context.ChangeNameText(text);
    }

    private void RichTextBoxAddress_TextChanged(object sender, TextChangedEventArgs e)
    {
        string text = new TextRange(richTextBoxAddress.Document.ContentStart, richTextBoxAddress.Document.ContentEnd).Text;
        text = text.TrimEnd('\r', '\n');
        _context.ChangeAddressText(text);
    }

    private void ClientProfile_DeleteClicked(object sender, ResourceEventArgs<Client> e)
    {
        _context.DeleteClient(e.Resource);
    }

    private void ClientProfile_UpdateClicked(object sender, ResourceEventArgs<Client> e)
    {
        _context.UpdateClient(e.Resource);
    }
}
