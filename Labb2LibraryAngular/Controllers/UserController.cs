using AutoMapper;
using FinalProjectLibrary.Enums;
using FinalProjectLibrary.Models;
using FinalProjectLibrary.Models.Users;
using FinalProjectLibrary.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectLibrary.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepo _userRepo;
        private readonly IBookRepo _bookRepo;
        private readonly IMapper _mapper;

        public UserController(IUserRepo userRepo, IBookRepo bookRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _bookRepo = bookRepo;
            _mapper = mapper;
        }

        [HttpPut("borrow/{userId}/{bookId}")]
        public async Task<IActionResult> BorrowBook([FromRoute] int userId, [FromRoute] int bookId)
        {
            APIResponse response = new APIResponse
            {
                IsSuccess = false,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };

            var user = await _userRepo.GetByIdAsync(userId);
            var book = await _bookRepo.GetByIdAsync(bookId);
            if (user != null && book != null)
            {
                // Add book to user's borrowed books
                user.BorrowedBooks.Add(book);

                // Create a new history record for borrowing
                var userHistory = new UserHistory
                {
                    UserID = user.UserID,
                    Action = BookStatusEnum.Borrowed,
                    Timestamp = DateTime.UtcNow,
                    Notes = $"Borrowed: {book.Title}"
                };

                user.UserHistory.Add(userHistory); // Add history to user

                // Save changes to the repository
                await _userRepo.SaveAsync();
                await _bookRepo.SaveAsync();

                response.IsSuccess = true;
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Result = user;
                return Ok(response);
            }
            else
            {
                response.ErrorMessages.Add("User or Book not found.");
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return NotFound(response);
            }
        }

        [HttpPut("reserve/{userId}/{bookId}")]
        public async Task<IActionResult> ReserveBook([FromRoute] int userId, [FromRoute] int bookId)
        {
            APIResponse response = new APIResponse
            {
                IsSuccess = false,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };

            var user = await _userRepo.GetByIdAsync(userId);
            var book = await _bookRepo.GetByIdAsync(bookId);
            if (user != null && book != null)
            {
                // Add book to user's reserved books
                user.ReservedBooks.Add(book);

                // Create a new history record for reserving
                var userHistory = new UserHistory
                {
                    UserID = user.UserID,
                    Action = BookStatusEnum.Reserved,
                    Timestamp = DateTime.UtcNow,
                    Notes = $"Reserved: {book.Title}"
                };

                user.UserHistory.Add(userHistory); // Add history to user

                // Save changes to the repository
                await _userRepo.SaveAsync();
                await _bookRepo.SaveAsync();

                response.IsSuccess = true;
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Result = user;
                return Ok(response);
            }
            else
            {
                response.ErrorMessages.Add("User or Book not found.");
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return NotFound(response);
            }
        }

        [HttpPut("return/{userId}/{bookId}")]
        public async Task<IActionResult> ReturnBook([FromRoute] int userId, [FromRoute] int bookId)
        {
            APIResponse response = new APIResponse
            {
                IsSuccess = false,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };

            var user = await _userRepo.GetByIdAsync(userId);
            var book = await _bookRepo.GetByIdAsync(bookId);
            if (user != null && book != null)
            {
                // Add book to user's reserved books
                user.ReservedBooks.Add(book);

                // Create a new history record for reserving
                var userHistory = new UserHistory
                {
                    UserID = user.UserID,
                    Action = BookStatusEnum.Returned,
                    Timestamp = DateTime.UtcNow,
                    Notes = $"Reserved: {book.Title}"
                };

                user.UserHistory.Add(userHistory); // Add history to user

                // Save changes to the repository
                await _userRepo.SaveAsync();
                await _bookRepo.SaveAsync();

                response.IsSuccess = true;
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Result = user;
                return Ok(response);
            }
            else
            {
                response.ErrorMessages.Add("User or Book not found.");
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return NotFound(response);
            }
        }
    }

}
