using CodeHubDesktop.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeHubDesktop.Data
{
    public class SimpleDbContext : DbContext
    {
        public DbSet<SnippetsModel> SnippetsModel { get; set; }

        public SimpleDbContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite(@"Data Source=localSnippets.db");
        }
    }
}
