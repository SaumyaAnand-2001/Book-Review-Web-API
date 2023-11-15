using BookReview.Data;
using BookReview.Interfaces;
using BookReview.Models;
using Microsoft.EntityFrameworkCore;

namespace BookReview.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookReviewContext _context;

        public BookRepository(BookReviewContext context)
        {
            _context = context;
        }
        public bool createBook(Book Book)
        {
            _context.Add(Book);
            return Save();
        }

        public bool deleteBook(Book Book)
        {
            _context.Remove(Book);
            return Save();

        }

        public List<Book> getAll()
        {
            return _context.Book.Include(Book=>Book.Reviews).ToList();
        }

        public Book getById(int Id)
        {
            return _context.Book.Where(book => book.Id == Id).Include(book=>book.Reviews).FirstOrDefault();
        }

        public bool updateBook(Book Book)
        {
            _context.Update(Book);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool BookExists(int Id)
        {
            return _context.Book.Any(p => p.Id == Id);
        }
    }
}
