namespace BookReview.Dto
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AuthName { get; set; }
        public List<ReviewDto> reviews { get; set; }

        public override string ToString()
        {
            return "Book Id: "+Id+"\nBook Name: "+Name+"\nAuthor Name: "+AuthName+"\nReviews: "+ String.Join(",", (object[])reviews?.ToArray());
        }

    }
}
