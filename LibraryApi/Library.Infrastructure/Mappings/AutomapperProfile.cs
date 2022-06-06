using AutoMapper;
using Library.Core.DTOs;
using Library.Core.Entities;

namespace Library.Infrastructure.Mappings
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Book, BookDto>().ReverseMap(); 
            CreateMap<Author, AuthorDto>().ReverseMap(); 
            CreateMap<Author, AuthorBookDto>().ReverseMap(); 
        }
    }
}
