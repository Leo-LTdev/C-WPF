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
        private int _playerHealth;
        [ObservableProperty]
        private int _enemyHealth;
        [ObservableProperty]
        private string _combatLog;
        public Combat(Hero selectedHero)
        {
            Player = selectedHero;
            PlayerHealth = selectedHero.Health;
            Enemy = new Hero { Name = "Orc Sombre", Health = (int)(selectedHero.Health * 1.10) };
            EnemyHealth = Enemy.Health;
            CombatLog = $"Un {Enemy.Name} sauvage apparaît avec {EnemyHealth} HP !\n";
        }
        [RelayCommand]
        private void CastSpell(Spell spell)
        {
            if (PlayerHealth <= 0 || EnemyHealth <= 0 || spell == null) return;
            if (spell.Damage < 0)
            {
                PlayerHealth -= spell.Damage;
                if (PlayerHealth > Player.Health) PlayerHealth = Player.Health;
                CombatLog = $"Vous lancez {spell.Name} et récupérez {-spell.Damage} HP.\n" + CombatLog;
            }
            else
            {
                EnemyHealth -= spell.Damage;
                CombatLog = $"Vous lancez {spell.Name} ! L'ennemi perd {spell.Damage} HP.\n" + CombatLog;
            }
            if (EnemyHealth <= 0)
            {
                EnemyHealth = 0;
                CombatLog = "VICTOIRE ! L'ennemi est vaincu.\n" + CombatLog;
                MessageBox.Show("Vous avez gagné le combat !", "Victoire");
                return;
            }
            int enemyDmg = new Random().Next(10, 25);
            PlayerHealth -= enemyDmg;
            CombatLog = $"L'Orc Sombre riposte et vous inflige {enemyDmg} dégâts.\n" + CombatLog;
            if (PlayerHealth <= 0)
            {
                PlayerHealth = 0;
                CombatLog = "DÉFAITE... Vous avez succombé.\n" + CombatLog;
                MessageBox.Show("Vous êtes mort...", "Game Over");
            }
        }
    }
}