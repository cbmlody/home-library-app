using AutoMapper;

using HomeLibraryAPI.EF.DTO;
using HomeLibraryAPI.EF.Models;

namespace HomeLibraryAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Author, AuthorDto>();
            CreateMap<Book, BookDto>();
            CreateMap<Publisher, PublisherDto>();
        }
    }
}
