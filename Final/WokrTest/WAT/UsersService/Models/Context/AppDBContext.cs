using Microsoft.EntityFrameworkCore;
using UsersService.Models.EssenceModel;
using UsersService.Models.RolesModel;

namespace UsersService.Models.Context
{
    public partial class AppDBContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Message> Messages { get; set; }

        private readonly string _connectString;

        public AppDBContext(string connectString) 
        {
            _connectString = connectString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
           => optionsBuilder.UseLazyLoadingProxies().UseNpgsql(_connectString);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("user_pk");
                entity.HasIndex(e => e.Email).IsUnique();

                entity.ToTable("users");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Email)
                .HasColumnName("email")
                .HasMaxLength(255);

                entity.Property(e => e.Password).HasColumnName("password");
                entity.Property(e => e.Salt).HasColumnName("salt");

                entity.Property(e => e.RoleId).HasConversion<int>();
            });

            modelBuilder.Entity<Role>()
                .Property(e => e.RoleId)
                .HasConversion<int>();

            modelBuilder.Entity<Role>()
                .HasData(Enum.GetValues(typeof(RoleId))
                .Cast<RoleId>()
                .Select(s => new Role()
                {
                    RoleId = s,
                    Email = s.ToString()
                }));

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

                entity.HasOne(e => e.User).WithMany(p => p.Messages);


            });

            OnModelCreatingPartian(modelBuilder);
        }

        partial void OnModelCreatingPartian(ModelBuilder modelBuilder);
    }
}
