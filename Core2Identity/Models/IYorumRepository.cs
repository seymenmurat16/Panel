using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core2Identity.Models
{
    public interface IYorumRepository
    {
        IQueryable<Yorum> Yorum { get; }
        Yorum GetYorumId(int yorumid);
        void CreateYorum(Yorum yorum);
        void DeleteYorum(int yorumid);
        IQueryable<Yorum> DuyuruYorumlar(int duyuruid);
    }
}
