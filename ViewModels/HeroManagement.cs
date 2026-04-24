using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using WpfApp1.Models;
using WpfApp1.Services;

namespace WpfApp1.ViewModels
{
    public partial class HeroManagement : ObservableObject
    {
        private readonly HeroService _heroService;
        [ObservableProperty]
        private ObservableCollection<Hero> _heroes;
        [ObservableProperty]
        private Hero? _selectedHero;
        public HeroManagement()
        {
            _heroService = new HeroService();
            Heroes = new ObservableCollection<Hero>();
            LoadData();
        }
        [RelayCommand]
        private void LoadData()
        {
            var data = _heroService.GetAllHeroes();
            Heroes = new ObservableCollection<Hero>(data);
        }
    }
}