using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Library.Models
{
    public class LibraryContext : IdentityDbContext<ApplicationUser>
    {
        public virtual DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<AuthorBook> AuthorBooks { get; set; }

        public virtual DbSet<Copy> Copies { get; set; }
        public DbSet<Patron> Patrons { get; set; }

        public DbSet<Checkout> Checkouts { get; set; }

        public LibraryContext(DbContextOptions options) : base(options) { }
    }
}