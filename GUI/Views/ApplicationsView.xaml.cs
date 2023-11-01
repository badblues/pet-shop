using System.Windows.Controls;
using System.Windows.Input;
using GUI.ViewModels;
using Persistence.Models;

namespace GUI.Views;

public partial class ApplicationsView : UserControl
{
    public ApplicationsView()
    {
        InitializeComponent();
    }

    private void Applications_MouseLeftButton(object sender, MouseButtonEventArgs e)
    {
        ApplicationsViewModel context = (ApplicationsViewModel)DataContext;
        DockPanel dockPanel = (DockPanel)sender;
        Application? application = dockPanel.DataContext as Application;
        context.SelectApplication(application);
    }
}
