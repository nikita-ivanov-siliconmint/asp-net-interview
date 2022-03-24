using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using interview.Application.Models;

namespace interview.Application.Data
{
    public static class DataSeeder
    {
        private static readonly Fixture Fixture = new();
        
        public static void SeedData(DataContext dataContext)
        {
            var dummyAuthors = GetRandom<Author>(2).ToList();

            dummyAuthors[0].Rank = "Advanced Author";
            dummyAuthors[1].Rank = "Unknown";
            
            var dummyUser = new User
            {
                Id = 1,
                Login = "testUser",
                Password = "testPassword"
            };

            dataContext.Authors.AddRange(dummyAuthors);
            dataContext.Users.Add(dummyUser);
            dataContext.SaveChanges();
        }

        private static IEnumerable<T> GetRandom<T>(int count)
        {
            for (var i = 0; i < count; i++)
            {
                yield return Fixture.Create<T>();
            }
        }
    }
}