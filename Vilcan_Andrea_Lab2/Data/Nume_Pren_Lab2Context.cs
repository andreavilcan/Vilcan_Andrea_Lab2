using Microsoft.EntityFrameworkCore;
using Vilcan_Andrea_Lab2.Models;

namespace Vilcan_Andrea_Lab2.Data
{
    public class Nume_Pren_Lab2Context : DbContext
    {
        public Nume_Pren_Lab2Context(DbContextOptions<Nume_Pren_Lab2Context> options) : base(options) {}

        public DbSet<Book> Book { get; set; } = default!;
        public DbSet<Publisher> Publisher { get; set; } = default!;
    }
}