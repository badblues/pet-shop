using System.Windows.Controls;
using System.Windows.Input;
using GUI.ViewModels;
using Persistence.Models;

namespace GUI.Views;

public partial class CompetitionsView : UserControl
{
    public CompetitionsView()
    {
        InitializeComponent();
    }

    private void Competitions_MouseLeftButton(object sender, MouseButtonEventArgs e)
    {
        CompetitionsViewModel context = (CompetitionsViewModel)DataContext;
        DockPanel dockPanel = (DockPanel)sender;
        Competition? competition = dockPanel.DataContext as Competition;
        context.SelectCompetition(competition);
    }
}
