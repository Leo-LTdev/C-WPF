using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Linq;
using System.Windows;
using WpfApp1.Models;
using WpfApp1.Services;

namespace WpfApp1.ViewModels
{
    public partial class Login : ObservableObject
    {
        private readonly AppDbContext _context;
        private readonly AuthService _authService;

        [ObservableProperty]
        private string _username;

        [ObservableProperty]
        private string _password;

        public Login()
        {
            _context = new AppDbContext();
            _authService = new AuthService();
        }
        [RelayCommand]
        private void SubmitLogin()
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                MessageBox.Show("Remplissez les champs", "Avertissement");
                return;
            }
            string hashedInput = _authService.HashEnBase(Password);
            var user = _context.Logins.FirstOrDefault(u => u.Username == Username && u.PasswordHash == hashedInput);
            if (user != null)
            {
                MessageBox.Show("Connexion réussie", "Succès");
            }
            else
            {
                MessageBox.Show("Identifiants ou mot de passe incorrect", "Erreur");
            }
        }
    }
}