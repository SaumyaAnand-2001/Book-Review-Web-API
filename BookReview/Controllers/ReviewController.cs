using AutoMapper;
using BookReview.Dto;
using BookReview.Interfaces;
using BookReview.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookReview.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        //public IActionResult Index() { 
        //    return View();
        //}
        public ReviewController(IBookRepository bookRepository,
            IReviewRepository reviewRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("review")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult AddReview([FromBody] ReviewDto reviewDto)
        {
            if (reviewDto == null)
                return BadRequest(ModelState);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            Review review = _mapper.Map<ReviewDto,Review>(reviewDto);
            Book book = _bookRepository.getById(review.BookId);
            review.Book = book;
            if (!_reviewRepository.createReview(review))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }
            else { 
                book.Reviews.Add(review);
                if (!_bookRepository.updateBook(book))
                {
                    ModelState.AddModelError("", "Something went wrong updating owner");
                    return StatusCode(500, ModelState);
                }
                Console.WriteLine(book.ToString());
            }
            return Ok("Successfully created");

        }

        [HttpGet]
        [Route("review/{Id}")]
        [ProducesResponseType(200, Type = typeof(ReviewDto))]
        [ProducesResponseType(400)]
        public IActionResult findReview(int Id)
        {
            if (!_reviewRepository.ReviewExist(Id))
                return NotFound();
            var reviewDto = _mapper.Map<ReviewDto>(_reviewRepository.getById(Id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviewDto);
        }

        [HttpPut]
        [Route("updatereview")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateReview([FromBody] ReviewDto reviewDto)
        {
            if (reviewDto == null)
                return BadRequest(ModelState);

            if (!_reviewRepository.ReviewExist(reviewDto.Id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            if (!_reviewRepository.updateReview(_mapper.Map<Review>(reviewDto)))
            {
                ModelState.AddModelError("", "Something went wrong updating owner");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
        [HttpDelete]
        [Route("deletereview")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteReview([FromBody] ReviewDto reviewDto)
        {
            if (!_reviewRepository.ReviewExist(reviewDto.Id))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_reviewRepository.deleteReview(_mapper.Map<Review>(reviewDto)))
            {
                ModelState.AddModelError("", "Something went wrong when deleting reviews");
            }
            return NoContent();
        }
        [HttpGet]
        [Route("allreviews")]
        [ProducesResponseType(200, Type = typeof(IList<ReviewDto>))]
        [ProducesResponseType(400)]
        public IActionResult getReviews()
        {
            var review = _mapper.Map<List<ReviewDto>>(_reviewRepository.GetAllReviews());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(review);
        }


    }
}
