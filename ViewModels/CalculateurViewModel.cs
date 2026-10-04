namespace CalculateurAge.ViewModels;

// Contient l ETAT de l ecran et les ACTIONS possibles.
public class CalculateurViewModel : BaseViewModel
{
    // Champs prives : la vraie donnee.
    private string _nom = "";
    private DateTime _dateNaissance = DateTime.Today.AddYears(-20);
    private string _resultat = "";
    private bool _resultatVisible;

    // Champ prive : la vraie donnee du message Majeur / Mineur.
    private string _message = "";

    // Champ prive : la vraie donnee des jours restants.
    private string _joursRestants = "";

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
        set => SetField(ref _resultatVisible, value);
    }

    // Propriete publique : ce que le XAML voit pour le message.
    public string Message
    {
        get => _message;
        set => SetField(ref _message, value);
    }

    // Propriete publique : ce que le XAML voit pour les jours restants.
    public string JoursRestants
    {
        get => _joursRestants;
        set => SetField(ref _joursRestants, value);
    }

    // Liee a Button.Command dans le XAML.
    public RelayCommand CalculerCommand { get; }

    // Liee au bouton Effacer dans le XAML.
    public RelayCommand EffacerCommand { get; }

    public CalculateurViewModel()
    {
        CalculerCommand = new RelayCommand(
            Calculer,
            () => !string.IsNullOrWhiteSpace(Nom));

        // Cree la commande Effacer : elle appelle la methode Effacer.
        EffacerCommand = new RelayCommand(Effacer);
    }

    // La logique metier : aucun controle d interface ici.
    private void Calculer()
    {
        // Refuse une date de naissance dans le futur.
        if (DateNaissance.Date > DateTime.Today)
        {
            // Affiche un message d'erreur a la place de l'age.
            Resultat = "Date de naissance invalide";
            Message = "";
            // Pas de calcul d'anniversaire si la date est invalide.
            JoursRestants = "";

            ResultatVisible = true;
            // On sort sans rien calculer.
            return;
        }

        int age = DateTime.Today.Year
                  - DateNaissance.Year;
        if (DateNaissance.Date >
            DateTime.Today.AddYears(-age)) age--;

        Resultat = $"{Nom}, vous avez {age} ans";

        // Si l'age est 18 ou plus : Majeur, sinon : Mineur.
        Message = age >= 18 ? "Majeur" : "Mineur";

        // Date du prochain anniversaire : date de naissance + (age + 1) ans.
        DateTime prochain = DateNaissance.Date.AddYears(age + 1);

        // Nombre de jours entre aujourd'hui et ce prochain anniversaire.
        int jours = (prochain - DateTime.Today).Days;

        // Texte affiche a l'ecran.
        JoursRestants = $"Prochain anniversaire dans {jours} jours";

        ResultatVisible = true;
    }

    // Remet tous les champs a zero : aucun controle d interface ici.
    private void Effacer()
    {
        Nom = "";
        DateNaissance = DateTime.Today.AddYears(-20);
        Resultat = "";
        Message = "";
        // Remet aussi les jours restants a zero.
        JoursRestants = "";
        ResultatVisible = false;
    }
}