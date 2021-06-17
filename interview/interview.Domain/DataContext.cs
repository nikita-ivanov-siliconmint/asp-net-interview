using interview.Domain.Models;

using Microsoft.EntityFrameworkCore;

namespace interview.Domain
{
    public class DataContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }

        public DbSet<Book> Books { get; set; }


        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().HasData(
                new Author
                {
                    FullName = "Dmitriy Gluhovsky",
                    Age = 29,
                    Books =
                    {
                        new Book
                        {
                            Cost = 200,
                            Name = "Metro 2033",
                            PageCount = 300
                        },
                        new Book
                        {
                            Cost = 250,
                            Name = "Metro 2034",
                            PageCount = 240
                        }
                    }
                },
                new Author
                {
                    FullName = "Jeffry Richter",
                    Age = 72,
                    Books =
                    {
                        new Book
                        {
                            Cost = 400,
                            Name = "CLR via C#",
                            PageCount = 500
                        }
                    }
                },
                new Author
                {
                    FullName = "Author3",
                    Age = 15
                },
                new Author
                {
                    FullName = "Author4",
                    Age = 25
                }
            );
        }
    }
}