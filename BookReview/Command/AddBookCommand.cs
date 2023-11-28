using BookReview.Models;
using MediatR;

namespace BookReview.Command
{
    public class AddBookCommand:IRequest<bool>
    {
        public Book Book { get; set; }
        public AddBookCommand(Book book)
        {
            Book = book;
        }
    }
}
