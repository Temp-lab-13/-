using Microsoft.EntityFrameworkCore;

namespace WATaskStoreg.Models.Context
{
    public class StoregeContext : DbContext
    {
        public DbSet<Storage> Storages { get; set; }
        private string connectionString;

        public StoregeContext() { }

        public StoregeContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies().UseNpgsql(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) // В идеале, у нас здесь должна быть максимальная полнота информации о товаре.
                                                                           // Но сейчас мы, по сути, просто тестируем работоспособность отдельного микросервеса.
        {
            
            modelBuilder.Entity<Storage>(entity =>
            {
                entity.ToTable("Storage");

                entity.HasKey(x => x.Id).HasName("PositionID");
                entity.HasIndex(x => x.productId).IsUnique();
                entity.HasIndex(x => x.Name).IsUnique();

                entity.Property(e => e.productId)
               .HasColumnName("ProductID")
               .HasMaxLength(255)
               .IsRequired();

                entity.Property(e => e.Name)
                .HasColumnName("PositionName")
                .HasMaxLength(255)
                .IsRequired();

                entity.Property(e => e.Descript)
                .HasColumnName("Descript")
                .HasMaxLength(255)
                .IsRequired();

                entity.Property(e => e.Count)
                .HasColumnName("ProductCount")
                .HasMaxLength(255)
                .IsRequired();
            });
        }
    }
}
