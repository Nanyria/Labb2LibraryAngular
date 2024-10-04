using AutoMapper;
using Labb2LibraryAngular.Data;
using Labb2LibraryAngular.Models;
using Labb2LibraryAngular.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Labb2LibraryAngular.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public LibraryController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            APIResponse response = new APIResponse();

            try
            {
                var books = await _context.Books.ToListAsync();

                // Use AutoMapper to map the list of Book entities to BookDTOs
                var bookDTOs = _mapper.Map<List<BookDTO>>(books);

                response.Result = bookDTOs;
                response.IsSuccess = true;
                response.StatusCode = System.Net.HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages.Add(ex.Message);
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
            }

            return Ok(response);
        }

        [HttpGet]
        [Route("{id:int}")]
        [ActionName("GetBookByID")]
        public async Task<IActionResult> GetBookByID([FromRoute] int id)
        {

            APIResponse response = new APIResponse();

            try
            {
                var book = await _context.Books.FirstOrDefaultAsync(c => c.BookID == id);
                if (book != null)
                {
                    var bookDTO = _mapper.Map<BookDTO>(book);

                    response.Result = bookDTO;
                    response.IsSuccess = true;
                    response.StatusCode = System.Net.HttpStatusCode.OK;
                    return Ok(response);
                }
                else
                {
                    response.IsSuccess = false;
                    response.ErrorMessages.Add("Book not found.");
                    response.StatusCode = System.Net.HttpStatusCode.NotFound;
                    return NotFound(response);
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages.Add(ex.Message);
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpGet]
        [Route("title/{title}")]
        [ActionName("GetBooksByTitle")]
        public async Task<IActionResult> GetBooksByTitle([FromRoute] string title)
        {
            APIResponse response = new APIResponse();

            try
            {
                // Find all books with the matching title (case-insensitive search)
                var books = await _context.Books
                                          .Where(c => c.Title.ToLower().Contains(title.ToLower()))
                                          .ToListAsync();

                if (books.Any())
                {
                    // Map the list of Book entities to a list of BookDTOs
                    var bookDTOs = _mapper.Map<List<BookDTO>>(books);

                    response.Result = bookDTOs;
                    response.IsSuccess = true;
                    response.StatusCode = System.Net.HttpStatusCode.OK;
                    return Ok(response);
                }
                else
                {
                    response.IsSuccess = false;
                    response.ErrorMessages.Add("No books found with the provided title.");
                    response.StatusCode = System.Net.HttpStatusCode.NotFound;
                    return NotFound(response);
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages.Add(ex.Message);
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpGet]
        [Route("author/{author}")]
        [ActionName("GetBooksByTitle")]
        public async Task<IActionResult> GetBooksByAuthor([FromRoute] string author)
        {
            APIResponse response = new APIResponse();

            try
            {
                // Find all books with the matching title (case-insensitive search)
                var books = await _context.Books
                                          .Where(c => c.Author.ToLower().Contains(author.ToLower()))
                                          .ToListAsync();

                if (books.Any())
                {
                    // Map the list of Book entities to a list of BookDTOs
                    var bookDTOs = _mapper.Map<List<BookDTO>>(books);

                    response.Result = bookDTOs;
                    response.IsSuccess = true;
                    response.StatusCode = System.Net.HttpStatusCode.OK;
                    return Ok(response);
                }
                else
                {
                    response.IsSuccess = false;
                    response.ErrorMessages.Add("No books found with the provided title.");
                    response.StatusCode = System.Net.HttpStatusCode.NotFound;
                    return NotFound(response);
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages.Add(ex.Message);
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] CreateBookDTO c_Book_DTO)
        {
            APIResponse response = new APIResponse
            {
                IsSuccess = false,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };

            // Basic validation for title, consider adding validation for other fields
            if (string.IsNullOrEmpty(c_Book_DTO.Title))
            {
                response.ErrorMessages.Add("Title must not be empty");
                return BadRequest(response);
            }

            try
            {
                // Map the CreateBookDTO to the Book entity
                Book book = _mapper.Map<Book>(c_Book_DTO);
                await _context.Books.AddAsync(book);
                await _context.SaveChangesAsync();

                // Map the created book back to a BookDTO
                BookDTO bookDTO = _mapper.Map<BookDTO>(book);

                response.Result = bookDTO;
                response.IsSuccess = true;
                response.StatusCode = System.Net.HttpStatusCode.Created;

                // Return 201 Created with the location of the new resource
                return CreatedAtAction(nameof(GetBookByID), new { id = book.BookID }, response);
            }
            catch (Exception ex)
            {
                // Handle any unexpected exceptions
                response.ErrorMessages.Add(ex.Message);
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteBook([FromRoute] int id)
        {
            APIResponse response = new APIResponse();

            try
            {
                var book = await _context.Books.FindAsync(id);
                if (book != null)
                {
                    // Save book details to return after deletion
                    BookDTO deletedBook = _mapper.Map<BookDTO>(book);

                    _context.Books.Remove(book);
                    await _context.SaveChangesAsync();

                    response.IsSuccess = true;
                    response.StatusCode = System.Net.HttpStatusCode.OK;
                    response.Result = deletedBook; // Return deleted book details
                    return Ok(response); // Return 200 OK with deleted book details
                }
                else
                {
                    response.IsSuccess = false;
                    response.ErrorMessages.Add("Book not found.");
                    response.StatusCode = System.Net.HttpStatusCode.NotFound;
                    return NotFound(response);
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages.Add(ex.Message);
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateBookInfo([FromRoute] int id, [FromBody] UpdateBookInfoDTO u_book_DTO)
        {
            APIResponse response = new APIResponse
            {
                IsSuccess = false,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };

            // Validate the incoming DTO
            if (!ModelState.IsValid)
            {
                response.ErrorMessages.Add("Invalid book data.");
                return BadRequest(response);
            }

            try
            {
                var existingBook = await _context.Books.FindAsync(id);
                if (existingBook != null)
                {
                    // Map the updated info to the existing book entity
                    _mapper.Map(u_book_DTO, existingBook);
                    await _context.SaveChangesAsync();

                    // Map the updated book to a DTO and return it in the response
                    BookDTO updatedBook = _mapper.Map<BookDTO>(existingBook);

                    response.Result = updatedBook;
                    response.IsSuccess = true;
                    response.StatusCode = System.Net.HttpStatusCode.OK;
                    return Ok(response);
                }
                else
                {
                    response.IsSuccess = false;
                    response.ErrorMessages.Add("Book not found.");
                    response.StatusCode = System.Net.HttpStatusCode.NotFound;
                    return NotFound(response);
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages.Add(ex.Message);
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpPut]
        [Route("{id:int}/stock")]
        public async Task<IActionResult> UpdateBookStock([FromRoute] int id, [FromBody] UpdateBookStockDTO u_book_s_DTO)
        {
            APIResponse response = new APIResponse
            {
                IsSuccess = false,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };

            // Validate the DTO, if necessary
            if (!ModelState.IsValid)
            {
                response.ErrorMessages.Add("Invalid stock data.");
                return BadRequest(response);
            }

            try
            {
                var existingBook = await _context.Books.FindAsync(id);
                if (existingBook != null)
                {
                    // Update only the stock-related field
                    existingBook.IsInStock = u_book_s_DTO.IsInStock;
                    await _context.SaveChangesAsync();

                    // Return the updated book as a DTO
                    BookDTO updatedBook = _mapper.Map<BookDTO>(existingBook);

                    response.Result = updatedBook;
                    response.IsSuccess = true;
                    response.StatusCode = System.Net.HttpStatusCode.OK;
                    return Ok(response);
                }
                else
                {
                    response.IsSuccess = false;
                    response.ErrorMessages.Add("Book not found.");
                    response.StatusCode = System.Net.HttpStatusCode.NotFound;
                    return NotFound(response);
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages.Add(ex.Message);
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

    }
}
