using Microsoft.EntityFrameworkCore;
using PochtaServers.Models.EssenceModel;

namespace PochtaServers.Models.Context
{
    public partial class AppDbContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Message> Messages { get; set; }

        private readonly string connect;

        public AppDbContext(string connectStringn)
        {
            this.connect = connectStringn;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
          => optionsBuilder.UseLazyLoadingProxies().UseNpgsql(connect);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("client_pk");
                entity.HasIndex(e => e.Email).IsUnique();

                entity.ToTable("clients");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Email)
                .HasColumnName("email")
                .HasMaxLength(255);
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("message_pk");

                entity.ToTable("messages");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Topic)
                .HasColumnName("topic")
                .HasMaxLength(255);
                entity.Property(e => e.Text)
                .HasColumnName("text");

                entity.HasOne(e => e.Client).WithMany(p => p.Messages);
               

            });

            
            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
