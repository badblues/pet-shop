﻿using System.Windows;
using System.Windows.Controls;
using Persistence.Models;

namespace GUI.Controls;

public partial class ClientProfile : UserControl
{
    public Client Client
    {
        get { return (Client)GetValue(ClientProperty); }
        set { SetValue(ClientProperty, value); }
    }

    public static readonly DependencyProperty ClientProperty =
        DependencyProperty.Register("Client", typeof(Client), typeof(ClientProfile));

    public ClientProfile()
    {
        InitializeComponent();
    }
}
