using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using MvcLibrary.Data;

using System;
using System.Linq;

namespace MvcLibrary.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new MvcLibraryContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<MvcLibraryContext>>()))
        {
            // Look for any books.
            if (context.Book.Any())
            {
                return;   // DB has been seeded
            }
            context.Book.AddRange(
                new Book
                {
                    Title = "Huck finn",
                    Description ="book about stuff",
                    Author = "larry",
                    CoverImage = "Img",
                    Publisher = "Barber Inc",
                    PublicationDate = DateTime.Parse("1984-3-13"),
                    Category = "fiction",
                    ISBN = 100,
                    PageCount = 69,
                    IsAvailable = true,
                    Rating = 2
                },
                new Book
                {
                    Title = "Troy Web",
                    Description = "Kick ass",
                    Author = "Jon",
                    CoverImage = "Img2",
                    Publisher = "Barber Inc",
                    PublicationDate = DateTime.Parse("1985-3-13"),
                    Category = "Non fiction",
                    ISBN = 101,
                    PageCount = 68,
                    IsAvailable = true,
                    Rating = 1
                },
                new Book
                {
                    Title = "UL",
                    Description = "UL Solutions",
                    Author = "Jenn",
                    CoverImage = "Img",
                    Publisher = "G Books",
                    PublicationDate = DateTime.Parse("1982-3-13"),
                    Category = "biography",
                    ISBN = 102,
                    PageCount = 67,
                    IsAvailable = true,
                    Rating = 3
                },
                new Book
                {
                    Title = "Meta",
                    Description = "Facebook",
                    Author = "Mark Zuck",
                    CoverImage = "Img",
                    Publisher = "ABC",
                    PublicationDate = DateTime.Parse("1981-3-13"),
                    Category = "Fiction",
                    ISBN = 103,
                    PageCount = 70,
                    IsAvailable = true,
                    Rating = 4
                }
            );
            context.SaveChanges();
        }
    }
}