using BookReview.Models;

namespace BookReview.Interfaces
{
    public interface IBookRepository
    {
        bool createBook(Book book);
        bool updateBook(Book book);
        bool deleteBook(Book Book);
        Book getById(int Id);
        List<Book> getAll();
        bool BookExists(int Id);
        bool Save();

    }
}
