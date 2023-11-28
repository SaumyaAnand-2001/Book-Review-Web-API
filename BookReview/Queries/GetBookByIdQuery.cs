using BookReview.Models;
using MediatR;

namespace BookReview.Queries
{
    public class GetBookByIdQuery:IRequest<Book>
    {
        public int Id { get; set; }
        public GetBookByIdQuery(int id) {
            Id = id;
        }
    }
}
