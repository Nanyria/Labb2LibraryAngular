using AutoMapper;
using FinalProjectLibrary.Enums;
using FinalProjectLibrary.Models;
using FinalProjectLibrary.Models.Books;
using FinalProjectLibrary.Models.Books.BookDTOs;
using FinalProjectLibrary.Repositories;
using System.Net;

namespace FinalProjectLibrary.Services
{
    public class BookService
    {
        private readonly IBookRepo _bookRepo;
        private readonly IMapper _mapper;

        public BookService(IBookRepo bookRepo, IMapper mapper)
        {
            _bookRepo = bookRepo;
            _mapper = mapper;
        }

        public async Task<APIResponse<List<BookDto>>> GetAllBooksAsync()
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

        public async Task<APIResponse<BookDto>> GetBookByIdAsync(int id)
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

        public async Task<APIResponse<List<BookDto>>> GetBooksByTitleAsync(string title)
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

        public async Task<APIResponse<List<BookDto>>> GetBooksByAuthorAsync(string author)
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

        public async Task<APIResponse<CreateBookDto>> AddBookAsync(CreateBookDto createBookDTO)
        {
            var response = new APIResponse<CreateBookDto>
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.BadRequest
            };

            if (string.IsNullOrEmpty(createBookDTO.Title))
            {
                response.ErrorMessages.Add("Title must not be empty.");
                return response;
            }

            try
            {
                var book = _mapper.Map<Book>(createBookDTO);
                await _bookRepo.CreateBookAsync(book);
                await _bookRepo.SaveAsync();

                var bookDTO = _mapper.Map<CreateBookDto>(book);
                response.Result = bookDTO;
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

        public async Task<APIResponse<UpdateBookInfoDTO>> UpdateBookInfoAsync(int id, UpdateBookInfoDTO updateBookInfoDTO)
        {
            var response = new APIResponse<UpdateBookInfoDTO>
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.BadRequest
            };

            try
            {
                var existingBook = await _bookRepo.GetByIdAsync(id);
                if (existingBook != null)
                {
                    _mapper.Map(updateBookInfoDTO, existingBook);
                    await _bookRepo.SaveAsync();

                    var updatedBook = _mapper.Map<UpdateBookInfoDTO>(existingBook);
                    response.Result = updatedBook;
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

        public async Task<APIResponse<UpdateBookStatusDTO>> UpdateBookStockAsync(int id, UpdateBookStatusDTO updateBookStatusDTO, int userId)
        {
            var response = new APIResponse<UpdateBookStatusDTO>
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.BadRequest
            };

            try
            {
                var existingBook = await _bookRepo.GetByIdAsync(id);
                if (existingBook != null)
                {
                    var statusHistoryItem = new StatusHistoryItem
                    {
                        BookID = existingBook.BookID,
                        BookStatus = updateBookStatusDTO.BookStatus,
                        Timestamp = DateTime.UtcNow,
                        Notes = "Stock status updated"
                    };

                    existingBook.StatusHistory.Add(statusHistoryItem);
                    await _bookRepo.SaveAsync();

                    var updatedBook = _mapper.Map<UpdateBookStatusDTO>(existingBook);
                    response.Result = updatedBook;
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

        public BookStatusEnum GetCurrentStatus(Book book)
        {
            return book.StatusHistory
                .OrderByDescending(sh => sh.Timestamp)
                .FirstOrDefault()?.BookStatus ?? BookStatusEnum.Available;
        }
    }
}
