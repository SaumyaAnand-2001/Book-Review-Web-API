namespace BookReview.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AuthName { get; set; }
        public List<Review> Reviews { get; set; }
        public override string ToString()
        {
            return Id+" "+Name+" "+AuthName+" "+Reviews;
        }
    }
}
