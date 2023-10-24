using System;

namespace GUI.CustomEventArgs;

public class ResourceEventArgs<T> : EventArgs
{
    public T Resource { get;  }

    public ResourceEventArgs(T resource)
    {
        Resource = resource;
    }
}
