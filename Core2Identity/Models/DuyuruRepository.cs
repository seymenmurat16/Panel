using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core2Identity.Models
{
    public class DuyuruRepository : IDuyuruRepository
    {

        private ApplicationIdentityDbContext _context;

        public DuyuruRepository(ApplicationIdentityDbContext context)
        {
            _context = context;
        }

        public IQueryable<Duyuru> Duyuru => _context.Duyurular;

        public void CreateDuyuru(Duyuru duyuru)
        {
            _context.Duyurular.Add(duyuru);
            _context.SaveChanges();
        }

        public void DeleteDuyuru(int duyuruid)
        {
            var duyuru = GetDuyuruId(duyuruid);
            if (duyuru != null)
            {
                _context.Duyurular.Remove(duyuru);
                _context.SaveChanges();
            }
        }

        public Duyuru GetDuyuruId(int duyuruid)
        {
            return _context.Duyurular.FirstOrDefault(i => i.DuyuruId == duyuruid);
        }

        public void UpdateDuyuru(Duyuru duyuru)
        {
            _context.Duyurular.Update(duyuru);
            _context.SaveChanges();
        }
    }
}
