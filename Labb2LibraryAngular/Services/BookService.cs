using AutoMapper;
using FinalProjectLibrary.Enums;
using FinalProjectLibrary.Models;
using FinalProjectLibrary.Models.Books;
using FinalProjectLibrary.Models.Books.BookDTOs;
using FinalProjectLibrary.Models.History;
using FinalProjectLibrary.Models.History.HistoryDTOs;
using FinalProjectLibrary.Models.Users;
using FinalProjectLibrary.Models.Users.UserDTOs;
using FinalProjectLibrary.Repositories;
using System.Net;

namespace FinalProjectLibrary.Services
{
    public interface IBookService
    {
        Task<APIResponse<List<BookDto>>> GetAllBookDtosAsync();
        Task<APIResponse<BookDto>> GetBookDtoByIdAsync(int id);
        Task<APIResponse<List<BookDto>>> GetBookDtosByTitleAsync(string title);
        Task<APIResponse<List<BookDto>>> GetBookDtosByAuthorAsync(string author);
        Task<APIResponse<BookDto>> AddBookDtoAsync(BookDto bookDto);
        Task<APIResponse<BookDto>> DeleteBookAsync(int id);
        Task<APIResponse<BookDto>> UpdateBookInfoAsync(int id, BookDto bookDto);
        Task<APIResponse<BookDto>> UpdateBookStatusAsync(int id, int userId, BookStatusEnum bookStatus, string? n);
    }
    public class BookService : IBookService
    {
        private readonly IBookRepo _bookRepo;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;


        public BookService(IBookRepo bookRepo, IMapper mapper, IUserService userService)
        {
            _bookRepo = bookRepo;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<APIResponse<List<BookDto>>> GetAllBookDtosAsync()
        {
            var response = new APIResponse<List<BookDto>>();

            try
            {
                var books = await _bookRepo.GetAllAsync();
                var bookDTOs = _mapper.Map<List<BookDto>>(books);


                response.Result = bookDTOs;
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages.Add(ex.Message);
                response.StatusCode = HttpStatusCode.InternalServerError;
            }

            return response;
        }

        public async Task<APIResponse<BookDto>> GetBookDtoByIdAsync(int id)
        {
            var response = new APIResponse<BookDto>();

            try
            {
                var book = await _bookRepo.GetByIdAsync(id);
                if (book != null)
                {
                    var bookDTO = _mapper.Map<BookDto>(book);
                    response.Result = bookDTO;
                    response.IsSuccess = true;
                    response.StatusCode = HttpStatusCode.OK;
                }
                else
                {
                    response.IsSuccess = false;
                    response.ErrorMessages.Add("Book not found.");
                    response.StatusCode = HttpStatusCode.NotFound;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages.Add(ex.Message);
                response.StatusCode = HttpStatusCode.InternalServerError;
            }

            return response;
        }

        public async Task<APIResponse<List<BookDto>>> GetBookDtosByTitleAsync(string title)
        {
            var response = new APIResponse<List<BookDto>>();

            try
            {
                var books = await _bookRepo.GetByTitleAsync(title);
                if (books.Any())
                {

                    var bookDTOs = _mapper.Map<List<BookDto>>(books);
                    response.Result = bookDTOs;
                    response.IsSuccess = true;
                    response.StatusCode = HttpStatusCode.OK;
                }
                else
                {
                    response.IsSuccess = false;
                    response.ErrorMessages.Add("No books found with the provided title.");
                    response.StatusCode = HttpStatusCode.NotFound;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages.Add(ex.Message);
                response.StatusCode = HttpStatusCode.InternalServerError;
            }

            return response;
        }

        public async Task<APIResponse<List<BookDto>>> GetBookDtosByAuthorAsync(string author)
        {
            var response = new APIResponse<List<BookDto>>();

            try
            {
                var books = await _bookRepo.GetByAuthorAsync(author);
                if (books.Any())
                {

                    var bookDTOs = _mapper.Map<List<BookDto>>(books);
                    response.Result = bookDTOs;
                    response.IsSuccess = true;
                    response.StatusCode = HttpStatusCode.OK;
                }
                else
                {
                    response.IsSuccess = false;
                    response.ErrorMessages.Add("No books found with the provided author.");
                    response.StatusCode = HttpStatusCode.NotFound;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages.Add(ex.Message);
                response.StatusCode = HttpStatusCode.InternalServerError;
            }

            return response;
        }

        public async Task<APIResponse<BookDto>> AddBookDtoAsync(BookDto bookDto)
        {
            var response = new APIResponse<BookDto>
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.BadRequest
            };

            if (string.IsNullOrEmpty(bookDto.Title))
            {
                response.ErrorMessages.Add("Title must not be empty.");
                return response;
            }

            try
            {
                var book = new Book
                {
                    Title = bookDto.Title,
                    Author = bookDto.Author,
                    Genre = bookDto.Genre,
                    BookDescription = bookDto.BookDescription,
                    PublicationYear = bookDto.PublicationYear,
                    BookStatus = BookStatusEnum.Available 
                };

                await _bookRepo.CreateBookAsync(book);
                await _bookRepo.SaveAsync();

                var savedBookDto = _mapper.Map<BookDto>(book);

                response.Result = savedBookDto;
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.Created;
            }
            catch (Exception ex)
            {
                response.ErrorMessages.Add(ex.Message);
                response.StatusCode = HttpStatusCode.InternalServerError;
            }

            return response;
        }

        public async Task<APIResponse<BookDto>> DeleteBookAsync(int id)
        {
            var response = new APIResponse<BookDto>();

            try
            {
                var book = await _bookRepo.GetByIdAsync(id);
                if (book != null)
                {
                    var deletedBook = _mapper.Map<BookDto>(book);
                    await _bookRepo.DeleteAsync(book);
                    await _bookRepo.SaveAsync();

                    response.Result = deletedBook;
                    response.IsSuccess = true;
                    response.StatusCode = HttpStatusCode.OK;
                }
                else
                {
                    response.IsSuccess = false;
                    response.ErrorMessages.Add("Book not found.");
                    response.StatusCode = HttpStatusCode.NotFound;
                }
            }
            catch (Exception ex)
            {
                response.ErrorMessages.Add(ex.Message);
                response.StatusCode = HttpStatusCode.InternalServerError;
            }

            return response;
        }

        public async Task<APIResponse<BookDto>> UpdateBookInfoAsync(int id, BookDto bookDto)
        {
            var response = new APIResponse<BookDto>
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.BadRequest
            };

            try
            {
                var existingBook = await _bookRepo.GetByIdAsync(id);
                if (existingBook != null)
                {
                    existingBook.Title = bookDto.Title;
                    existingBook.Author = bookDto.Author;
                    existingBook.Genre = bookDto.Genre;
                    existingBook.PublicationYear = bookDto.PublicationYear;
                    existingBook.BookDescription = bookDto.BookDescription;

                    await _bookRepo.SaveAsync();

                    var updatedBookDto = _mapper.Map<BookDto>(existingBook);
                    response.Result = updatedBookDto;
                    response.IsSuccess = true;
                    response.StatusCode = HttpStatusCode.OK;
                }
                else
                {
                    response.ErrorMessages.Add("Book not found.");
                    response.StatusCode = HttpStatusCode.NotFound;
                }
            }
            catch (Exception ex)
            {
                response.ErrorMessages.Add(ex.Message);
                response.StatusCode = HttpStatusCode.InternalServerError;
            }

            return response;
        }

        public async Task<APIResponse<BookDto>> UpdateBookStatusAsync(int bookId, int userId, BookStatusEnum bookStatus, string? notes)
        {
            var response = new APIResponse<BookDto>
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.BadRequest
            };

            try
            {
                var bookDtoResponse = await GetBookDtoByIdAsync(bookId);
                var userDtoResponse = await _userService.GetUserByIdAsync(userId);

                if (!bookDtoResponse.IsSuccess || bookDtoResponse.Result == null)
                {
                    response.ErrorMessages.Add("Book not found.");
                    response.StatusCode = HttpStatusCode.NotFound;
                    return response;
                }

                if (!userDtoResponse.IsSuccess || userDtoResponse.Result == null)
                {
                    response.ErrorMessages.Add("User not found.");
                    response.StatusCode = HttpStatusCode.NotFound;
                    return response;
                }

                var bookDto = bookDtoResponse.Result;
                var userDto = userDtoResponse.Result;

                GetBookStatus(bookDto, bookStatus); 
                AddStatusHistoryItem(userDto, bookDto, bookStatus, notes);

                var book = _mapper.Map<Book>(bookDto);
                await _bookRepo.UpdateAsync(book);
                await _bookRepo.SaveAsync();

                response.Result = bookDto;
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.ErrorMessages.Add(ex.Message);
                response.StatusCode = HttpStatusCode.InternalServerError;
            }

            return response;
        }
        public BookDto GetBookStatus(BookDto book, BookStatusEnum bookStatus)
        {
            if (bookStatus == BookStatusEnum.Returned)
            {
                // Set to Reserved if there are reservations, otherwise Available
                book.BookStatus = (book.Reservations != null && book.Reservations.Any())
                    ? BookStatusEnum.Reserved
                    : BookStatusEnum.Available;
            }
            else if (book.BookStatus != bookStatus)
            {
                // Determine status based on CheckedOutBy and Reservations
                book.BookStatus = book.CheckedOutBy != null
                    ? BookStatusEnum.CheckedOut
                    : (book.Reservations != null && book.Reservations.Any())
                        ? BookStatusEnum.Reserved
                        : BookStatusEnum.Available;
            }

            return book;
        }
        public void AddStatusHistoryItem(UserDto user, BookDto book, BookStatusEnum bookStatus, string? notes)
        {
            var statusHistoryItem = new StatusHistoryItemDto
            {
                UserID = user.UserID,
                BookID = book.BookID,
                BookStatus = bookStatus,
                Timestamp = DateTime.UtcNow,
                Notes = notes
            };

            user.UserHistory.Add(statusHistoryItem);
            book.StatusHistory.Add(statusHistoryItem);
        }

        public BookStatusEnum GetCurrentStatus(Book book)
        {
            return book.StatusHistory
                .OrderByDescending(sh => sh.Timestamp)
                .FirstOrDefault()?.BookStatus ?? BookStatusEnum.Available;
        }

    }
}
