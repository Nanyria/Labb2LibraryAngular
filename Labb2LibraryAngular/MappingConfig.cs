using AutoMapper;
using FinalProjectLibrary.Models.Books;
using FinalProjectLibrary.Models.Books.BookDTOs;

namespace FinalProjectLibrary
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Book, BookDTO>().ReverseMap();
            CreateMap<Book, CreateBookDTO>().ReverseMap();

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
        }
    }
}
