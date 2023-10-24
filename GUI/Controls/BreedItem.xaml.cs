using System.Windows;
using System.Windows.Controls;
using Persistence.Models;

namespace GUI.Controls;

public partial class BreedItem : UserControl
{
    public Breed Breed
    {
        get { return (Breed)GetValue(BreedProperty); }
        set { SetValue(BreedProperty, value);}
    }

    public static readonly DependencyProperty BreedProperty =
        DependencyProperty.Register("Breed", typeof(Breed), typeof(BreedItem));


    public BreedItem()
    {
        InitializeComponent();
    }
}
