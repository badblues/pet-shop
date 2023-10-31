using System.Windows.Controls;
using System.Windows.Input;
using GUI.ViewModels;
using Persistence.Models;

namespace GUI.Views;

public partial class AnimalsView : UserControl
{
    public AnimalsView()
    {
        InitializeComponent();
    }

    private void Animals_MouseLeftButton(object sender, MouseButtonEventArgs e)
    {
        AnimalsViewModel context = (AnimalsViewModel)DataContext;
        DockPanel dockPanel = (DockPanel)sender;
        Animal? animal = dockPanel.DataContext as Animal;
        context.SelectAnimal(animal);
    }

    private void PositiveIntegerTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        if (!int.TryParse(e.Text, out _))
        {
            e.Handled = true;
        }
    }
}
