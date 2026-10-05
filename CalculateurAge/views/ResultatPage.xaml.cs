namespace CalculateurAge.Views;

// Relie les paramètres "nom" et "age" de l'URL aux propriétés.
[QueryProperty(nameof(Nom), "nom")]
[QueryProperty(nameof(Age), "age")]
public partial class ResultatPage : ContentPage
{
    // Remplies par la navigation, APRÈS le constructeur.
    public string Nom { get; set; } = "";
    public string Age { get; set; } = "";

    public ResultatPage()
    {
        InitializeComponent();
    }

    // Appelée à CHAQUE affichage de la page.
    protected override void OnAppearing()
    {
        base.OnAppearing();
        lblMessage.Text = $"{Nom}, vous avez {Age} ans";
    }

    // ".." = revenir à la page précédente.
    private async void OnRetourClicked(object sender, EventArgs e)
        => await Shell.Current.GoToAsync("..");
}