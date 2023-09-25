using ITI_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace ITI_Project.Data
{
    public class ApplicationDbContext : DbContext
    {
        protected ApplicationDbContext()
        {
        }
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Category> categories { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Role> roles { get; set; }
        public DbSet<UserRole> userRoles { get; set; }  
    }
}
