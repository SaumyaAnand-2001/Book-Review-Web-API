using AutoMapper;
using BookReview.Command;
using BookReview.Dto;
using BookReview.Models;
using BookReview.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookReview.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CqrsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public CqrsController(IMediator mediator, IMapper mapper)
        { 
            _mediator=mediator;
            _mapper=mapper;
        }
        [HttpGet]
        [Route("/books")]
        public async Task<List<BookDto>> GetAllBooks()
        {
            return _mapper.Map<List<BookDto>>(await _mediator.Send(new GetBookListQuery()));
        }
        [HttpGet]
        [Route("/books/{id}")]
        public async Task<BookDto> GetBookById(int id)
        {
            return _mapper.Map<BookDto>(await _mediator.Send(new GetBookByIdQuery(id)));
        }

        [HttpPost]
        [Route("/books")]
        public async Task<bool> CreateBook([FromBody] BookRequestDto bookRequestDto)
        {
            Book book = _mapper.Map<Book>(bookRequestDto);
            return await _mediator.Send(new AddBookCommand(book));
        }

        [HttpPut]
        [Route("/books")]
        public async Task<bool> UpdateBook([FromBody] BookRequestDto bookRequestDto)
        {
            Book book = _mapper.Map<Book>(bookRequestDto);
            return await _mediator.Send(new UpdateBookCommand(book));
        }
        [HttpDelete]
        [Route("/books")]
        public async Task<bool> DeleteBook([FromBody] BookDto bookDto)
        {
            Book book= _mapper.Map<Book>(bookDto);
            return await _mediator.Send(new DeleteBookCommand(book));

        }

        [HttpPost]
        [Route("/review")]
        public async Task<bool> CreateReview([FromBody] ReviewDto reviewDto)
        {
            Review review = _mapper.Map<Review>(reviewDto);
            return await _mediator.Send(new AddReviewCommand(review));
        }

        [HttpPut]
        [Route("/review")]
        public async Task<bool> UpdateReview([FromBody] ReviewDto reviewDto)
        {
            Review review = _mapper.Map<Review>(reviewDto);
            return await _mediator.Send(new UpdateReviewCommand(review));
        }

        [HttpDelete]
        [Route("/review")]
        public async Task<bool> DeleteReview([FromBody] ReviewDto reviewDto)
        {
            Review review = _mapper.Map<Review>(reviewDto);
            return await _mediator.Send(new DeleteReviewCommand(review));
        }
    }
}
