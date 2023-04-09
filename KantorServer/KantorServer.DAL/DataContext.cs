using KantorServer.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace KantorServer.DAL
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        { }

        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Kantor> Kantors { get; set; }
        public DbSet<Rate> Rates { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserSession> UserSessions { get; set; }
 
        public override int SaveChanges()
        {
            var entries = ChangeTracker.Entries().Where(x => x.Entity is BaseModel && (x.State == EntityState.Added ||  x.State == EntityState.Modified));
            foreach (var entry in entries) { ((BaseModel)entry.Entity).LastUpdate = DateTime.Now; }
            return base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
