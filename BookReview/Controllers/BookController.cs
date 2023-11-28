using AutoMapper;
using BookReview.Data;
using BookReview.Dto;
using BookReview.Interfaces;
using BookReview.Models;
using BookReview.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BookReview.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;
        private readonly BookReviewContext _context;
        

        //public IActionResult Index() { 
        //    return View();
        //}
        public BookController(IBookRepository bookRepository,
            IReviewRepository reviewRepository,IMapper mapper,BookReviewContext bookReviewContext)
        {
            _bookRepository = bookRepository;
            _reviewRepository = reviewRepository;
            _mapper = mapper;
            _context= bookReviewContext;
        }

        [HttpPost]
        [Route("addbook")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult AddBook([FromBody] BookRequestDto bookRequestDto) {
            if (bookRequestDto == null)
                return BadRequest(ModelState);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_bookRepository.createBook(_mapper.Map<Book>(bookRequestDto)))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created");

        }

        [HttpGet]
        [Route("book/{Id}")]
        [ProducesResponseType(200, Type = typeof(BookDto))]
        [ProducesResponseType(400)]
        public IActionResult findBook(int Id)
        {
            if (!_bookRepository.BookExists(Id))
                return NotFound();
            var book = _mapper.Map < BookDto > (_bookRepository.getById(Id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(book);
        }

        [HttpGet]
        [Route("allbook")]
        [ProducesResponseType(200, Type = typeof(IList<BookDto>))]
        [ProducesResponseType(400)]
        public IActionResult getBooks() {
            var books = _mapper.Map < List < BookDto >>( _bookRepository.getAll());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(books);
        }

        [HttpPut]
        [Route("updatebook")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateBook([FromBody] BookRequestDto bookRequestDto) {
            if (bookRequestDto == null)
                return BadRequest(ModelState);

            if (!_bookRepository.BookExists(bookRequestDto.Id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

           

            if (!_bookRepository.updateBook(_mapper.Map<Book>(bookRequestDto)))
            {
                ModelState.AddModelError("", "Something went wrong updating owner");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete]
        [Route("deletebook")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteBook([FromBody]BookRequestDto bookRequestDto) {
            if (!_bookRepository.BookExists(bookRequestDto.Id))
            {
                return NotFound();
            }

            var reviewsToDelete = _reviewRepository.GetAllByBookId(bookRequestDto.Id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_reviewRepository.deleteReview(reviewsToDelete.ToList()))
            {
                ModelState.AddModelError("", "Something went wrong when deleting reviews");
            }

            if (!_bookRepository.deleteBook(_mapper.Map<Book>(bookRequestDto)))
            {
                ModelState.AddModelError("", "Something went wrong deleting owner");
            }

            return NoContent();
        }
        [HttpDelete]
        [Route("clear")]
        [ProducesResponseType(200)]
        
        public IActionResult ClearDb()
        {
            _context.Database.EnsureDeleted();
            return Ok();
        }

    }
}
