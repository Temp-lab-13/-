using Microsoft.EntityFrameworkCore;

namespace WATask2Storeg.Models
{
    public class StoregContext : DbContext
    {
        private readonly string _connectionString;
        public DbSet<Storage> Storages { get; set; }

        public StoregContext() { }

        public StoregContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString).UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Storage>(entity =>
            {
                entity.ToTable("Storage");

                entity.HasKey(x => x.Id).HasName("PositionID");
                entity.HasIndex(x => x.Name).IsUnique();

                entity.Property(e => e.productId).HasColumnName("ProductId").HasMaxLength(255).IsRequired();
                entity.Property(e => e.Name).HasColumnName("Product").HasMaxLength(255).IsRequired();
                entity.Property(e => e.Count).HasColumnName("ProductCount").HasMaxLength(255).IsRequired();

            });
        }



    }
}
