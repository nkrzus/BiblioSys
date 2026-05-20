using Microsoft.EntityFrameworkCore;

namespace Bibliosys
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }
        public DbSet<Model.Author> Authors { get; set; }

        public DbSet<Model.Book> Books { get; set; }

        public DbSet<Model.User> Users { get; set; }

        public DbSet<Model.Reservation> Reservations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=BiblioSysDatabase;Trusted_Connection=True;MultipleActiveResultSets=true;");
        }
    }
}
