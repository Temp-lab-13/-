using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace TestBD3.Model

// Сработало... миграция прошла.
// Консольная команда для миграции: dotnet ef migrations add InitialCreate --context TestContext
// !!!TestContext - название класса с контекстом (структорой таблиц в коде), по образу которого идёт миграция.
// !!!InitialCreate - название миграции. Оно у каждой миграции должно быть уникально. 
// Обновление базы данных прошло. Появилась нова БД TestNew с заданной в коде структурой таблиц. Алилуя.
// Консольная команда для обновления БД: dotnet ef database update –-c TestContext
// Важно!!! Команда сработала только без последней части команды: –-c TestContext; По какой-то причине ef не воспринимает --с.
{
    public partial class TestContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseLazyLoadingProxies().UseNpgsql("Host=localhost;Username=postgres;Password=lotta;Database=TestNew");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("messages_pk");

                entity.ToTable("messegers");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.MessageContent).HasColumnName("message");
                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User).WithMany(d => d.Messages)
                    .HasForeignKey(e => e.UserId)
                    .HasConstraintName("messages_user_id_pk");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("user_pk");

                entity.ToTable("users");

                entity.Property(e => e.Id).HasColumnName("Id");
                entity.Property(e => e.Name).HasMaxLength(255).HasColumnName("name");
                entity.Property(e => e.GenderId).HasConversion<int>();
            });

            modelBuilder.Entity<Gender>().Property(e => e.genderId).HasConversion<int>();
            modelBuilder.Entity<Gender>().HasData(Enum.GetValues(typeof(GenderId)).Cast<GenderId>().Select(e => new Gender()
            {
                genderId = e,
                name = e.ToString()
            }));

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
