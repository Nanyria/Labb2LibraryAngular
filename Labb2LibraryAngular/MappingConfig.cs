using AutoMapper;
using Labb2LibraryAngular.Models;
using Labb2LibraryAngular.Models.DTOs;

namespace Labb2LibraryAngular
{
    public class MappingConfig :Profile
    {
        public MappingConfig()
        {
            CreateMap<Book, BookDTO>().ReverseMap();
            CreateMap<Book, CreateBookDTO>().ReverseMap();
            CreateMap<UpdateBookStockDTO, Book>()
                .ForMember(dest => dest.IsInStock, opt => opt.MapFrom(src => src.IsInStock))
                .ForMember(dest => dest.Title, opt => opt.Ignore())        // Explicitly ignore unwanted fields
                .ForMember(dest => dest.Author, opt => opt.Ignore())
                .ForMember(dest => dest.Genre, opt => opt.Ignore())
                .ForMember(dest => dest.PublicationYear, opt => opt.Ignore())
                .ForMember(dest => dest.BookDescription, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<Book, UpdateBookInfoDTO>().ReverseMap();
        }
    }
}
