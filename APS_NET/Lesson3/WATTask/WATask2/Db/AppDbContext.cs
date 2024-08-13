using Microsoft.EntityFrameworkCore;

namespace WATask2.Db
{
    public partial class AppDbContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        private string _connectionString;

        public AppDbContext() { }

        public AppDbContext(string connectString)
        {
            _connectionString = connectString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseNpgsql(_connectionString);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>(en =>
            {
                en.HasKey(e => e.Id).HasName("AuthorId");

                en.ToTable("Authors");

                en.Property(e => e.Id).HasColumnName("Id");
                en.Property(e => e.Name).HasColumnName("Name");

            });

            modelBuilder.Entity<Book>(en =>
            {
                en.HasKey(e => e.Id).HasName("BookId");

                en.ToTable("Books");

                en.Property(e => e.Id).HasColumnName("Id");
                en.Property(e => e.Title).HasColumnName("Title").HasMaxLength(255);

                en.HasOne(en => en.Author).WithMany(p => p.Books);
            });


            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
