using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Panel.Models
{
    public interface IKisiReprository
    {
        IQueryable<Kisi> Kisi { get; }
        void NewUser(Kisi kisi);
        bool SearchUser(Kisi kisi);
    }
}
