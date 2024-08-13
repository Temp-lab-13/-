using Microsoft.EntityFrameworkCore;

namespace WATask1.DadaBase
{
    public partial class AppDbContext : DbContext
    {
        
        public DbSet<User> Users { get; set; }
        private string _connectionString;
        public AppDbContext()
        {
        }

        public AppDbContext(string connectString)
        {
            _connectionString = connectString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseNpgsql(_connectionString);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(en =>
            {
                en.HasKey(e => e.Id).HasName("UserId");
                en.HasIndex(e => e.Email).IsUnique();

                en.ToTable(nameof(User));

                en.Property(e => e.Id).HasColumnName("Id");
                en.Property(e => e.Email).HasColumnName("Email");
                en.Property(e => e.Name).HasColumnName("Name");
                en.Property(e => e.Surname).HasColumnName("Surname");
                en.Property(e => e.Registration).HasColumnName("Registration");
                en.Property(e => e.Active).HasColumnName("Ative");
                en.Property(e => e.Password).HasColumnName("Password");
            });


            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder); // particion надо объявлять и в названии скласса. в верху
    }
}
