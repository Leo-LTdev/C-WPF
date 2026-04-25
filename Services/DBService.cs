using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WpfApp1.Models;

namespace WpfApp1.Services
{
    public class DBService
    {
        private readonly AppDbContext _context;
        private readonly AuthService _authService;
        public DBService()
        {
            _context = new AppDbContext();
            _authService = new AuthService();
        }
        public void InitData()
        {
            if (_context.Logins.Any())
            {
                _context.Database.ExecuteSqlRaw("DELETE FROM HeroSpell; DELETE FROM PlayerHero; DELETE FROM Login; DELETE FROM Player; DELETE FROM Hero; DELETE FROM Spell;");
            }
            var admin = new Login { Username = "admin", PasswordHash = _authService.HashEnBase("admin123") };
            _context.Logins.Add(admin);
            var s1 = new Spell { Name = "Coup d'épée", Damage = 20, Description = "Attaque de base rapide." };
            var s2 = new Spell { Name = "Fracas", Damage = 35, Description = "Lourde frappe étourdissante." };
            var s3 = new Spell { Name = "Cri de guerre", Damage = 10, Description = "Effraie l'ennemi." };
            var s4 = new Spell { Name = "Exécution", Damage = 50, Description = "Coup final dévastateur." };
            var s5 = new Spell { Name = "Boule de feu", Damage = 40, Description = "Puissante magie de feu." };
            var s6 = new Spell { Name = "Éclair de glace", Damage = 25, Description = "Ralentit l'adversaire." };
            var s7 = new Spell { Name = "Soin light", Damage = -20, Description = "Rend un peu de vie." };
            var s8 = new Spell { Name = "Explosion arcane", Damage = 45, Description = "Énergie pure." };
            _context.Spells.AddRange(s1, s2, s3, s4, s5, s6, s7, s8);
            var warrior = new Hero { Name = "Guerrier", Health = 150, Spells = new List<Spell> { s1, s2, s3, s4 } };
            var mage = new Hero { Name = "Mage", Health = 90, Spells = new List<Spell> { s5, s6, s7, s8 } };
            _context.Heroes.AddRange(warrior, mage);
            _context.SaveChanges();
        }
    }
}