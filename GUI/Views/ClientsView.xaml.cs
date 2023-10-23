﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using GUI.Controls;
using GUI.CustomEventArgs;
using GUI.ViewModels;
using Persistence.Models;

namespace GUI.Views;

//TODO context
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
        context.SelectClient(clientItem.Client);
    } 

    private void AddClient_Click(object sender, RoutedEventArgs e)
    {
        var context = (ClientsViewModel)DataContext;
        context.AddClient();
    }

    private void RichTextBoxName_TextChanged(object sender, TextChangedEventArgs e)
    {
        var context = (ClientsViewModel)DataContext;
        string text = new TextRange(richTextBoxName.Document.ContentStart, richTextBoxName.Document.ContentEnd).Text;
        text = text.TrimEnd('\r', '\n');
        context.ChangeNameText(text);
    }

    private void RichTextBoxAddress_TextChanged(object sender, TextChangedEventArgs e)
    {
        var context = (ClientsViewModel)DataContext;
        string text = new TextRange(richTextBoxAddress.Document.ContentStart, richTextBoxAddress.Document.ContentEnd).Text;
        text = text.TrimEnd('\r', '\n');
        context.ChangeAddressText(text);
    }

    private void ClientProfile_DeleteClicked(object sender, ClientEventArgs e)
    {
        var context = (ClientsViewModel)DataContext;
        context.DeleteClient(e.Client);
    }

    private void ClientProfile_UpdateClicked(object sender, ClientEventArgs e)
    {
        var context = (ClientsViewModel)DataContext;
        context.UpdateClient(e.Client);
    }
}
