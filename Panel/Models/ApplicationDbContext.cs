using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Panel.Models
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            :base(options){ }

        public DbSet<Kisi> Kisiler { get; set; }
        public DbSet<Duyuru> Duyurular { get; set; }
    }
}
