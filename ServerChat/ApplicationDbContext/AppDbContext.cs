using Microsoft.EntityFrameworkCore;
using ServerChat.Model;

namespace ServerChat.ApplicationDbContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)   
        {
        }
        public DbSet<User> Users { get; set; }  
    }
}
