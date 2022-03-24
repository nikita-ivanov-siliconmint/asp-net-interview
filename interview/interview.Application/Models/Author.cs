using System.Collections.Generic;

namespace interview.Application.Models
{
    public class Author
    {
        public int Id { get; set; }

        public string FullName { get; set; }
        
        public string Email { get; set; }

        public int Age { get; set; }
        
        public string Rank { get; set; }

        public List<Book> Books { get; set; } = new();
    }
}