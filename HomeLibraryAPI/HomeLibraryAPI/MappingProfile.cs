using AutoMapper;

using HomeLibraryAPI.EF.DTO;
using HomeLibraryAPI.EF.Models;
using HomeLibraryAPI.EF.UpdateDTO;

namespace HomeLibraryAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Author, AuthorDto>().ReverseMap();
            CreateMap<Book, BookDto>().ReverseMap();
            CreateMap<Publisher, PublisherDto>().ReverseMap();
            CreateMap<BookSeries, BookSeriesDto>()
                .ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.Books))
                .ReverseMap();

            CreateMap<AuthorCreateUpdateDto, Author>();
            CreateMap<BookCreateUpdateDto, Book>();
            CreateMap<PublisherCreateUpdateDto, Publisher>();
            CreateMap<BookSeriesCreateUpdateDto, BookSeries>();
        }
    }
}
