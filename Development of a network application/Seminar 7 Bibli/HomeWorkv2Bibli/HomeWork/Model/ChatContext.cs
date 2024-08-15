using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork.Model
{
    // Метод шаблон для базы данных. 
    public class ChatContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }

        public ChatContext() { }

        public ChatContext(DbContextOptions<ChatContext> options) : base(options) { }

        // Делал черз постгресс. Миграция успешна, обновление базы данных успешно.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {        
            optionsBuilder.UseLazyLoadingProxies().UseNpgsql("Host=localhost;Username=postgres;Password=lotta;Database=TestAt2");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users"); // Название колонки

                entity.HasKey(x => x.Id).HasName("user_pkey"); // Авто инкремент
                entity.HasIndex(x => x.FullName).IsUnique(); // Уникальность поля(имени)

                entity.Property(es => es.FullName).HasColumnName("FullName").HasMaxLength(255).IsRequired(); // Выводим пользователей.
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("messages");

                entity.HasKey(f => f.MessageId).HasName("messagePK");

                entity.Property(e => e.Text).HasColumnName("messageName");
                entity.Property(e => e.DateSend).HasColumnName("messageData");
                entity.Property(e => e.IsSent).HasColumnName("is_sent");
                entity.Property(e => e.MessageId).HasColumnName("id");

                entity.HasOne(x => x.UserTO).WithMany(m => m.messagesTo).HasForeignKey(x => x.UserTOId).HasConstraintName("messageToUserFK");
                entity.HasOne(x => x.UserFrom).WithMany(m => m.messagesFrom).HasForeignKey(x => x.UserFromId).HasConstraintName("messageFromUserFK");
            });
        }


    }
}
