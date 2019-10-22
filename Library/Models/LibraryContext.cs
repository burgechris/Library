using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Libary.Models
{
    public class LibraryContext : IdentityDbContext<ApplicationUser>
    {
        public virtual DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<CourseStudent> CourseStudent { get; set; }

        public LibraryContext(DbContextOptions options) : base(options) { }
    }
}