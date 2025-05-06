using AutoMapper;
using Azure;
using FinalProjectLibrary.Enums;
using FinalProjectLibrary.Models;
using FinalProjectLibrary.Models.Books;
using FinalProjectLibrary.Models.Books.BookDTOs;
using FinalProjectLibrary.Models.Users;
using FinalProjectLibrary.Models.Users.UserDTOs;
using FinalProjectLibrary.Repositories;
using System.Net;

namespace FinalProjectLibrary.Services
{
    public class UserService
    {
        private readonly IUserRepo _userRepo;
        private readonly IBookRepo _bookRepo;
        private readonly IMapper _mapper;

        public UserService(IUserRepo userRepo, IBookRepo bookRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _bookRepo = bookRepo;
            _mapper = mapper;
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
            if (user != null && book != null)
            {

                if (user.ReservedBooks.Contains(book))
                {
                    response.ErrorMessages.Add("Book already reserved by user.");
                    response.StatusCode = HttpStatusCode.Conflict;
                    return response;
                }
                else if (user.BorrowedBooks.Contains(book))
                {
                    response.ErrorMessages.Add("Book already borrowed by user.");
                    response.StatusCode = HttpStatusCode.Conflict;
                    return response;
                }

                else
                {

                    var userHistory = new StatusHistoryItem
                    {
                        UserID = user.UserID,
                        BookStatus = BookStatusEnum.Reserved,
                        Timestamp = DateTime.UtcNow,
                        Notes = $"Reserved: {book.Title}"
                    };

                    user.UserHistory.Add(userHistory);
                    user.ReservedBooks.Add(book);
                    book.StatusHistory.Add(userHistory);
                    book.BookStatus = BookStatusEnum.Reserved;

                    await _userRepo.SaveUserAsync();
                    await _bookRepo.SaveAsync();
                    var userDto = _mapper.Map<UserDto>(user);
                    response.IsSuccess = true;
                    response.StatusCode = HttpStatusCode.OK;
                    response.Result = userDto;
                }
            }
            else
            {
                response.ErrorMessages.Add("User or Book not found.");
                response.StatusCode = HttpStatusCode.NotFound;
            }

            return response;
        }
        public async Task<APIResponse<UserDto>> CancelReservationAsync(int userId, int bookId)
        {
            var response = new APIResponse<UserDto>
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.BadRequest
            };
            var user = await _userRepo.GetUserByIdAsync(userId);
            var book = await _bookRepo.GetByIdAsync(bookId);
            if (user != null && book != null)
            {
                if (user.ReservedBooks.Contains(book))
                {
                    user.ReservedBooks.Remove(book);
                    var userHistory = new StatusHistoryItem
                    {
                        UserID = user.UserID,
                        BookStatus = BookStatusEnum.Available,
                        Timestamp = DateTime.UtcNow,
                        Notes = $"Reservation cancelled: {book.Title}"
                    };
                    user.UserHistory.Add(userHistory);
                    book.StatusHistory.Add(userHistory);
                    book.BookStatus = BookStatusEnum.Available;
                    await _userRepo.SaveUserAsync();
                    await _bookRepo.SaveAsync();
                    var userDto = _mapper.Map<UserDto>(user);
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
        //public async Task<APIResponse> CheckStatus(int userId, int bookId)
        //{
        //    var response = new APIResponse
        //    {
        //        IsSuccess = false,
        //        StatusCode = HttpStatusCode.BadRequest
        //    };
        //    var user = await _userRepo.GetUserByIdAsync(userId);
        //    var book = await _bookRepo.GetByIdAsync(bookId);
        //    if (user != null && book != null)
        //    {
        //        if (user.BorrowedBooks.Contains(book))
        //        {
        //            response.IsSuccess = true;
        //            response.StatusCode = HttpStatusCode.OK;
        //            response.Result = "Book is borrowed by user.";
        //        }
        //        else if (user.ReservedBooks.Contains(book))
        //        {
        //            response.IsSuccess = true;
        //            response.StatusCode = HttpStatusCode.OK;
        //            response.Result = "Book is reserved by user.";
        //        }
        //        else
        //        {
        //            response.IsSuccess = true;
        //            response.StatusCode = HttpStatusCode.OK;
        //            response.Result = "Book is available.";
        //        }
        //    }
        //    else
        //    {
        //        response.ErrorMessages.Add("User or Book not found.");
        //        response.StatusCode = HttpStatusCode.NotFound;
        //    }
        //    return response;
        //}
        public async Task<APIResponse<UserDto>> BorrowBookAsync(int userId, int bookId)
        {
            var response = new APIResponse<UserDto>
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.BadRequest
            };

            // Retrieve user and book from repositories
            var user = await _userRepo.GetUserByIdAsync(userId);
            var book = await _bookRepo.GetByIdAsync(bookId);

            if (user != null && book != null)
            {
                // Check if the book is reserved by the user
                if (user.ReservedBooks.Contains(book))
                {
                    user.ReservedBooks.Remove(book);
                    await SetBorrowedBookAsync(user, book);
                }
                // Check if the book is available
                else if (book.BookStatus == BookStatusEnum.Available)
                {
                    await SetBorrowedBookAsync(user, book);
                }
                else
                {
                    response.ErrorMessages.Add("Book is not available for borrowing.");
                    response.StatusCode = HttpStatusCode.Conflict;
                    return response;
                }

                var userDto = _mapper.Map<UserDto>(user);

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
        public async Task SetBorrowedBookAsync(User user, Book book)
        {
            user.BorrowedBooks.Add(book);

            var userHistory = new StatusHistoryItem
            {
                UserID = user.UserID,
                BookID = book.BookID,
                BookStatus = BookStatusEnum.Borrowed,
                Timestamp = DateTime.UtcNow,
                Notes = $"Borrowed: {book.Title}"
            };

            user.UserHistory.Add(userHistory);
            book.StatusHistory.Add(userHistory);

            book.BookStatus = BookStatusEnum.Borrowed;

            await _userRepo.SaveUserAsync();
            await _bookRepo.SaveAsync();
        }

        public async Task<APIResponse<UserDto>> ReturnBookAsync(int userId, int bookId)
        {
            var response = new APIResponse<UserDto>
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.BadRequest
            };

            var user = await _userRepo.GetUserByIdAsync(userId);
            var book = await _bookRepo.GetByIdAsync(bookId);
            if (user != null && book != null)
            {
                if (user.BorrowedBooks.Contains(book))
                {
                    user.BorrowedBooks.Remove(book);
                }

                var userHistory = new StatusHistoryItem
                {
                    UserID = user.UserID,
                    BookStatus = BookStatusEnum.Available,
                    Timestamp = DateTime.UtcNow,
                    Notes = $"Returned: {book.Title}"
                };

                user.UserHistory.Add(userHistory);
                book.StatusHistory.Add(userHistory);
                book.BookStatus = BookStatusEnum.Available;

                await _userRepo.SaveUserAsync();
                await _bookRepo.SaveAsync();

                var userDto = _mapper.Map<UserDto>(user);
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
    }
}
