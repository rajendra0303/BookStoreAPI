using BookStoreAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyBookController : ControllerBase
    {
        private  readonly BookContext _context;
        public MyBookController(BookContext bookContext)

        {
            _context = bookContext;
            
        }
        // fetch the all books

        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            return await _context.Books.ToListAsync();
        }

        // fetch specific book
        [HttpPost]
        [Route("Id")]

        public async Task<ActionResult<IEnumerable<Book>>> GetBook(int id)
        {
            var book = await _context.Books.FindAsync(id);  //find specific id in bookk store
            if (book == null)
            {
                return NotFound();  //404 status code
            }
            return Ok(book); //200 status code 
        }


        //add new Book in book store

        [HttpPost]
        [Route("AddBook")]

        public async Task<ActionResult<Book>> addBook( Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return Ok(book);
        }

        // Delete book in bookstore
        [HttpDelete]
        [Route("DeleteBook/{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);  //await the async method
            if (book == null)
            {
                return NotFound();  //404 status code
            }

            _context.Books.Remove(book);  //remove the book
            await _context.SaveChangesAsync();  //save changes

            return NoContent();  //204 status code
        }

        //using where clause 
        [HttpGet]
        [Route("FindSpecificTitle")]

        public async Task<ActionResult<Book>> FindBookByTitle(string title)
        {
            var book = await _context.Books
                .Where(x => x.Title == title).FirstOrDefaultAsync();
            if(book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        // New endpoint to get all books ordered by price
        [HttpGet]
        [Route("GetBookaOrderByPrice")]

        public async Task<ActionResult<IEnumerable<Book>>> GetBookOrderByPrice()
        {
            var book = await _context.Books
                .OrderBy(x => x.Price).ToListAsync();
            return Ok(book);
        }

        // New endpoint to get all books ordered by price in descending order
        [HttpGet]
        [Route("GetBooksOrderedByPriceDescending")]
        public async Task<ActionResult<IEnumerable<Book>>> GetBookOrderByPriceDescending()
        {
            var book = await _context.Books
                .OrderByDescending(x => x.Price).ToListAsync(); 
            return Ok(book);
        }

        [HttpGet]
        [Route("CountBooks")]

        public async Task<ActionResult<int>> CountBook()
        {
            int count = await _context.Books.CountAsync();
            return Ok(count);
        }

        // New endpoint to select specific fields of books
        [HttpGet]
        [Route("SelectBookDetails")]
        public async Task<ActionResult<IEnumerable<object>>> SelectBookDetails()
        {
            // Select specific fields from the Book entity
            var selectedBooks = await _context.Books
                .Select(b => new
                {
                    b.Title,
                    b.Author,
                    b.Price
                })
                .ToListAsync();

            return Ok(selectedBooks); // Return the selected book details
        }


    }
}
