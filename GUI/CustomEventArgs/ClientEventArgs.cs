using System;
using Persistence.Models;

namespace GUI.CustomEventArgs;

public class ClientEventArgs : EventArgs
{
    public Client Client { get;  }

    public ClientEventArgs(Client client)
    {
        Client = client;
    }
}
