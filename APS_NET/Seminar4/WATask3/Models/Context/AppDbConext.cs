using Microsoft.EntityFrameworkCore;
using WATask3.Models.Model;
using WATask3.Models.Roles;

namespace WATask3.Models.Context
{
    public class AppDbConext : DbContext
    {
        private readonly string _connectionString;

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public AppDbConext() { }

        public AppDbConext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString).UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasIndex(x => x.Name).IsUnique();

                entity.Property(e => e.Password).IsRequired();

                entity.HasOne(x => x.Role).WithMany(x => x.Users);

            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(x => x.RoleId);
                entity.HasIndex(x => x.Name).IsUnique();
            });
        }
    }
}
