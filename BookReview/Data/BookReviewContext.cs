using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BookReview.Models;

namespace BookReview.Data
{
    public class BookReviewContext : DbContext
    {
        public BookReviewContext (DbContextOptions<BookReviewContext> options)
            : base(options)
        {
            
        }

        public DbSet<BookReview.Models.Book> Book { get; set; }

        public DbSet<BookReview.Models.Review>? Review { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<BookReview.Models.Book>()
        //        .HasMany(book => book.Reviews)
        //        .WithOne(review => review.Book)
        //        .HasForeignKey(review => review.BookId)
        //        .IsRequired();
        //}
    }
}
