using System.Collections.ObjectModel;

namespace CalculateurAge.ViewModels;

public class CalculateurViewModel : BaseViewModel
{
    private string _nom = "";
    private DateTime _dateNaissance = DateTime.Today.AddYears(-20);
    private string _resultat = "";
    private string _message = "";
    private string _joursRestants = "";
    private bool _resultatVisible;

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
        set
        {
            if (SetField(ref _dateNaissance, value))
                CalculerCommand.Rafraichir();
        }
    }

    public string Resultat
    {
        get => _resultat;
        set => SetField(ref _resultat, value);
    }

    // Fonctionnalité 1 : "Majeur" ou "Mineur".
    public string Message
    {
        get => _message;
        set => SetField(ref _message, value);
    }

    // Fonctionnalité 2 : jours avant le prochain anniversaire.
    public string JoursRestants
    {
        get => _joursRestants;
        set => SetField(ref _joursRestants, value);
    }

    public bool ResultatVisible
    {
        get => _resultatVisible;
        set => SetField(ref _resultatVisible, value);
    }

    // Fonctionnalité 4 : historique des calculs.
    public ObservableCollection<string> Historique { get; } = new();

    public RelayCommand CalculerCommand { get; }
    public RelayCommand EffacerCommand { get; }

    public CalculateurViewModel()
    {
        // Calculer impossible si le nom est vide
        // ou si la date est dans le futur.
        CalculerCommand = new RelayCommand(
            Calculer,
            () => !string.IsNullOrWhiteSpace(Nom)
                  && DateNaissance.Date <= DateTime.Today);

        // Fonctionnalité 3 : Effacer.
        EffacerCommand = new RelayCommand(Effacer);
    }

    private void Calculer()
    {
        DateTime aujourdhui = DateTime.Today;

        int age = aujourdhui.Year - DateNaissance.Year;
        if (DateNaissance.Date > aujourdhui.AddYears(-age)) age--;

        Resultat = $"{Nom}, vous avez {age} ans";
        Message = age >= 18 ? "Majeur" : "Mineur";

        // Prochain anniversaire (AddYears gère le 29 février).
        if (DateNaissance.AddYears(age).Date == aujourdhui)
        {
            JoursRestants = "Joyeux anniversaire !";
        }
        else
        {
            DateTime prochain = DateNaissance.AddYears(age + 1).Date;
            int jours = (prochain - aujourdhui).Days;
            JoursRestants = $"Prochain anniversaire dans {jours} jour(s)";
        }

        ResultatVisible = true;

        // Le plus récent en haut de la liste.
        Historique.Insert(0, $"{Nom} : {age} ans ({Message})");
    }

    private void Effacer()
    {
        Nom = "";
        DateNaissance = DateTime.Today.AddYears(-20);
        Resultat = "";
        Message = "";
        JoursRestants = "";
        ResultatVisible = false;
    }
}