using System.Windows;
using System;
using System.Windows.Controls;
using GUI.CustomEventArgs;
using Persistence.Models;
using System.Windows.Documents;

namespace GUI.Controls;

public partial class BreedProfile : UserControl
{

    private string _enteredName = "";

    public event EventHandler<ResourceEventArgs<Breed>> DeleteClicked;
    public event EventHandler<ResourceEventArgs<Breed>> UpdateClicked;
    public static readonly DependencyProperty BreedProperty =
        DependencyProperty.Register("Breed", typeof(Breed), typeof(BreedProfile));

    public Breed Breed
    {
        get
        {
            return (Breed)GetValue(BreedProperty);
        }
        set
        {
            SetValue(BreedProperty, value);
        }
    }

    public BreedProfile()
    {
        InitializeComponent();
    }

    private void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        ResourceEventArgs<Breed> args = new ResourceEventArgs<Breed>(Breed);
        DeleteClicked?.Invoke(this, args);
    }

    private void UpdateButton_Click(object sender, RoutedEventArgs e)
    {
        if (_enteredName.Length == 0)
            return;
        Breed updatedBreed = Breed;
        if (_enteredName.Length > 0)
            updatedBreed.Name = _enteredName;
        ResourceEventArgs<Breed> args = new ResourceEventArgs<Breed>(updatedBreed);
        UpdateClicked?.Invoke(this, args);
        richTextBoxName.Document = new FlowDocument(new Paragraph(new Run("")));
    }

    private void RichTextBoxName_TextChanged(object sender, TextChangedEventArgs e)
    {
        string text = new TextRange(richTextBoxName.Document.ContentStart, richTextBoxName.Document.ContentEnd).Text;
        text = text.TrimEnd('\r', '\n');
        _enteredName = text;
    }
}
