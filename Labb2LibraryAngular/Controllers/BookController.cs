using FinalProjectLibrary.Enums;
using FinalProjectLibrary.Models.Books.BookDTOs;
using FinalProjectLibrary.Models.Users;
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
        var response = await _bookService.GetAllBookDtosAsync();
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetBookByID(int id)
    {
        var response = await _bookService.GetBookDtoByIdAsync(id);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpGet("title/{title}")]
    public async Task<IActionResult> GetBooksByTitle(string title)
    {
        var response = await _bookService.GetBookDtosByTitleAsync(title);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpGet("author/{author}")]
    public async Task<IActionResult> GetBooksByAuthor(string author)
    {
        var response = await _bookService.GetBookDtosByAuthorAsync(author);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> AddBook([FromBody] BookDto bookDTO)
    {
        var response = await _bookService.AddBookDtoAsync(bookDTO);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var response = await _bookService.DeleteBookAsync(id);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateBookInfo(int id, [FromBody] BookDto bookInfoDTO)
    {
        var response = await _bookService.UpdateBookInfoAsync(id, bookInfoDTO);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpPut("stock/{id}")]
    public async Task<IActionResult> UpdateBookStatus(int bookId, int userId, BookStatusEnum bookStatus, string? notes)
    {
        var response = await _bookService.UpdateBookStatusAsync(bookId, userId, bookStatus, notes);
        return StatusCode((int)response.StatusCode, response);
    }
}
