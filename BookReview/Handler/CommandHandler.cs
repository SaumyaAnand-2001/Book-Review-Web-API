using BookReview.Command;
using BookReview.Interfaces;
using BookReview.Models;
using BookReview.Repository;
using MediatR;

namespace BookReview.Handler
{
    public class CommandHandler : IRequestHandler<AddBookCommand, bool>, IRequestHandler<UpdateBookCommand, bool>, IRequestHandler<DeleteBookCommand,bool>, IRequestHandler<AddReviewCommand,bool>, IRequestHandler<UpdateReviewCommand,bool>, IRequestHandler<DeleteReviewCommand,bool>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IReviewRepository _reviewRepository;
        public CommandHandler(IBookRepository bookRepository, IReviewRepository reviewRepository)
        {
            _bookRepository = bookRepository;
            _reviewRepository = reviewRepository;
        }
        public Task<bool> Handle(AddBookCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_bookRepository.createBook(request.Book));
        }

        public Task<bool> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_bookRepository.updateBook(request.Book));
        }

        public Task<bool> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_bookRepository.deleteBook(request.Book));
        }

        public Task<bool> Handle(AddReviewCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_reviewRepository.createReview(request.Review));
        }

        public Task<bool> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_reviewRepository.updateReview(request.Review));
        }

        public Task<bool> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_reviewRepository.deleteReview(request.Review));
        }
    }
}

