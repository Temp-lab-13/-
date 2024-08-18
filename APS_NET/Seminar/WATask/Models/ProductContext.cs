using Microsoft.EntityFrameworkCore;

namespace WATask.Models
{
    public class ProductContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; } 
        public DbSet<Storage> Storages { get; set; }

        public ProductContext() { }

        public ProductContext(DbContextOptions<ProductContext> pc) : base(pc) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies().UseNpgsql("Host=localhost;Username=postgres;Password=lotta;Database=AppStoreg");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.HasKey(x => x.Id).HasName("ProductID");
                entity.HasIndex(x => x.Name).IsUnique();

                entity.Property(e => e.Name)
                .HasColumnName("ProductName")
                .HasMaxLength(255)
                .IsRequired();

                entity.Property(e => e.Descript)
                .HasColumnName("Descript")
                .HasMaxLength(255)
                .IsRequired();

                entity.Property(e => e.Price)
                .HasColumnName("Price")
                .IsRequired();

                entity.HasOne(x => x.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(x => x.CategoriId)
                .HasConstraintName("CategoryProduct"); // Связь
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("CategoryProduct");

                entity.HasKey(x => x.Id).HasName("CategoryID");
                entity.HasIndex(x => x.Name).IsUnique();

                entity.Property(e => e.Name)
                .HasColumnName("ProductName")
                .HasMaxLength(255)
                .IsRequired();
            });

            modelBuilder.Entity<Storage>(entity =>
            {
                entity.ToTable("Storage");

                entity.HasKey(x => x.Id).HasName("StorageID");
                entity.HasIndex(x => x.Name).IsUnique();

                entity.Property(e => e.Name)
                .HasColumnName("StorageName")
                .HasMaxLength(255)
                .IsRequired();

                entity.Property(e => e.Count)
                .HasColumnName("ProductCount")
                .HasMaxLength(255)
                .IsRequired();

                entity.HasMany(x => x.Products)
                .WithMany(m => m.Stores)
                .UsingEntity(j => j.ToTable("StorageToProduct"));
            });
        }
    }
}
