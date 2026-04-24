using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using WpfApp1.Services;

namespace WpfApp1.ViewModels
{
    public partial class Settings : ObservableObject
    {
        private readonly DBService _dbService;
        [ObservableProperty]
        private string _connectionString;
        public Settings()
        {
            _dbService = new DBService();
            ConnectionString = @"Server=(localdb)\MSSQLLocalDB;Database=ExerciceHero;Trusted_Connection=True;TrustServerCertificate=True;";
        }
        [RelayCommand]
        private void InitDatabase()
        {
            try
            {
                _dbService.InitData();
                MessageBox.Show("Succès (Utilisateur: admin / Mdp: admin123)", "Succès");
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Erreur {ex.Message}", "Erreur");
            }
        }
    }
}