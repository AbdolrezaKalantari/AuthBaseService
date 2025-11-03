using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthBaseService.Data.ApplicationContext
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        AppDbContext IDesignTimeDbContextFactory<AppDbContext>.CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();


            optionsBuilder.UseSqlServer("Server=.;Database=AuthDb;Trusted_Connection=True;Encrypt=False;");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
