using BookApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using BookApi.Model.BookModel;


namespace BookApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly MongoDbContext _context;

        public BookController(MongoDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Book>>> GetBooks()
        {
            var books = await _context.Books.Find(book => true).ToListAsync();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(string id)
        {
            var book = await _context.Books.Find(book => book.Id == new ObjectId(id)).FirstOrDefaultAsync();
            if (book == null)
                return NotFound();

            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult> CreateBook(Book book)
        {
            await _context.Books.InsertOneAsync(book);
            return CreatedAtAction(nameof(GetBook), new { id = book.Id.ToString() }, book);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBook(string id, Book book)
        {
            var result = await _context.Books.ReplaceOneAsync(b => b.Id == new ObjectId(id), book);
            if (result.ModifiedCount == 0)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBook(string id)
        {
            var result = await _context.Books.DeleteOneAsync(b => b.Id == new ObjectId(id));
            if (result.DeletedCount == 0)
                return NotFound();

            return NoContent();
        }
    }

}
