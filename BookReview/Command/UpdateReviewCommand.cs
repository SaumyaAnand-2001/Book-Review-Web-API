using BookReview.Models;
using MediatR;

namespace BookReview.Command
{
    public class UpdateReviewCommand:IRequest<bool>
    {
        public Review Review { get; set; }
        public UpdateReviewCommand(Review review)
        { 
            Review = review;
        }
    }
}
