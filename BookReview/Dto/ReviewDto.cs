using BookReview.Models;
using System.Xml.Linq;

namespace BookReview.Dto
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public string comment { get; set; }
        public int bookId { get; set; }

        public override string ToString()
        {
            return "Review Id: " + Id + "\nComment: " + comment + "\nBook Id: " + bookId ;
        }
    }
}
