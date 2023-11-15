using AutoMapper;
using BookReview.Dto;
using BookReview.Models;

namespace BookReview.Helper
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Book, BookDto>();
            CreateMap<Book, BookRequestDto>();
            CreateMap<BookRequestDto, Book>();
            CreateMap<Review, ReviewDto>();
            CreateMap<BookDto, Book>();
            CreateMap<ReviewDto, Review>();
        }
            
    }
}
