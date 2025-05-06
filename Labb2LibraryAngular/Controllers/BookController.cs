using FinalProjectLibrary.Models.Books.BookDTOs;
using FinalProjectLibrary.Services;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly BookService _bookService;

    public BookController(BookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBooks()
    {
        var response = await _bookService.GetAllBooksAsync();
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetBookByID(int id)
    {
        var response = await _bookService.GetBookByIdAsync(id);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpGet("title/{title}")]
    public async Task<IActionResult> GetBooksByTitle(string title)
    {
        var response = await _bookService.GetBooksByTitleAsync(title);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpGet("author/{author}")]
    public async Task<IActionResult> GetBooksByAuthor(string author)
    {
        var response = await _bookService.GetBooksByAuthorAsync(author);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> AddBook([FromBody] CreateBookDTO createBookDTO)
    {
        var response = await _bookService.AddBookAsync(createBookDTO);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var response = await _bookService.DeleteBookAsync(id);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateBookInfo(int id, [FromBody] UpdateBookInfoDTO updateBookInfoDTO)
    {
        var response = await _bookService.UpdateBookInfoAsync(id, updateBookInfoDTO);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpPut("stock/{id}")]
    public async Task<IActionResult> UpdateBookStock(int id, [FromBody] UpdateBookStatusDTO updateBookStatusDTO)
    {
        var response = await _bookService.UpdateBookStockAsync(id, updateBookStatusDTO);
        return StatusCode((int)response.StatusCode, response);
    }
}
