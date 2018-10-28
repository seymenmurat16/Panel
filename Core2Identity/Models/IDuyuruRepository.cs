using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core2Identity.Models
{
    public interface IDuyuruRepository
    {
        IQueryable<Duyuru> Duyuru { get; }
        Duyuru GetDuyuruId(int duyuruid);
        void UpdateDuyuru(Duyuru duyuru);
        void CreateDuyuru(Duyuru duyuru);
        void DeleteDuyuru(int duyuruid);
    }
}
