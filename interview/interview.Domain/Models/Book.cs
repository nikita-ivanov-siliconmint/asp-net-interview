namespace interview.Domain.Models
{
    public class Book
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Cost { get; set; }

        public int PageCount { get; set; }

        public Author Author { get; set; }
    }
}