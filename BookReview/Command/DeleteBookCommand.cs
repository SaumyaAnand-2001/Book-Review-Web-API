using BookReview.Models;
using MediatR;

namespace BookReview.Command
{
    public class DeleteBookCommand:IRequest<bool>
    {
        public Book Book { get; set; }
        public DeleteBookCommand(Book book)
        {
            Book = book;
        }
    }
}
