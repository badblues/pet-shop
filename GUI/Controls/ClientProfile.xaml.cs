using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using GUI.CustomEventArgs;
using GUI.ViewModels;
using Persistence.Models;

namespace GUI.Controls;

public partial class ClientProfile : UserControl
{
    private string _enteredName = "";
    private string _enteredAddress = "";

    public static readonly DependencyProperty ClientProperty =
        DependencyProperty.Register("Client", typeof(Client), typeof(ClientProfile));

    public Client Client
    {
        get
        {
            return (Client)GetValue(ClientProperty);
        }
        set
        {
            SetValue(ClientProperty, value);
        }
    }

    public event EventHandler<ClientEventArgs> DeleteClicked;
    public event EventHandler<ClientEventArgs> UpdateClicked;

    public ClientProfile()
    {
        InitializeComponent();
    }

    private void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        ClientEventArgs args = new ClientEventArgs(Client);
        DeleteClicked?.Invoke(this, args);
    }

    private void UpdateButton_Click(object sender, RoutedEventArgs e)
    {
        if (_enteredName.Length == 0 && _enteredAddress.Length == 0)
            return;
        Client updatedClient = Client;
        if (_enteredName.Length > 0)
            updatedClient.Name = _enteredName;
        if (_enteredAddress.Length > 0)
            updatedClient.Address = _enteredAddress;
        ClientEventArgs args = new ClientEventArgs(updatedClient);
        UpdateClicked?.Invoke(this, args);
        //TODO fix this bullshit
        richTextBoxName.Document = new FlowDocument(new Paragraph(new Run("")));
        richTextBoxAddress.Document = new FlowDocument(new Paragraph(new Run("")));
    }

    private void RichTextBoxName_TextChanged(object sender, TextChangedEventArgs e)
    {
        string text = new TextRange(richTextBoxName.Document.ContentStart, richTextBoxName.Document.ContentEnd).Text;
        text = text.TrimEnd('\r', '\n');
        _enteredName = text;
    }

    private void RichTextBoxAddress_TextChanged(object sender, TextChangedEventArgs e)
    {
        string text = new TextRange(richTextBoxAddress.Document.ContentStart, richTextBoxAddress.Document.ContentEnd).Text;
        text = text.TrimEnd('\r', '\n');
        _enteredAddress = text;
    }
}
