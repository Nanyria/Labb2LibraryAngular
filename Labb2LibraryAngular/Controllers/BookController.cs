using FinalProjectLibrary.Enums;
using FinalProjectLibrary.Models.Books;
using FinalProjectLibrary.Models.Books.BookDTOs;
using FinalProjectLibrary.Models.Users;
using FinalProjectLibrary.Services;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly IBookService _bookService;
    private readonly IUserService _userService;
    public BookController(IBookService bookService, IUserService userService)
    {
        _bookService = bookService;
        _userService = userService;
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
    public async Task<IActionResult> AddBook([FromBody] Book book)
    {
        var response = await _bookService.AddBookAsync(book);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpDelete("{bookId:int}")]
    public async Task<IActionResult> DeleteBook(int bookId)
    {
        var response = await _bookService.DeleteBookAsync(bookId);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpPut("{bookId:int}")]
    public async Task<IActionResult> UpdateBookInfo(int bookId, [FromBody] Book bookInfo)
    {
        var response = await _bookService.UpdateBookInfoAsync(bookId, bookInfo);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpPut("stock/{bookId:int}")]
    public async Task<IActionResult> UpdateBookStatus(int bookId, int userId, BookStatusEnum bookStatus, string? notes)
    {
        var bookResponse = await _bookService.GetBookByIdAsync(bookId);
        if (!bookResponse.IsSuccess)
        {
            return StatusCode((int)bookResponse.StatusCode, bookResponse);
        }

        var userResponse = await _userService.GetUserByIdAsync(userId); 
        if (!userResponse.IsSuccess)
        {
            return StatusCode((int)userResponse.StatusCode, userResponse);
        }
        var response = await _bookService.UpdateBookStatusAsync(bookResponse.Result, userResponse.Result, bookStatus, notes);
        return StatusCode((int)response.StatusCode, response);
    }
}
