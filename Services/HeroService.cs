using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WpfApp1.Models;

namespace WpfApp1.Services
{
    public class HeroService
    {
        private readonly AppDbContext _context;
        public HeroService()
        {
            _context = new AppDbContext();
        }
        public List<Hero> GetAllHeroes()
        {
            return _context.Heroes.Include(h => h.Spells).ToList();
        }
    }
}