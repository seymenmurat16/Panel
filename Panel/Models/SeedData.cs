using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Panel.Models
{
    public class SeedData
    {
        public static void Seed(IApplicationBuilder app)
        {
            ApplicationDbContext context = app.ApplicationServices.GetRequiredService<ApplicationDbContext>();

            context.Database.Migrate();

            /*
             * add-migration testMig
             * update-database
            if (!context.Kisiler.Any())
            {
                context.Kisiler.AddRange(
                    new Kisi() { Email = "admin", Password = "admin", Email_X = "admin", Password_X = "admin" },
                    new Kisi() { Email = "admin2", Password = "admin2", Email_X = "admin2", Password_X = "admin2" }
                    );
                context.SaveChanges();
            }*/

        }
    }
}
