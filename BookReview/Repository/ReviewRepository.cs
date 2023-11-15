using BookReview.Data;
using BookReview.Interfaces;
using BookReview.Models;

namespace BookReview.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly BookReviewContext _context;

        public ReviewRepository(BookReviewContext context)
        {
            _context = context;
        }
        public bool createReview(Review Review)
        {
            _context.Add(Review);
            return Save();
        }

        public bool deleteReview(Review Review)
        {
            _context.Remove(Review);
            return Save();
        }

        public bool deleteReview(List<Review> reviews)
        {
            _context.RemoveRange(reviews);
            return Save();
        }

        public List<Review> GetAllByBookId(int Id)
        {
            return _context.Review.Where(r => r.Book.Id == Id).ToList();
        }

        public Review getById(int Id)
        {
            return _context.Review.Where(p => p.Id == Id).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool updateReview(Review Review)
        {
            _context.Update(Review);
            return Save();
        }
        public bool ReviewExist(int Id)
        {
            return _context.Review.Any(p => p.Id == Id);
        }

        public List<Review> GetAllReviews()
        {
            return _context.Review.ToList();
        }
    }
}
