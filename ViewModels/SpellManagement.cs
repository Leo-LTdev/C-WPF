using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WpfApp1.Models;

namespace WpfApp1.ViewModels
{
    public partial class SpellManagement : ObservableObject
    {
        private readonly AppDbContext _context;
        [ObservableProperty]
        private ObservableCollection<Spell>? _filteredSpells;
        [ObservableProperty]
        private ObservableCollection<Hero>? _heroes;
        [ObservableProperty]
        private Hero? _selectedHeroFilter;
        public SpellManagement()
        {
            _context = new AppDbContext();
            LoadData();
        }
        private void LoadData()
        {
            var allHeroes = _context.Heroes.Include(h => h.Spells).ToList();
            Heroes = new ObservableCollection<Hero>(allHeroes);
            FilteredSpells = new ObservableCollection<Spell>(_context.Spells.ToList());
        }
        partial void OnSelectedHeroFilterChanged(Hero? value)
        {
            if (value == null)
            {
                FilteredSpells = new ObservableCollection<Spell>(_context.Spells.ToList());
            }
            else
            {
                FilteredSpells = new ObservableCollection<Spell>(value.Spells);
            }
        }
    }
}