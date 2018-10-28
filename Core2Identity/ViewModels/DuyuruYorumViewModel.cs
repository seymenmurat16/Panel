using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core2Identity.Models;

namespace Core2Identity.ViewModels
{
    public class DuyuruYorumViewModel
    {
        public Duyuru Duyuru { get; set; }
        public IQueryable<Yorum> Yorumlar { get; set; }
        public Yorum Yorum { get; set; }
    }
}
