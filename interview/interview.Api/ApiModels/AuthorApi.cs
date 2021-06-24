using System.Collections.Generic;

namespace interview.Models
{
    public class AuthorApi
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public int Age { get; set; }

        public List<BookApi> Books { get; set; }
    }
}