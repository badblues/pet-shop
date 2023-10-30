using System.Windows;
using GUI.ViewModels;

namespace GUI.Views;

public partial class MainWindow : Window
{

    public MainWindow(MainViewModel mainVM)
    {
        DataContext = mainVM;
        InitializeComponent();
    }
}
