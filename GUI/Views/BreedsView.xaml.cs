using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using GUI.Controls;
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
        var context = (BreedsViewModel)DataContext;
        var breedItem = (BreedItem)sender;
        context.SelectBreed(breedItem.Breed);
    }

    private void AddBreed_Click(object sender, RoutedEventArgs e)
    {
        var context = (BreedsViewModel)DataContext;
        context.AddBreed();
    }

    private void RichTextBoxName_TextChanged(object sender, TextChangedEventArgs e)
    {
        var context = (BreedsViewModel)DataContext;
        string text = new TextRange(richTextBoxName.Document.ContentStart, richTextBoxName.Document.ContentEnd).Text;
        text = text.TrimEnd('\r', '\n');
        context.ChangeNameText(text);
    }

    private void BreedProfile_DeleteClicked(object sender, ResourceEventArgs<Breed> e)
    {
        var context = (BreedsViewModel)DataContext;
        context.DeleteBreed(e.Resource);
    }

    private void BreedProfile_UpdateClicked(object sender, ResourceEventArgs<Breed> e)
    {
        var context = (BreedsViewModel)DataContext;
        context.UpdateBreed(e.Resource);
    }

}
