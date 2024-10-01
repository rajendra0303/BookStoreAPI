using BookStoreAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Controllers
{
    public class BookContext :DbContext
    {
        public BookContext(DbContextOptions<BookContext> options):base(options)
        { }


        public DbSet<Book> Books { get; set; }

    }
}
