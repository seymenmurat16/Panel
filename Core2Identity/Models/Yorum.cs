using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core2Identity.Models
{
    public class Yorum
    {
        public int YorumId { get; set; }
        public int DuyuruId { get; set; }
        public string UserId  { get; set; }
        public string YorumDescription { get; set; }
    }
}
