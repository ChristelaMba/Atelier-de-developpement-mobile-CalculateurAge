// pour ObservableCollection
using System.Collections.ObjectModel;
// pour les dates et nombres en français
using System.Globalization;

namespace CalculateurAge.ViewModels;

// Contient l ETAT de l ecran et les ACTIONS possibles.
public class CalculateurViewModel : BaseViewModel
{
    // culture française
    private static readonly CultureInfo fr = CultureInfo.GetCultureInfo("fr-FR");

    // Champs prives : la vraie donnee.
    private string _nom = "";
    private DateTime _dateNaissance = DateTime.Today.AddYears(-20);
    private string _resultat = "";
    private bool _resultatVisible;
    private string _message = "";
    private string _joursRestants = "";
    private string _ageAnnees = "";
    private string _ageDetail = "";
    private string _neLe = "";

    // champs de la page détail
    private string _totalJours = "";
    private string _jourNaissance = "";
    private string _prochainAnniv = "";

    // Proprietes publiques : ce que le XAML voit.
    public string Nom
    {
        get => _nom;
        set
        {
            if (SetField(ref _nom, value))
                CalculerCommand.Rafraichir();
        }
    }

    public DateTime DateNaissance
    {
        get => _dateNaissance;
        set => SetField(ref _dateNaissance, value);
    }

    public string Resultat
    {
        get => _resultat;
        set => SetField(ref _resultat, value);
    }

    public bool ResultatVisible
    {
        get => _resultatVisible;
        set
        {
            // le bouton Voir le détail dépend de ce champ
            if (SetField(ref _resultatVisible, value))
                VoirResultatCommand.Rafraichir();
        }
    }

    public string Message
    {
        get => _message;
        set => SetField(ref _message, value);
    }

    public string JoursRestants
    {
        get => _joursRestants;
        set => SetField(ref _joursRestants, value);
    }

    public string AgeAnnees
    {
        get => _ageAnnees;
        set => SetField(ref _ageAnnees, value);
    }

    public string AgeDetail
    {
        get => _ageDetail;
        set => SetField(ref _ageDetail, value);
    }

    public string NeLe
    {
        get => _neLe;
        set => SetField(ref _neLe, value);
    }

    public string TotalJours
    {
        get => _totalJours;
        set => SetField(ref _totalJours, value);
    }

    public string JourNaissance
    {
        get => _jourNaissance;
        set => SetField(ref _jourNaissance, value);
    }

    public string ProchainAnniv
    {
        get => _prochainAnniv;
        set => SetField(ref _prochainAnniv, value);
    }

    // liste des calculs deja faits
    public ObservableCollection<string> Historique { get; } = new();

    // Liee a Button.Command dans le XAML.
    public RelayCommand CalculerCommand { get; }
    public RelayCommand EffacerCommand { get; }
    public RelayCommand VoirResultatCommand { get; }

    public CalculateurViewModel()
    {
        CalculerCommand = new RelayCommand(
            Calculer,
            () => !string.IsNullOrWhiteSpace(Nom));

        EffacerCommand = new RelayCommand(Effacer);

        // actif seulement quand un résultat est affiché
        VoirResultatCommand = new RelayCommand(
            VoirResultat,
            () => ResultatVisible);
    }

    // La logique metier : aucun controle d interface ici.
    private void Calculer()
    {
        // date dans le futur : on refuse
        if (DateNaissance.Date > DateTime.Today)
        {
            Resultat = "Date de naissance invalide";
            Message = "";
            JoursRestants = "";
            AgeAnnees = "—";
            AgeDetail = "Date de naissance invalide";
            NeLe = "";
            TotalJours = "";
            JourNaissance = "";
            ProchainAnniv = "";
            ResultatVisible = true;
            return;
        }

        int age = DateTime.Today.Year
                  - DateNaissance.Year;
        if (DateNaissance.Date >
            DateTime.Today.AddYears(-age)) age--;

        Resultat = $"{Nom}, vous avez {age} ans";

        // majeur ou mineur
        Message = age >= 18 ? "Majeur" : "Mineur";
        ResultatVisible = true;

        // prochain anniversaire
        DateTime prochain = DateNaissance.Date.AddYears(age + 1);
        int jours = (prochain - DateTime.Today).Days;
        JoursRestants = $"Prochain anniversaire dans {jours} jours";

        // mois et jours depuis le dernier anniversaire
        DateTime dernier = DateNaissance.Date.AddYears(age);
        int moisEcoules = 0;
        while (dernier.AddMonths(moisEcoules + 1) <= DateTime.Today)
            moisEcoules++;
        int joursEcoules = (DateTime.Today - dernier.AddMonths(moisEcoules)).Days;

        AgeAnnees = age > 1 ? $"{age} ans" : $"{age} an";
        AgeDetail = $"{moisEcoules} mois et {joursEcoules} jours";
        NeLe = "Né(e) le " + DateNaissance.ToString("d MMMM yyyy", fr);

        // jours vécus et jour de naissance
        int totalJours = (DateTime.Today - DateNaissance.Date).Days;
        TotalJours = totalJours.ToString("N0", fr) + " jours";
        JourNaissance = fr.TextInfo.ToTitleCase(DateNaissance.ToString("dddd", fr));

        // date du prochain anniversaire, avec majuscule
        string texteAnniv = prochain.ToString("dddd d MMMM yyyy", fr);
        ProchainAnniv = char.ToUpper(texteAnniv[0]) + texteAnniv.Substring(1);

        // on ajoute en haut de l'historique
        Historique.Insert(0, Resultat);
    }

    // Remet tous les champs a zero.
    private void Effacer()
    {
        Nom = "";
        DateNaissance = DateTime.Today.AddYears(-20);
        Resultat = "";
        Message = "";
        JoursRestants = "";
        AgeAnnees = "";
        AgeDetail = "";
        NeLe = "";
        TotalJours = "";
        JourNaissance = "";
        ProchainAnniv = "";
        ResultatVisible = false;
    }

    // Ouvre ResultatPage et lui donne CE ViewModel (this).
    private async void VoirResultat()
    {
        await Shell.Current.GoToAsync(
            nameof(CalculateurAge.Views.ResultatPage),
            new Dictionary<string, object> { ["donnees"] = this });
    }
}