using FurniMpa201.Models;
using Microsoft.EntityFrameworkCore;

namespace FurniMpa201.Context
{
    public class AppDbContext:DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) 
        {
            
        }
    }
}
