using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using My_Book.Data.Models.ViewModels;
using My_Book.Data.Services;

namespace My_Book.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        public BookService _bookService;
        public BookController(BookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost("add-book-with-author")]
        public IActionResult AddBook([FromBody]BookVM book)
        {
            _bookService.AddBookWithAuthors(book);
            return Ok();
        }

        [HttpGet("all-books")]
        public IActionResult GetBooks()
        {
            return Ok(_bookService.GetBooks());
        }

        [HttpGet("get-book-by-id/{id}")]
        public IActionResult GetBook(int id)
        {
            return Ok(_bookService.GetBookById(id));
        }

        [HttpPut("updatebook/{id}")]
        public IActionResult UpdateBook(int id, [FromBody]BookVM book)
        {
            var updateBook = _bookService.UpdateBookById(id, book);
            return Ok(updateBook);
        }

        [HttpDelete("deletebook/{id}")]
        public IActionResult DeeteBook(int id)
        {
            _bookService.DeleteBookById(id);
            return Ok();
        }
    }
}
