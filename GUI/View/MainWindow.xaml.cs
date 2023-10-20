using System.Windows;
using GUI.ViewModels;

namespace GUI.View;

public partial class MainWindow : Window
{
    public MainWindow(MainVM mainVM)
    {
        DataContext = mainVM;
        InitializeComponent();
    }
}
