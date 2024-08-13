using Microsoft.EntityFrameworkCore;

namespace WATask2.Models
{
    public class ProductContext : DbContext
    {
        private readonly string _connectionString;
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; } 
        public DbSet<Storage> Storages { get; set; }

        public ProductContext() { }

        public ProductContext(string connectionString) 
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString).UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.HasKey(x => x.Id).HasName("ProductID");
                entity.HasIndex(x => x.Name).IsUnique();

                entity.Property(e => e.Name).HasColumnName("ProductName").HasMaxLength(255).IsRequired();

                entity.Property(e => e.Descript).HasColumnName("Descript").HasMaxLength(255).IsRequired();

                entity.Property(e => e.Price).HasColumnName("Price").IsRequired();

                entity.HasOne(x => x.Category).WithMany(c => c.Products).HasForeignKey(x => x.Id).HasConstraintName("CategoryToProduct"); // Связь
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("CategoryToProduct");

                entity.HasKey(x => x.Id).HasName("CategoryID");
                entity.HasIndex(x => x.Name).IsUnique();

                entity.Property(e => e.Name).HasColumnName("ProductName").HasMaxLength(255).IsRequired();
            });

            modelBuilder.Entity<Storage>(entity =>
            {
                entity.ToTable("Storage");

                entity.HasKey(x => x.Id).HasName("StorageID");
                entity.HasIndex(x => x.Name).IsUnique();

                entity.Property(e => e.Name).HasColumnName("StorageName").HasMaxLength(255).IsRequired();
                entity.Property(e => e.Count).HasColumnName("ProductCount").HasMaxLength(255).IsRequired();


                entity.HasMany(x => x.Products).WithMany(m => m.Stores).UsingEntity(j => j.ToTable("StorageProduct"));
            });

        }



    }
}
