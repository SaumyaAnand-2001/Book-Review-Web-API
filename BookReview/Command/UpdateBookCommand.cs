using BookReview.Models;
using MediatR;

namespace BookReview.Command
{
    public class UpdateBookCommand:IRequest<bool>
    {
        public Book Book { get; set; }
        public UpdateBookCommand(Book book)
        {
            Book = book;
        }
    }
}
