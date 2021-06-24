namespace interview.Models
{
    public class BookApi
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Cost { get; set; }

        public int PageCount { get; set; }

        public AuthorApi Author { get; set; }
    }
}