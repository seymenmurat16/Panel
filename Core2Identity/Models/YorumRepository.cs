using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core2Identity.Models
{
    public class YorumRepository:IYorumRepository
    {
        private ApplicationIdentityDbContext _context;

        public YorumRepository(ApplicationIdentityDbContext context)
        {
            _context = context;
        }

        public IQueryable<Yorum> Yorum => _context.Yorumlar;

        public void CreateYorum(Yorum yorum)
        {
            _context.Yorumlar.Add(yorum);
            _context.SaveChanges();
        }

        public void DeleteYorum(int yorumid)
        {
            var yorum = GetYorumId(yorumid);
            if (yorum != null)
            {
                _context.Yorumlar.Remove(yorum);
                _context.SaveChanges();
            }
        }

        public IQueryable<Yorum> DuyuruYorumlar(int duyuruid)
        {
            return _context.Yorumlar.Where(i => i.DuyuruId == duyuruid);
        }

        public Yorum GetYorumId(int yorumid)
        {
            return _context.Yorumlar.FirstOrDefault(i => i.YorumId == yorumid);
        }

    }
}
