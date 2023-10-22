using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
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
        var context = (ClientsViewModel)DataContext;
        var clientItem = (ClientItem)sender;
        context.OnSelectClient(clientItem.Client);
    } 

    private void AddClient_Click(object sender, RoutedEventArgs e)
    {
        var context = (ClientsViewModel)DataContext;
        context.OnAddClient();
    }

    private void RichTextBoxName_TextChanged(object sender, TextChangedEventArgs e)
    {
        var context = (ClientsViewModel)DataContext;
        string text = new TextRange(richTextBoxName.Document.ContentStart, richTextBoxName.Document.ContentEnd).Text;
        text = text.TrimEnd('\r', '\n');
        context.OnNameTextChanged(text);
    }

    private void RichTextBoxAddress_TextChanged(object sender, TextChangedEventArgs e)
    {
        var context = (ClientsViewModel)DataContext;
        string text = new TextRange(richTextBoxAddress.Document.ContentStart, richTextBoxAddress.Document.ContentEnd).Text;
        text = text.TrimEnd('\r', '\n');
        context.OnAddressTextChanged(text);
    }
}
