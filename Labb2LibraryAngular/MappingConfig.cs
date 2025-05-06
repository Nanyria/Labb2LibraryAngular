using AutoMapper;
using FinalProjectLibrary.Models;
using FinalProjectLibrary.Models.Books;
using FinalProjectLibrary.Models.Books.BookDTOs;
using FinalProjectLibrary.Models.Users;
using FinalProjectLibrary.Models.Users.UserDTOs;

namespace FinalProjectLibrary
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Book, BookDto>()
                .ReverseMap()
                .ForMember(dest => dest.StatusHistory, opt => opt.Ignore());


            // Update for BookStatus - Map StatusHistoryItem from DTO
            CreateMap<UpdateBookStatusDTO, Book>()
                .ForMember(dest => dest.StatusHistory, opt => opt.MapFrom(src =>
                    new List<StatusHistoryItem>
                    {
                    new StatusHistoryItem
                    {
                        BookStatus = src.BookStatus, // Map the BookStatus from DTO
                        Timestamp = DateTime.UtcNow, // Timestamp when the status is updated
                        Notes = "Stock status updated" // Or you can make this dynamic if needed
                    }
                    }))
                .ForMember(dest => dest.Title, opt => opt.Ignore())       // Explicitly ignore unwanted fields
                .ForMember(dest => dest.Author, opt => opt.Ignore())
                .ForMember(dest => dest.Genre, opt => opt.Ignore())
                .ForMember(dest => dest.PublicationYear, opt => opt.Ignore())
                .ForMember(dest => dest.BookDescription, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<Book, UpdateBookInfoDTO>().ReverseMap();

            CreateMap<User, UserDto>()
                .ReverseMap()
                .ForMember(dest => dest.BorrowedBooks, opt => opt.Ignore())
                .ForMember(dest => dest.ReservedBooks, opt => opt.Ignore())
                .ForMember(dest => dest.UserHistory, opt => opt.Ignore());

            CreateMap<User, CreateUserDto>();
            CreateMap<CreateUserDto, User>();
            // Map between User and UpdateUserAsAdminDto
            CreateMap<User, UpdateUserAsAdminDto>().ReverseMap();

            // Map between User and UpdateUserDto
            CreateMap<User, UpdateUserDto>().ReverseMap();
            CreateMap<StatusHistoryItem, StatusHistoryItemDto>().ReverseMap();
        }
    }
}
