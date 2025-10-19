using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Vilcan_Andrea_Lab2.Data
{
    public class Nume_Pren_Lab2ContextFactory : IDesignTimeDbContextFactory<Nume_Pren_Lab2Context>
    {
        public Nume_Pren_Lab2Context CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder<Nume_Pren_Lab2Context>()
                .UseNpgsql("Host=localhost;Port=5432;Database=vilcan_andrea_lab2;Username=postgres;Password=PAROLA_TA")
                .Options;

            return new Nume_Pren_Lab2Context(options);
        }
    }
}