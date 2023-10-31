using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using GUI.CustomEventArgs;
using GUI.ViewModels;
using Persistence.Models;

namespace GUI.Views;

public partial class BreedsView : UserControl
{
    public BreedsView()
    {
        InitializeComponent();
    }

    private void Breeds_MouseLeftButton(object sender, MouseButtonEventArgs e)
    {
        BreedsViewModel context = (BreedsViewModel)DataContext;
        DockPanel dockPanel = (DockPanel)sender;
        Breed? breed = dockPanel.DataContext as Breed;
        context.SelectBreed(breed);
    }
}
