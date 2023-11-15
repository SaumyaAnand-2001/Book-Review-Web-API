using BookReview.Models;

namespace BookReview.Interfaces
{
    public interface IReviewRepository
    {
        bool createReview(Review Review);
        bool updateReview(Review Review);
        bool deleteReview(Review Review);
        Review getById(int Id);
        List<Review> GetAllReviews();
        public bool Save();

        List<Review> GetAllByBookId(int Id);

        bool deleteReview(List<Review> Review);
        bool ReviewExist(int Id);
    }
}
