using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBD2.Model
{
    public class TestDbContext : DbContext
    {
        public TestDbContext(DbContextOptions options):base(options) 
        {
            this.ChangeTracker.LazyLoadingEnabled = true;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
    }
}
