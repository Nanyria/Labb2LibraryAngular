using AutoMapper;
using Azure;
using FinalProjectLibrary.Data;
using FinalProjectLibrary.Enums;
using FinalProjectLibrary.Models;
using FinalProjectLibrary.Models.Books;
using FinalProjectLibrary.Models.Books.BookDTOs;
using FinalProjectLibrary.Models.History;
using FinalProjectLibrary.Models.History.HistoryDTOs;
using FinalProjectLibrary.Models.Users;
using FinalProjectLibrary.Models.Users.UserDTOs;
using FinalProjectLibrary.Repositories;
using System.Linq;
using System.Net;

namespace FinalProjectLibrary.Services
{
    public interface IUserService
    {
        Task<APIResponse<CreateUserDto>> AddUserAsync(CreateUserDto createUserDTO);
        Task<APIResponse<User>> DeleteUserAsync(int userId);
        Task<APIResponse<UpdateUserAsAdminDto>> UpdateUserAsAdminAsync(int userId, UpdateUserAsAdminDto userToUpdate);
        Task<APIResponse<UpdateUserDto>> UpdateUserAsync(int userId, UpdateUserDto userToUpdate);
        Task<APIResponse<User>> GetUserByIdAsync(int userId);
        Task<APIResponse<List<User>>> GetAllUsersAsync();
        Task<APIResponse<User>> ReserveBookAsync(int userId, int bookId);
        Task<APIResponse<User>> CancelReservationAsync(int userId, int bookId);
        Task<APIResponse<User>> CheckOutBookAsync(int userId, int bookId);
        Task<APIResponse<User>> ReturnBookAsync(int userId, int bookId);
    }
    
    public class UserService : IUserService
    {
        private readonly IUserRepo _userRepo;
        private readonly IBookRepo _bookRepo;
        private readonly IMapper _mapper;
        private readonly IBookService _bookService;
        private readonly AppDbContext _dbContext;

        public UserService(IUserRepo userRepo, IBookRepo bookRepo, IMapper mapper, IBookService bookService, AppDbContext dbContext)
        {
            _userRepo = userRepo;
            _bookRepo = bookRepo;
            _mapper = mapper;
            _bookService = bookService;
            _dbContext = dbContext;
        }

        public async Task<APIResponse<CreateUserDto>> AddUserAsync(CreateUserDto createUserDTO)
        {
            var response = new APIResponse<CreateUserDto>
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.BadRequest
            };
            var user = _mapper.Map<User>(createUserDTO);
            await _userRepo.CreateUserAsync(user);
            await _userRepo.SaveUserAsync();

            var createdUserDto = _mapper.Map<CreateUserDto>(user);
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.Created;
            response.Result = createdUserDto;
            return response;
        }
        public async Task<APIResponse<User>> DeleteUserAsync (int userId)
        {
            var response = new APIResponse<User>
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.BadRequest
            };
            var user = await _userRepo.GetUserByIdAsync(userId);
            if (user != null)
            {

                await _userRepo.DeleteUser(user);
                await _userRepo.SaveUserAsync();

                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.NoContent;
                response.Result = user;
            }
            else
            {
                response.ErrorMessages.Add("User not found.");
                response.StatusCode = HttpStatusCode.NotFound;
            }
            return response;
        }
        public async Task<APIResponse<UpdateUserAsAdminDto>> UpdateUserAsAdminAsync(int userId, UpdateUserAsAdminDto userToUpdate)
        {
            var response = new APIResponse<UpdateUserAsAdminDto>
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.BadRequest
            };
            var user = await _userRepo.GetUserByIdAsync(userId);
            if (user != null)
            {
                // Map UpdateUserAsAdminDto to User
                _mapper.Map(userToUpdate, user);

                await _userRepo.UpdateUser(user);
                await _userRepo.SaveUserAsync();

                // Map updated User to UpdateUserAsAdminDto for the response
                var updatedUserDto = _mapper.Map<UpdateUserAsAdminDto>(user);
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Result = updatedUserDto;
            }
            else
            {
                response.ErrorMessages.Add("User not found.");
                response.StatusCode = HttpStatusCode.NotFound;
            }
            return response;
        }
        public async Task<APIResponse<UpdateUserDto>> UpdateUserAsync(int userId, UpdateUserDto userToUpdate)
        {
            var response = new APIResponse<UpdateUserDto>
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.BadRequest
            };
            var user = await _userRepo.GetUserByIdAsync(userId);
            if (user != null)
            {
                // Map UpdateUserDto to User
                _mapper.Map(userToUpdate, user);

                await _userRepo.UpdateUser(user);
                await _userRepo.SaveUserAsync();

                // Map updated User to UpdateUserDto for the response
                var updatedUserDto = _mapper.Map<UpdateUserDto>(user);

                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Result = updatedUserDto;
            }
            else
            {
                response.ErrorMessages.Add("User not found.");
                response.StatusCode = HttpStatusCode.NotFound;
            }
            return response;
        }
        public async Task<APIResponse<User>> GetUserByIdAsync(int userId)
        {
            var response = new APIResponse<User>
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.BadRequest
            };
            var user = await _userRepo.GetUserByIdAsync(userId);
            if (user != null)
            {

                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Result = user;
            }
            else
            {
                response.ErrorMessages.Add("User not found.");
                response.StatusCode = HttpStatusCode.NotFound;
            }
            return response;
        }
        public async Task<APIResponse<List<User>>> GetAllUsersAsync()
        {
            var response = new APIResponse<List<User>>
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.BadRequest
            };
            var users = await _userRepo.GetAllUsersAsync();
            if (users != null)
            {
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
            }
            else
            {
                response.ErrorMessages.Add("No users found.");
                response.StatusCode = HttpStatusCode.NotFound;
            }
            return response;
        }
        public async Task<APIResponse<User>> ReserveBookAsync(int userId, int bookId)
        {
            var response = new APIResponse<User>
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.BadRequest
            };

            var user = await _userRepo.GetUserByIdAsync(userId);
            var book = await _bookRepo.GetByIdAsync(bookId);

            if (user == null || book == null)
            {
                response.ErrorMessages.Add("User or Book not found.");
                response.StatusCode = HttpStatusCode.NotFound;
                return response;
            }


            if (user != null && book != null)
            {
                if (user.ReservedBooks.Any(r => r.BookID == book.BookID))
                {
                    response.ErrorMessages.Add("Book already reserved by user.");
                    response.StatusCode = HttpStatusCode.Conflict;
                    return response;
                }
                else if (user.CheckedOutBooks.Any(b => b.BookId == book.BookID))
                {
                    response.ErrorMessages.Add("Book already checked out by user.");
                    response.StatusCode = HttpStatusCode.Conflict;
                    return response;
                };

                AddReservation(user, book); 

                await _bookService.UpdateBookStatusAsync(book, user, BookStatusEnum.Reserved, $"Book reserved by {user.UserName}"); // Update the book status to Reserved

                await _dbContext.SaveChangesAsync();

                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Result = user;
            }
            else
            {
                response.ErrorMessages.Add("User or Book not found.");
                response.StatusCode = HttpStatusCode.NotFound;
            }

            return response;
        }
        private void AddReservation (User user, Book book)
        {
            var reservation = new ReservationItem
            {
                BookID = book.BookID,
                Book = book,
                BookIsAvaliableEmailSent = null,
                AvailabilityDate = null,
                UserID = user.UserID,
                User = user,
                ReservationDate = DateTime.UtcNow
            };
            user.ReservedBooks.Add(reservation);
            book.Reservations.Add(reservation);
            _dbContext.ReservationItems.Add(reservation);
        }

        public bool RemoveReservation(User user, Book book)
        {
            // Find the reservation item for the given book
            var reservationItem = user.ReservedBooks.FirstOrDefault(r => r.BookID == book.BookID);
            if (reservationItem != null)
            {
                // Remove the reservation from both the user and the book
                user.ReservedBooks.Remove(reservationItem);
                book.Reservations.Remove(reservationItem);

                return true; 
            }

            return false; 
        }

        public async Task<APIResponse<User>> CancelReservationAsync(int userId, int bookId)
        {
            var response = new APIResponse<User>
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.BadRequest
            };

            var bookResponse = await _bookService.GetBookByIdAsync(bookId);
            var userResponse = await GetUserByIdAsync(userId);
            var book = bookResponse.Result;
            var user = userResponse.Result;


            if (user != null && book != null)
            {
                // Use the refactored RemoveReservation method
                if (RemoveReservation(user, book))
                {
                    await _bookService.UpdateBookStatusAsync(book, user, BookStatusEnum.Available, $"Reservation cancelled by {user.UserName}");

                    await _userRepo.SaveUserAsync();
                    await _bookRepo.SaveAsync();


                    response.IsSuccess = true;
                    response.StatusCode = HttpStatusCode.OK;
                    response.Result = user;
                }
                else
                {
                    response.ErrorMessages.Add("Book not reserved by user.");
                    response.StatusCode = HttpStatusCode.Conflict;
                }
            }
            else
            {
                response.ErrorMessages.Add("User or Book not found.");
                response.StatusCode = HttpStatusCode.NotFound;
            }

            return response;
        }

        public async Task<APIResponse<User>> CheckOutBookAsync(int userId, int bookId)
        {
            var response = new APIResponse<User>
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.BadRequest
            };


            var bookResponse = await _bookService.GetBookByIdAsync(bookId);
            var userResponse = await GetUserByIdAsync(userId);
            var book = bookResponse.Result;
            var user = userResponse.Result;

            if (user != null && book != null)
            {
                // Check if the book is reserved by the user
                if (RemoveReservation(user, book) || book.BookStatus == BookStatusEnum.Available)
                {
                   SetCheckedOutBookAsync(user, book);
                   await _bookService.UpdateBookStatusAsync(book, user, BookStatusEnum.CheckedOut, $"Checked out by {user.UserName}");
                }

                else
                {
                    response.ErrorMessages.Add("Book is not available to check out.");
                    response.StatusCode = HttpStatusCode.Conflict;
                    return response;
                }

                await _userRepo.UpdateUser(user);
                await _userRepo.SaveUserAsync();
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Result = user;
            }
            else
            {
                response.ErrorMessages.Add("User or Book not found.");
                response.StatusCode = HttpStatusCode.NotFound;
            }

            return response;
        }
        public void SetCheckedOutBookAsync(User user, Book book)
        {
            var checkedOutItem = new CheckedOutItem
            {
                BookId = book.BookID,
                UserId = user.UserID,
                CheckOutDate = DateTime.UtcNow,
                ReturnDate = DateTime.UtcNow.AddMonths(1),
            };
            user.CheckedOutBooks.Add(checkedOutItem);
        }

        public async Task<APIResponse<User>> ReturnBookAsync(int userId, int bookId)
        {
            var response = new APIResponse<User>
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.BadRequest
            };

            var bookResponse = await _bookService.GetBookByIdAsync(bookId);
            var userResponse = await GetUserByIdAsync(userId);
            var book = bookResponse.Result;
            var user = userResponse.Result;
            if (user != null && book != null)
            {
                if (user.CheckedOutBooks.Any(c => c.BookId == book.BookID))
                {
                    RemoveFromCheckedOutList(user, book);
                }
                await _bookService.UpdateBookStatusAsync(book, user, BookStatusEnum.Returned, $"Returned by {user.UserName}");

                await _userRepo.SaveUserAsync();
                await _bookRepo.SaveAsync();

                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Result = user;
            }
            else
            {
                response.ErrorMessages.Add("User or Book not found.");
                response.StatusCode = HttpStatusCode.NotFound;
            }

            return response;
        }
        public bool RemoveFromCheckedOutList(User user, Book book)
        {
            // Find the reservation item for the given book
            var checkedOutItem = user.CheckedOutBooks.FirstOrDefault(r => r.BookId == book.BookID);
            if (checkedOutItem != null)
            {
                // Remove the reservation from both the user and the book
                user.CheckedOutBooks.Remove(checkedOutItem);
                book.CheckedOutBy = null;

                return true;
            }

            return false;
        }
    }
}
