// using System.Text.
using System.Security.Cryptography;
using System.Text;
using Domain.Entities;
using Infrastructure.Persistance;

namespace Infrastructure.Persistance.Seeding
{
    public class DataContextSeeding
    {
        public static async Task SeedDatabaseWithUsers(DataContext context)
        {
            if (context.Users.Any())
                return;

            var APassword = GetHasedPasswordAndKey("Password123");

            var users = new List<User>
            {
                new User
                {
                    FirstName = "Luke",
                    LastName = "Ratatui",
                    Email = "LukeRatatui@gmail.com",
                    Age = 20,
                    AccountCreation = DateTime.UtcNow,
                    Password = APassword.Item1,
                    PasswordKey = APassword.Item2
                },
                new User
                {
                    FirstName = "Lisa",
                    LastName = "Lambatz",
                    Email = "LisaLambatz",
                    Age = 42,
                    AccountCreation = DateTime.UtcNow,
                    Password = APassword.Item1,
                    PasswordKey = APassword.Item2,
                    Books = new List<Book>
                    {
                        new Book
                        {
                            Name = "Professional CMake",
                            PublishingDate = new DateTime(2018, 01, 02),
                            Format = "Pdf",
                            Pages = 1101,
                            Authors = new List<Author>
                            {
                                new Author
                                {
                                    FirstName = "Lerry",
                                    LastName = "Wheelson",
                                    Age = 41
                                },
                                new Author
                                {
                                    FirstName = "Major",
                                    LastName = "Tom",
                                    Age = 56
                                }
                            }
                        },
                        new Book
                        {
                            Name = "Test Driven Development with C++",
                            PublishingDate = new DateTime(2012, 02, 27),
                            Format = "Pdf",
                            Pages = 521,
                            Authors = new List<Author>
                            {
                                new Author
                                {
                                    FirstName = "Alfred",
                                    LastName = "Orus",
                                    Age = 43
                                }
                            }
                        },
                        new Book
                        {
                            Name = "C# in a nutshell",
                            PublishingDate = new DateTime(2019, 11, 14),
                            Format = "Pdf",
                            Pages = 1243
                        }
                    }
                },
                new User
                {
                    FirstName = "Kaktor",
                    LastName = "Dumbatz",
                    Email = "KaktorDumbatz@gmail.com",
                    Age = 20,
                    AccountCreation = DateTime.UtcNow,
                    Password = APassword.Item1,
                    PasswordKey = APassword.Item2,
                    Books = new List<Book>
                    {
                        new Book
                        {
                            Name = "Im ok; your ok",
                            PublishingDate = new DateTime(2009, 07, 21),
                            Format = "Epub",
                            Pages = 412
                        },
                        new Book
                        {
                            Name = "Professional CMake",
                            PublishingDate = new DateTime(2018, 01, 02),
                            Format = "Pdf",
                            Pages = 1101,
                            Authors = new List<Author>
                            {
                                new Author
                                {
                                    FirstName = "Lerry",
                                    LastName = "Wheelson",
                                    Age = 41
                                },
                                new Author
                                {
                                    FirstName = "Major",
                                    LastName = "Tom",
                                    Age = 56
                                }
                            }
                        }
                    }
                }
            };

            context.AddRange(users);
            await context.SaveChangesAsync();
        }

        private static ValueTuple<byte[], byte[]> GetHasedPasswordAndKey(string password)
        {
            var hmac = new HMACSHA256();
            var result = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            return (result, hmac.Key);
        }
    }
}