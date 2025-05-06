using AutoMapper;
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

        public async Task<APIResponse> GetAllBooksAsync()
        {
            var response = new APIResponse();

            try
            {
                var books = await _bookRepo.GetAllAsync();
                var bookDTOs = _mapper.Map<List<BookDTO>>(books);

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

        public async Task<APIResponse> GetBookByIdAsync(int id)
        {
            var response = new APIResponse();

            try
            {
                var book = await _bookRepo.GetByIdAsync(id);
                if (book != null)
                {
                    var bookDTO = _mapper.Map<BookDTO>(book);
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

        public async Task<APIResponse> GetBooksByTitleAsync(string title)
        {
            var response = new APIResponse();

            try
            {
                var books = await _bookRepo.GetByTitleAsync(title);
                if (books.Any())
                {
                    var bookDTOs = _mapper.Map<List<BookDTO>>(books);
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

        public async Task<APIResponse> GetBooksByAuthorAsync(string author)
        {
            var response = new APIResponse();

            try
            {
                var books = await _bookRepo.GetByAuthorAsync(author);
                if (books.Any())
                {
                    var bookDTOs = _mapper.Map<List<BookDTO>>(books);
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

        public async Task<APIResponse> AddBookAsync(CreateBookDTO createBookDTO)
        {
            var response = new APIResponse
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

                var bookDTO = _mapper.Map<BookDTO>(book);
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

        public async Task<APIResponse> DeleteBookAsync(int id)
        {
            var response = new APIResponse();

            try
            {
                var book = await _bookRepo.GetByIdAsync(id);
                if (book != null)
                {
                    var deletedBook = _mapper.Map<BookDTO>(book);
                    _bookRepo.DeleteAsync(book);
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

        public async Task<APIResponse> UpdateBookInfoAsync(int id, UpdateBookInfoDTO updateBookInfoDTO)
        {
            var response = new APIResponse
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

                    var updatedBook = _mapper.Map<BookDTO>(existingBook);
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

        public async Task<APIResponse> UpdateBookStockAsync(int id, UpdateBookStatusDTO updateBookStatusDTO)
        {
            var response = new APIResponse
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

                    var updatedBook = _mapper.Map<BookDTO>(existingBook);
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
    }
}
