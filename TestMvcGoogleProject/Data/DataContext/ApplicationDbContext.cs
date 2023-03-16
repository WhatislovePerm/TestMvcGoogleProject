using Microsoft.EntityFrameworkCore;
using TestMvcGoogleProject.Data.Entity;
using TestMvcGoogleProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace TestMvcGoogleProject.Data.DataContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Post> Posts { get; set; }

    }
}

