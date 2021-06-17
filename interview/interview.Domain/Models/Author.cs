using System.Collections.Generic;

namespace interview.Domain.Models
{
    public class Author
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public int Age { get; set; }

        public List<Book> Books { get; set; }
    }
}