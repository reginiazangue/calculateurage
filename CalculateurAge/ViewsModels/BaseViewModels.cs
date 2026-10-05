using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CalculateurAge.ViewModels;

// Classe mère de tous les ViewModels.
public class BaseViewModel : INotifyPropertyChanged
{
    // L'ÉVÉNEMENT : le moteur de binding s'y abonne.
    public event PropertyChangedEventHandler? PropertyChanged;

    // Prévient la vue que cette propriété a changé.
    protected void OnPropertyChanged([CallerMemberName] string? nom = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nom));

    // Affecte une valeur ET notifie. Renvoie true si la valeur a changé.
    protected bool SetField<T>(ref T champ, T valeur,
        [CallerMemberName] string? nom = null)
    {
        if (EqualityComparer<T>.Default.Equals(champ, valeur))
            return false;
        champ = valeur;
        OnPropertyChanged(nom);
        return true;
    }
}