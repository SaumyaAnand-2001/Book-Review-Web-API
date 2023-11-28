using BookReview.Data;
using BookReview.Interfaces;
using BookReview.Models;
using BookReview.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookReview.Handler
{
    public class QueryHandler : IRequestHandler<GetBookListQuery, List<Book>>, IRequestHandler<GetBookByIdQuery, Book>
    {
        private readonly IBookRepository _bookRepository;

        public QueryHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        public Task<List<Book>> Handle(GetBookListQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_bookRepository.getAll());
        }

        public Task<Book> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_bookRepository.getById(request.Id));
        }
    }
}
