using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Panel.Models
{
    public class KisiReprository : IKisiReprository
    {
        private ApplicationDbContext _context;

        public KisiReprository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Kisi> Kisi => _context.Kisiler;


        public void NewUser(Kisi kisi)
        {
            _context.Kisiler.Add(kisi);
            _context.SaveChanges();
        }

        public bool SearchUser(Kisi kisi)
        {
            return _context.Kisiler.Any(i => i.Email == kisi.Email && i.Password == kisi.Password);
        }
    }
}
