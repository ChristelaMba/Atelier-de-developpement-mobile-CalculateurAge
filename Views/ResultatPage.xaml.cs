using CalculateurAge.ViewModels;

namespace CalculateurAge.Views;

// Recoit le ViewModel envoye par la navigation, sous la cle "donnees".
[QueryProperty(nameof(Donnees), "donnees")]
public partial class ResultatPage : ContentPage
{
    // Quand la navigation donne le ViewModel, il devient
    // la source de tous les {Binding} de cette page.
    public CalculateurViewModel Donnees
    {
        set => BindingContext = value;
    }

    // Construit l'arbre visuel decrit par le XAML.
    public ResultatPage() => InitializeComponent();

    // ".." = revenir a la page precedente.
    private async void OnRetourClicked(object s, EventArgs e)
        => await Shell.Current.GoToAsync("..");
}