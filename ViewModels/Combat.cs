using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Windows;
using WpfApp1.Models;

namespace WpfApp1.ViewModels
{
    public partial class Combat : ObservableObject
    {
        [ObservableProperty]
        private Hero _player;
        [ObservableProperty]
        private Hero _enemy;
        [ObservableProperty]
        private string _combatLog;
        public Combat(Hero selectedHero)
        {
            Player = selectedHero;
            Enemy = new Hero
            {
                Name = "Orc Sombre",
                Health = (int)(selectedHero.Health * 1.10)
            };
            CombatLog = $"Un {Enemy.Name} sauvage apparaît avec {Enemy.Health} HP !\n";
        }
        [RelayCommand]
        private void CastSpell(Spell spell)
        {
            if (Player.Health <= 0 || Enemy.Health <= 0 || spell == null) return;
            Enemy.Health -= spell.Damage;
            CombatLog = $"Vous lancez {spell.Name} ! L'ennemi perd {spell.Damage} HP.\n" + CombatLog;
            if (Enemy.Health <= 0)
            {
                Enemy.Health = 0;
                CombatLog = "VICTOIRE ! L'ennemi est vaincu.\n" + CombatLog;
                MessageBox.Show("Vous avez gagné le combat", "Victoire");
                return;
            }
            int enemyDmg = new Random().Next(10, 25);
            Player.Health -= enemyDmg;
            CombatLog = $"L'Orc Sombre riposte et vous inflige {enemyDmg} dégâts.\n" + CombatLog;
            if (Player.Health <= 0)
            {
                Player.Health = 0;
                CombatLog = "DÉFAITE... Vous avez succombé.\n" + CombatLog;
                MessageBox.Show("Vous êtes mort", "Game Over");
            }
        }
    }
}