using BookReview.Models;
using MediatR;

namespace BookReview.Queries
{
    public class GetBookListQuery:IRequest<List<Book>>
    {

    }
}
