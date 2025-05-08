using AutoMapper;
using Azure;
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
        Task<APIResponse<UserDto>> DeleteUserAsync(int userId);
        Task<APIResponse<UpdateUserAsAdminDto>> UpdateUserAsAdminAsync(int userId, UpdateUserAsAdminDto userToUpdate);
        Task<APIResponse<UpdateUserDto>> UpdateUserAsync(int userId, UpdateUserDto userToUpdate);
        Task<APIResponse<UserDto>> GetUserByIdAsync(int userId);
        Task<APIResponse<List<UserDto>>> GetAllUsersAsync();
        Task<APIResponse<UserDto>> ReserveBookAsync(int userId, int bookId);
        Task<APIResponse<UserDto>> CancelReservationAsync(int userId, int bookId);
        Task<APIResponse<UserDto>> CheckOutBookAsync(int userId, int bookId);
        Task<APIResponse<UserDto>> ReturnBookAsync(int userId, int bookId);
    }
    
    public class UserService : IUserService
    {
        private readonly IUserRepo _userRepo;
        private readonly IBookRepo _bookRepo;
        private readonly IMapper _mapper;
        private readonly IBookService _bookService;

        public UserService(IUserRepo userRepo, IBookRepo bookRepo, IMapper mapper, IBookService bookService)
        {
            _userRepo = userRepo;
            _bookRepo = bookRepo;
            _mapper = mapper;
            _bookService = bookService;
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
        public async Task<APIResponse<UserDto>> DeleteUserAsync (int userId)
        {
            var response = new APIResponse<UserDto>
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.BadRequest
            };
            var user = await _userRepo.GetUserByIdAsync(userId);
            if (user != null)
            {

                await _userRepo.DeleteUser(user);
                await _userRepo.SaveUserAsync();
                var userDto = _mapper.Map<UserDto>(user);
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.NoContent;
                response.Result = userDto;
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
        public async Task<APIResponse<UserDto>> GetUserByIdAsync(int userId)
        {
            var response = new APIResponse<UserDto>
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.BadRequest
            };
            var user = await _userRepo.GetUserByIdAsync(userId);
            if (user != null)
            {
                var userDto = _mapper.Map<UserDto>(user);
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Result = userDto;
            }
            else
            {
                response.ErrorMessages.Add("User not found.");
                response.StatusCode = HttpStatusCode.NotFound;
            }
            return response;
        }
        public async Task<APIResponse<List<UserDto>>> GetAllUsersAsync()
        {
            var response = new APIResponse<List<UserDto>>
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.BadRequest
            };
            var users = await _userRepo.GetAllUsersAsync();
            if (users != null)
            {
                var userDtos = _mapper.Map<List<UserDto>>(users);
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Result = userDtos;
            }
            else
            {
                response.ErrorMessages.Add("No users found.");
                response.StatusCode = HttpStatusCode.NotFound;
            }
            return response;
        }
        public async Task<APIResponse<UserDto>> ReserveBookAsync(int userId, int bookId)
        {
            var response = new APIResponse<UserDto>
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.BadRequest
            };

            var user = await _userRepo.GetUserByIdAsync(userId);
            var book = await _bookRepo.GetByIdAsync(bookId);
            var userDto = _mapper.Map<UserDto>(user);
            var bookDto = _mapper.Map<BookDto>(book);

            if (userDto != null && book != null)
            {
                if (userDto.ReservedBooks.Any(r => r.BookID == book.BookID))
                {
                    response.ErrorMessages.Add("Book already reserved by user.");
                    response.StatusCode = HttpStatusCode.Conflict;
                    return response;
                }
                else if (userDto.CheckedOutBooks.Any(b => b.BookId == book.BookID))
                {
                    response.ErrorMessages.Add("Book already checked out by user.");
                    response.StatusCode = HttpStatusCode.Conflict;
                    return response;
                };

                var reservation = new ReservationItemDto
                {
                    BookID = book.BookID,
                    UserID = user.UserID,
                    ReservationDate = DateTime.UtcNow
                };

                // Add the reservation to the user and book and update the book's status which also creates a StatusHistoryItem
                userDto.ReservedBooks.Add(reservation);
                bookDto.Reservations.Add(reservation);
                await _bookService.UpdateBookStatusAsync(bookDto.BookID, userDto.UserID, BookStatusEnum.Reserved, $"Book reserved by {userDto.UserName}"); // Update the book status to Reserved

                await _userRepo.SaveUserAsync();
                await _bookRepo.SaveAsync();

                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Result = userDto;
            }
            else
            {
                response.ErrorMessages.Add("User or Book not found.");
                response.StatusCode = HttpStatusCode.NotFound;
            }

            return response;
        }

        public bool RemoveReservation(UserDto user, BookDto book)
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

        public async Task<APIResponse<UserDto>> CancelReservationAsync(int userId, int bookId)
        {
            var response = new APIResponse<UserDto>
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.BadRequest
            };

            var bookDtoResponse = await _bookService.GetBookDtoByIdAsync(bookId);
            var userDtoResponse = await GetUserByIdAsync(userId);
            var bookDto = bookDtoResponse.Result;
            var userDto = userDtoResponse.Result;


            if (userDto != null && bookDto != null)
            {
                // Use the refactored RemoveReservation method
                if (RemoveReservation(userDto, bookDto))
                {
                    await _bookService.UpdateBookStatusAsync(bookDto.BookID, userDto.UserID, BookStatusEnum.Available, $"Reservation cancelled by {userDto.UserName}");

                    var user = _mapper.Map<User>(userDto);
                    await _userRepo.SaveUserAsync();
                    await _bookRepo.SaveAsync();


                    response.IsSuccess = true;
                    response.StatusCode = HttpStatusCode.OK;
                    response.Result = userDto;
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

        public async Task<APIResponse<UserDto>> CheckOutBookAsync(int userId, int bookId)
        {
            var response = new APIResponse<UserDto>
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.BadRequest
            };


            var bookDtoResponse = await _bookService.GetBookDtoByIdAsync(bookId);
            var userDtoResponse = await GetUserByIdAsync(userId);
            var bookDto = bookDtoResponse.Result;
            var userDto = userDtoResponse.Result;

            if (userDto != null && bookDto != null)
            {
                // Check if the book is reserved by the user
                if (RemoveReservation(userDto, bookDto) || bookDto.BookStatus == BookStatusEnum.Available)
                {
                   SetCheckedOutBookAsync(userDto, bookDto);
                   await _bookService.UpdateBookStatusAsync(bookDto.BookID, userDto.UserID, BookStatusEnum.CheckedOut, $"Checked out by {userDto.UserName}");
                }

                else
                {
                    response.ErrorMessages.Add("Book is not available to check out.");
                    response.StatusCode = HttpStatusCode.Conflict;
                    return response;
                }

                var user = _mapper.Map<User>(userDto);
                await _userRepo.UpdateUser(user);
                await _userRepo.SaveUserAsync();
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Result = userDto;
            }
            else
            {
                response.ErrorMessages.Add("User or Book not found.");
                response.StatusCode = HttpStatusCode.NotFound;
            }

            return response;
        }
        public void SetCheckedOutBookAsync(UserDto user, BookDto book)
        {
            var checkedOutItem = new CheckedOutItemDto
            {
                BookId = book.BookID,
                UserId = user.UserID,
                CheckOutDate = DateTime.UtcNow,
                ReturnDate = DateTime.UtcNow.AddMonths(1),
            };
            user.CheckedOutBooks.Add(checkedOutItem);
        }

        public async Task<APIResponse<UserDto>> ReturnBookAsync(int userId, int bookId)
        {
            var response = new APIResponse<UserDto>
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.BadRequest
            };

            var bookDtoResponse = await _bookService.GetBookDtoByIdAsync(bookId);
            var userDtoResponse = await GetUserByIdAsync(userId);
            var bookDto = bookDtoResponse.Result;
            var userDto = userDtoResponse.Result;
            if (userDto != null && bookDto != null)
            {
                if (userDto.CheckedOutBooks.Any(c => c.BookId == bookDto.BookID))
                {
                    RemoveFromCheckedOutList(userDto, bookDto);
                }
                await _bookService.UpdateBookStatusAsync(bookDto.BookID, userDto.UserID, BookStatusEnum.Returned, $"Returned by {userDto.UserName}");

                await _userRepo.SaveUserAsync();
                await _bookRepo.SaveAsync();

                var user = _mapper.Map<User>(userDto);
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Result = userDto;
            }
            else
            {
                response.ErrorMessages.Add("User or Book not found.");
                response.StatusCode = HttpStatusCode.NotFound;
            }

            return response;
        }
        public bool RemoveFromCheckedOutList(UserDto user, BookDto book)
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
