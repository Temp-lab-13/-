using JWTAppTaskOne.Context.Dto;
using Microsoft.EntityFrameworkCore;

namespace JWTAppTaskOne.Context
{
    public partial class AppDbContext : DbContext
    {
        private readonly string connectionString;

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) // Здесь же, достаточно базового определения опции контеста записаной в сервесе билдера в Program.cs
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(x => x.Id).HasName("Users_Id");
                entity.HasIndex(x => x.Login).IsUnique();

                entity.ToTable("users");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Login).HasColumnName("login");
                entity.Property(e => e.Password).HasColumnName("password");
                entity.Property(e => e.Salt).HasColumnName("salt");
                entity.Property(e => e.RoleId).HasConversion<int>();
            });

            modelBuilder.Entity<Role>().Property(e => e.RoleId).HasConversion<int>();
            modelBuilder.Entity<Role>().HasData(Enum.GetValues(typeof(RoleId)).Cast<RoleId>().Select(e => new Role() { RoleId = e, Login = e.ToString()}));

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
