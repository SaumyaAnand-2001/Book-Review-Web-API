using BookReview.Models;
using MediatR;

namespace BookReview.Command
{
    public class DeleteReviewCommand:IRequest<bool>
    {
        public Review Review { get; set; }
        public DeleteReviewCommand(Review review)
        { 
            Review = review;
        }
    }
}
