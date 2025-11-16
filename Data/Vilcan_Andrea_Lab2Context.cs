using Microsoft.EntityFrameworkCore;
using Vilcan_Andrea_Lab2.Models;

namespace Vilcan_Andrea_Lab2.Data
{
    public class Vilcan_Andrea_Lab2Context : DbContext
    {
        public Vilcan_Andrea_Lab2Context(DbContextOptions<Vilcan_Andrea_Lab2Context> options)
            : base(options)
        {
        }

        public DbSet<Book> Book { get; set; }
        public DbSet<Publisher> Publisher { get; set; }
        public DbSet<Author> Author { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<BookCategory> BookCategory { get; set; }
        public DbSet<Member> Member { get; set; } = default!;
        public DbSet<Borrowing> Borrowing { get; set; } = default!;
    }
}