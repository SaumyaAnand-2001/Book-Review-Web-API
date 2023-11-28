using BookReview.Models;
using MediatR;

namespace BookReview.Command
{
    public class AddReviewCommand:IRequest<bool>
    {
        public Review Review { get; set; }
        public AddReviewCommand(Review review) { 
            Review = review;
        }
    }
}
