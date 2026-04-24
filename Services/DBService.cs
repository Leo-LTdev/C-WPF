using System.Linq;
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
            if (_context.Logins.Any()) return;
            var admin = new Login
            {
                Username = "admin",
                PasswordHash = _authService.HashEnBase("admin123")
            };
            _context.Logins.Add(admin);
            var slash = new Spell { Name = "Coup d'épée", Damage = 25, Description = "Attaque de base" };
            var fireball = new Spell { Name = "Boule de feu", Damage = 40, Description = "Attaque magique" };
            var heal = new Spell { Name = "Soin", Damage = -20, Description = "Rend des points de vie" };
            _context.Spells.AddRange(slash, fireball, heal);
            var warrior = new Hero
            {
                Name = "Guerrier",
                Health = 150,
                Spells = new System.Collections.Generic.List<Spell> { slash }
            };
            var mage = new Hero
            {
                Name = "Mage",
                Health = 90,
                Spells = new System.Collections.Generic.List<Spell> { fireball, heal }
            };
            _context.Heroes.AddRange(warrior, mage);
            _context.SaveChanges(); // save DB
        }
    }
}